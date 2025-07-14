import { UtilisateurService } from './../../../../core/services/utilisateur.service';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserInfos } from '../../../../core/models/user-infos';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  NgForm,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
import { UpdateUtilisateurRequest } from '../../../../core/models/update-utilisateur-request';
import { Observable } from 'rxjs';
import { Sexe } from '../../../../core/models/sexe';
import { Role } from '../../../../core/models/role';

@Component({
  selector: 'app-add',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class AddComponent implements OnInit {
  request!: FormGroup;
  id!: number;
  utilisateur$!: Observable<UserInfos>;

  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private utilisateurService: UtilisateurService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    const id = this.route.snapshot.params['id'];
    this.utilisateur$ = this.utilisateurService.utilisateur$;
    this.utilisateurService.getUtilisateur(id);
    this.utilisateur$.subscribe({
      next: (utilisateur: UserInfos) => {
        this.id = utilisateur.id as number;
        this.request.patchValue({
          login: utilisateur.login as string,
          nom: utilisateur.nom as string,
          sexe: utilisateur.sexe as Sexe,
          role: utilisateur.role as Role,
        });
        if (utilisateur.photo) {
          this.photo = this.baseUrl + '/' + utilisateur.photo;
        }
      },
    });
  }

  initForm(): void {
    this.request = this.fb.group({
      login: ['', [Validators.required]],
      nom: ['', [Validators.required]],
      sexe: ['', [Validators.required]],
      role: ['', [Validators.required]],
    });
  }

  submitForm(): void {
    if (this.request.valid) {
      this.utilisateurService
        .addOrUpdateUser(this.id, this.request.value)
        .subscribe({
          next: () => {
            this.toastr.success(
              "L'enregistrement des informations de l'utilisateur a réussie!"
            );
            this.utilisateurService.getAllUtilisateurFromServer();
            this.router.navigateByUrl('/utilisateur');
          },
          error: (error) => {
            this.toastr.error(
              "L'enregistrement des informations de l'utilisateur a échoué!",
              error.message
            );
            console.log(error.message);
          },
        });
    } else {
      this.toastr.error('Tous les champs doivent être renseignés.');
    }
  }

  onBack(): void {
    this.router.navigateByUrl('/utilisateur');
  }
}
