import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { UserInfos } from '../../../../core/models/user-infos';
import { AuthService } from '../../../../core/services/auth.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ImageAddComponent } from '../../../composants/image-add/image-add.component';
import { SafeUrl } from '@angular/platform-browser';
import { ChangePasswordComponent } from '../change-password/change-password.component';
import { ChangePasswordRequest } from '../../../../core/models/change-password-request';
import { ToastrService } from 'ngx-toastr';
import { UploadImage } from '../../../../core/models/upload-image';
import { FileUploadService } from '../../../../core/services/file-upload.service';
import { CroppedImage } from '../../../../core/models/cropped-image';

@Component({
  selector: 'app-profile',
  imports: [
    CommonModule,
    AsyncPipe,
    ImageAddComponent,
    ChangePasswordComponent,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProfileComponent implements OnInit {
  userInfos$!: Observable<UserInfos>;
  photo: SafeUrl = './assets/images/default_man.jpg';
  image: Blob | null | undefined = null;
  id!: number;
  uploadImage!: UploadImage;

  constructor(
    private authService: AuthService,
    private fileUploadService: FileUploadService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.userInfos$ = this.authService.userInfos$.pipe(
      tap((infos) => {
        if (infos.id) {
          this.id = infos.id;
        }

        if (infos.photo) {
          this.photo = infos.photo;
        }
      })
    );
    this.userInfos$.subscribe();
  }

  photoChange(croppendImage: CroppedImage): void {
    this.photo = croppendImage.croppendImage as SafeUrl;
    this.image = croppendImage.blob as Blob;
  }

  changePassword(request: ChangePasswordRequest): void {
    console.log(request);
    this.authService.changePassword(this.id, request).subscribe({
      next: () => {
        this.toastr.success('Password change successful!');
      },
      error: (error) => {
        this.toastr.error(
          'Change password failed. Please check your credentials.'
        );
        console.error('Login error:', error);
      },
    });
  }

  saveChanges(): void {
    this.fileUploadService.uploadFile(this.image as Blob).subscribe({
      next: (response: string) => {
        this.authService
          .updateInfos(this.id, { photo: response } as UserInfos)
          .subscribe({
            next: () => {
              this.toastr.success('Image uploaded successfully!');
              this.authService.getUserInfosFromServer();
            },
            error: (error) => {
              this.toastr.error('Image upload failed. Please try again.');
              console.error('Image upload error:', error);
            },
          });
      },
      error: (error) => {
        this.toastr.error('Image upload failed. Please try again.');
        console.error('Image upload error:', error);
      },
    });
  }
}
