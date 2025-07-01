import { AsyncPipe, CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TokenService } from '../../../core/token/token.service';
import { Observable, tap } from 'rxjs';
import { UserInfos } from '../../../core/models/user-infos';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-side-bar',
  imports: [CommonModule, RouterLink, RouterLinkActive, AsyncPipe],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SideBarComponent implements OnInit {
  userInfos$!: Observable<UserInfos>;
  photo: string = './assets/images/default_man.jpg';

  constructor(
    private tokenService: TokenService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.getUserInfosFromServer();
    this.userInfos$ = this.authService.userInfos$.pipe(
      tap((infos) => {
        if (infos.photo) {
          this.photo = infos.photo;
        }
      })
    );
  }

  logout(): void {
    this.tokenService.logout();
  }
}
