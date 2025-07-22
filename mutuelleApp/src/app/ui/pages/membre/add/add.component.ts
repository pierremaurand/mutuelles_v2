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
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { AgenceService } from '../../../../core/services/agence.service';
import { Agence } from '../../../../core/models/agence';
import { environment } from '../../../../../environments/environment';
import { Sexe } from '../../../../core/models/sexe';
import { MembreRequest } from '../../../../core/models/membre-request';

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
  photo: string = '';
  baseUrl: string = environment.imagesUrl;

  constructor(
    private router: Router,
    private membreService: MembreService,
    private agenceService: AgenceService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.initObservables();
  }

  initForm(): void {
    this.request = this.fb.group({
      id: [0],
      nom: ['', [Validators.required]],
      sexe: ['', [Validators.required]],
      dateNaissance: ['', [Validators.required]],
      lieuNaissance: ['', [Validators.required]],
      agenceId: ['', [Validators.required]],
      dateAdhesion: ['', [Validators.required]],
      telephone: ['', [Validators.required]],
      email: ['', [Validators.required]],
      photo: [''],
      estActif: [true],
    });
  }

  initObservables(): void {
    this.agences$ = this.agenceService.agences$;
    this.agences$.subscribe();
    this.membre$ = this.membreService.membre$;
    this.membre$.subscribe({
      next: (membre: Membre) => {
        this.photo = membre.photo;
        this.request.patchValue({
          id: membre.id as number,
          nom: membre.nom as string,
          sexe: membre.sexe as Sexe,
          dateNaissance: membre.dateNaissance as string,
          lieuNaissance: membre.lieuNaissance as string,
          agenceId: membre.agenceId as number,
          dateAdhesion: membre.dateAdhesion as string,
          telephone: membre.telephone as string,
          email: membre.email as string,
          photo: membre.photo as string,
          estActif: membre.estActif as boolean,
        });
      },
    });
  }

  submitForm(): void {
    if (this.request.valid) {
      this.membreService
        .addOrUpdate(
          this.request.controls['id'].value,
          this.request.value as MembreRequest
        )
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.membreService.getAllMembresFromServer();
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
