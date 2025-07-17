import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import {
  ImageCropperComponent,
  ImageCroppedEvent,
  LoadedImage,
} from 'ngx-image-cropper';
import { CroppedImage } from '../../../core/models/cropped-image';
import { Router } from '@angular/router';
import { FileUploadService } from '../../../core/services/file-upload.service';
import { UploadImage } from '../../../core/models/upload-image';
import { UpdatePhotoRequest } from '../../../core/models/update-photo-request';
import { UploadResponse } from '../../../core/models/upload-response';
import { AuthService } from '../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UtilisateurService } from '../../../core/services/utilisateur.service';
import { UserInfos } from '../../../core/models/user-infos';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-image-add',
  imports: [CommonModule, ImageCropperComponent],
  templateUrl: './image-add.component.html',
  styleUrl: './image-add.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ImageAddComponent implements OnInit {
  imageChangedEvent: Event | null = null;
  croppedImage: SafeUrl = '';
  blob!: Blob;
  uploadImage!: UploadImage;
  id!: number;
  updatePhotoRequest!: UpdatePhotoRequest;
  utilisateur$!: Observable<UserInfos>;

  constructor(
    private sanitizer: DomSanitizer,
    private router: Router,
    private fileUploadService: FileUploadService,
    private authService: AuthService,
    private utilisateurService: UtilisateurService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.utilisateur$ = this.utilisateurService.utilisateur$;
    this.utilisateur$.subscribe({
      next: (infos: UserInfos) => {
        if (infos.id) {
          this.id = infos.id;
        }
      },
    });
  }

  fileChangeEvent(event: Event): void {
    this.imageChangedEvent = event;
  }

  imageCropped(event: ImageCroppedEvent) {
    if (event.objectUrl) {
      this.croppedImage = this.sanitizer.bypassSecurityTrustUrl(
        event.objectUrl
      );
      this.blob = event.blob as Blob; // Ensure the blob is set correctly
    }
  }
  imageLoaded(image: LoadedImage) {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }

  saveChanges(): void {
    if (this.blob) {
      this.fileUploadService.blobToBase64(this.blob).then(
        (base64Image: string) => {
          this.uploadImage = {
            image: base64Image,
            extension: 'png',
          };
          // console.log(this.uploadImage);
          this.fileUploadService.uploadFile(this.uploadImage).subscribe({
            next: (response: UploadResponse) => {
              // console.log(response);
              if (this.id) {
                this.updatePhotoRequest = {
                  photo: response.fileName,
                };
                this.authService
                  .updatePhoto(this.id, this.updatePhotoRequest)
                  .subscribe({
                    next: () => {
                      this.toastr.success('Image uploaded successfully!');
                      this.authService.getUserInfosFromServer();
                      this.utilisateurService.getAllUtilisateurFromServer();
                      this.onCancel();
                    },
                    error: (error) => {
                      this.toastr.error(
                        'Image upload failed. Please try again.'
                      );
                      console.error('Image upload error:', error);
                    },
                  });
              }
            },
            error: (error) => {
              this.toastr.error('Image upload failed. Please try again.');
              console.error('Image upload error:', error);
            },
          });
        },
        (error) => {
          console.error('Error converting blob to base64:', error);
          this.toastr.error('Failed to convert image. Please try again.');
        }
      );
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/home/profile');
  }
}
