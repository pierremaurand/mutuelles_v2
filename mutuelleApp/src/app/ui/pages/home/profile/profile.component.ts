import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserInfos } from '../../../../core/models/user-infos';
import { AsyncPipe, CommonModule } from '@angular/common';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { UtilisateurService } from '../../../../core/services/utilisateur.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-profile',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProfileComponent implements OnInit {
  utilisateur$!: Observable<UserInfos>;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;
  utilisateur!: UserInfos;

  constructor(
    private utilisateurService: UtilisateurService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'] as number;
    if (id) {
      this.utilisateurService.getUtilisateur(id);
    }
    this.utilisateur$ = this.utilisateurService.utilisateur$;
    this.utilisateur$.subscribe({
      next: (utilisateur: UserInfos) => {
        this.utilisateur = utilisateur;
      },
    });
  }

  onChangePassword(): void {
    this.router.navigateByUrl(
      '/home/profile/' + this.utilisateur.id + '/password'
    );
  }

  onChangeImage(): void {
    this.router.navigateByUrl(
      '/home/profile/' + this.utilisateur.id + '/image/' + this.utilisateur.id
    );
  }
}
