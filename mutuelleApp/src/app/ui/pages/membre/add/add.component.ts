import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Observable } from 'rxjs';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { AgenceService } from '../../../../core/services/agence.service';
import { Agence } from '../../../../core/models/agence';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { Sexe } from '../../../../core/models/sexe';

@Component({
  selector: 'app-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class AddComponent implements OnInit {
  request!: FormGroup;
  membre$!: Observable<Membre>;
  agences$!: Observable<Agence[]>;
  id!: number;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private membreService: MembreService,
    private agenceService: AgenceService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.membre$ = this.membreService.membre$;
      this.membre$.subscribe({
        next: (membre: Membre) => {
          this.id = membre.id as number;
          this.request.patchValue({
            nom: membre.nom as string,
            sexe: membre.sexe as Sexe,
            dateNaissance: membre.dateNaissance as string,
            lieuNaissance: membre.lieuNaissance as string,
            agenceId: membre.agenceId as number,
            dateAdhesion: membre.dateAdhesion as string,
            telephone: membre.telephone as string,
            email: membre.email as string,
          });
          if (membre.photo) {
            this.photo = this.baseUrl + '/' + membre.photo;
          }
        },
      });
    }
  }

  initForm(): void {
    this.agences$ = this.agenceService.agences$;
    this.agences$.subscribe();
    this.agenceService.getAllAgenceFromServer();

    this.request = this.fb.group({
      nom: ['', [Validators.required]],
      sexe: ['', [Validators.required]],
      dateNaissance: ['', [Validators.required]],
      lieuNaissance: ['', [Validators.required]],
      agenceId: ['', [Validators.required]],
      dateAdhesion: ['', [Validators.required]],
      telephone: ['', [Validators.required]],
      email: ['', [Validators.required]],
    });
  }

  submitForm(): void {
    if (this.request.valid) {
      this.membreService.addOrUpdate(this.id, this.request.value).subscribe({
        next: () => {
          this.toastr.success("L'enregistrement a réussie!", 'Succès');
          this.membreService.getAllMembreFromServer();
          this.onCancel();
        },
        error: (error) => {
          console.log(error);
          if (error.status === 400) {
            this.toastr.error(
              this.afficheErreur(error.error),
              'Erreur de validation'
            );
          } else {
            this.toastr.error('Une erreur est survenue!', 'Erreur');
          }
        },
      });
    } else {
      this.toastr.error('Tous les champs doivent être renseignés.');
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/membre');
  }

  private afficheErreur(error: any): string {
    console.log(error.errors[0]);
    if (error.errors) {
      return error.errors[0] || 'Une erreur est survenue!';
    } else {
      return 'Une erreur est survenue!';
    }
  }
}
