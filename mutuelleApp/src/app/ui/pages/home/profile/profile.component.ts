import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { UserInfos } from '../../../../core/models/user-infos';
import { AuthService } from '../../../../core/services/auth.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { SafeUrl } from '@angular/platform-browser';
import { ChangePasswordRequest } from '../../../../core/models/change-password-request';
import { ToastrService } from 'ngx-toastr';
import { UploadImage } from '../../../../core/models/upload-image';
import { FileUploadService } from '../../../../core/services/file-upload.service';
import { CroppedImage } from '../../../../core/models/cropped-image';
import { UploadResponse } from '../../../../core/models/upload-response';
import { environment } from '../../../../../environments/environment';
import { UtilisateurService } from '../../../../core/services/utilisateur.service';
import { UpdatePhotoRequest } from '../../../../core/models/update-photo-request';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [RouterOutlet, CommonModule, AsyncPipe],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProfileComponent implements OnInit {
  userInfos$!: Observable<UserInfos>;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;
  blob!: Blob | undefined;
  uploadImage!: UploadImage;
  utilisateur!: UserInfos;
  updatePhotoRequest!: UpdatePhotoRequest;

  constructor(
    private authService: AuthService,
    private utilisateurService: UtilisateurService,
    private fileUploadService: FileUploadService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.userInfos$ = this.authService.userInfos$.pipe(
      tap((infos: UserInfos) => {
        if (infos.id) {
          this.utilisateurService.getUtilisateur(infos.id);
        }

        this.utilisateur = infos;
        this.blob = undefined;
      })
    );
    this.userInfos$.subscribe();
  }

  onChangePassword(): void {
    this.router.navigateByUrl('/home/profile/password');
  }

  onChangeImage(): void {
    this.router.navigateByUrl('/home/profile/image');
  }

  photoChange(croppendImage: CroppedImage): void {
    this.photo = croppendImage.croppendImage as SafeUrl;
    this.blob = croppendImage.blob as Blob;
  }
}
