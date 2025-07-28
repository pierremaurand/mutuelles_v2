import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { combineLatest, map, Observable, startWith } from 'rxjs';
import { CotisationService } from '../../../../core/services/cotisation.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { CotisationRequest } from '../../../../core/models/cotisation-request';
import { AgenceService } from '../../../../core/services/agence.service';
import { Agence } from '../../../../core/models/agence';

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
  agences$!: Observable<Agence[]>;

  dateCotisationCtrl!: FormControl;
  agenceCtrl!: FormControl;

  constructor(
    private router: Router,
    private cotisationService: CotisationService,
    private membreService: MembreService,
    private agenceService: AgenceService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initControls();
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

  initControls(): void {
    this.dateCotisationCtrl = this.fb.control('');
    this.agenceCtrl = this.fb.control('');
  }

  initObservables(): void {
    this.agences$ = this.agenceService.agences$;
    this.agences$.subscribe();

    const agence$ = this.agenceCtrl.valueChanges.pipe(
      startWith(this.agenceCtrl.value)
    );

    const dateCotisation$ = this.dateCotisationCtrl.valueChanges.pipe(
      startWith(this.dateCotisationCtrl.value)
    );

    this.membres$ = combineLatest([
      agence$,
      dateCotisation$,
      this.cotisationService.cotisations$,
      this.membreService.membres$,
    ]).pipe(
      map(([agence, dateCotisation, cotisations, membres]) =>
        membres.filter(
          (membre: Membre) =>
            !cotisations.find(
              (a) =>
                a.membreId === membre.id &&
                (a.dateCotisation.includes(dateCotisation) ||
                  dateCotisation === '')
            ) &&
            (membre.dateAdhesion <= dateCotisation || dateCotisation === '') &&
            (membre.agenceId === +agence || agence === '')
        )
      )
    );

    this.membres$.subscribe({
      next: (membres: Membre[]) => {
        this.initForm();
        membres.forEach((membre: Membre) => {
          this.addCotisation(membre);
        });
      },
    });
  }

  addCotisation(membre: Membre) {
    var cotisationForm = this.fb.group({
      membreId: [membre.id, [Validators.required]],
      dateCotisation: this.dateCotisationCtrl,
      salaire: ['', [Validators.required]],
    });
    this.lines.push(cotisationForm);
  }

  submitForm(): void {
    if (this.request.valid) {
      this.cotisationService
        .add(this.lines.value as CotisationRequest[])
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.cotisationService.getAllCotisationsFromServer();
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
    this.router.navigateByUrl('/cotisation');
  }

  private afficheErreur(error: any): string {
    if (error.errors) {
      return error.errors[0] || 'Une erreur est survenue!';
    } else {
      return 'Une erreur est survenue!';
    }
  }
}
