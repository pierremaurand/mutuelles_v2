import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { combineLatest, map, Observable } from 'rxjs';
import { AdhesionService } from '../../../../core/services/adhesion.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { AdhesionRequest } from '../../../../core/models/adhesion-request';

@Component({
  selector: 'app-add',
  imports: [ReactiveFormsModule, CommonModule, AsyncPipe],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class AddComponent implements OnInit {
  request!: FormGroup;
  membres$!: Observable<Membre[]>;

  constructor(
    private router: Router,
    private adhesionService: AdhesionService,
    private membreService: MembreService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.initObservables();
  }

  initForm(): void {
    this.request = this.fb.group({
      lines: this.fb.array([]),
    });
  }

  get lines(): FormArray {
    return this.request.get('lines') as FormArray;
  }

  initObservables(): void {
    this.membres$ = combineLatest([
      this.adhesionService.adhesions$,
      this.membreService.membres$,
    ]).pipe(
      map(([adhesions, membres]) =>
        membres.filter(
          (membre: Membre) => !adhesions.find((a) => a.membreId === membre.id)
        )
      )
    );

    this.membres$.subscribe({
      next: (membres: Membre[]) => {
        membres.forEach((membre: Membre) => {
          this.addAdhesion(membre);
        });
      },
    });
  }

  addAdhesion(membre: Membre) {
    var adhesionForm = this.fb.group({
      membreId: [membre.id, [Validators.required]],
      montant: ['', [Validators.required]],
    });
    this.lines.push(adhesionForm);
  }

  submitForm(): void {
    if (this.request.valid) {
      this.adhesionService
        .add(this.lines.value as AdhesionRequest[])
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.adhesionService.getAllAdhesionsFromServer();
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
    this.router.navigateByUrl('/adhesion');
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
