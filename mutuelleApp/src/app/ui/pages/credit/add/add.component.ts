import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { InfosPretComponent } from '../../../composants/infos-pret/infos-pret.component';
import { EcheancierPretComponent } from '../../../composants/echeancier-pret/echeancier-pret.component';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { CreditService } from '../../../../core/services/credit.service';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { combineLatest, map, Observable, startWith, tap } from 'rxjs';
import { Agence } from '../../../../core/models/agence';
import { AgenceService } from '../../../../core/services/agence.service';
import { Credit } from '../../../../core/models/credit';
import { CommonModule, DatePipe } from '@angular/common';
import { Echeance } from '../../../../core/models/echeance';
import { Router } from '@angular/router';
import { CreditRequest } from '../../../../core/models/credit-request';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add',
  imports: [
    InfosPretComponent,
    EcheancierPretComponent,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './add.component.html',
  styleUrl: './add.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class AddComponent implements OnInit {
  request!: FormGroup;
  id: number = 0;
  membreIdCtrl!: FormControl;
  montantCapitalCtrl!: FormControl;
  montantCommissionCtrl!: FormControl;
  montantInteretsCtrl!: FormControl;
  dureeCtrl!: FormControl;
  dateDemandeCtrl!: FormControl;
  dateDecaissementCtrl!: FormControl;
  differeCtrl!: FormControl;

  agenceCtrl!: FormControl;

  membres$!: Observable<Membre[]>;
  membre$!: Observable<Membre>;
  agences$!: Observable<Agence[]>;
  credit$!: Observable<Credit>;

  echeancier: Echeance[] = [];

  membre!: Membre;
  montant!: number;
  interets!: number;
  duree!: number;
  commission!: number;

  constructor(
    private membreService: MembreService,
    private agenceService: AgenceService,
    private creditService: CreditService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initControls();
    this.initForm();
    this.initObservables();
  }

  initControls(): void {
    this.differeCtrl = this.fb.control(0, [
      Validators.min(0),
      Validators.max(11),
    ]);
    this.membreIdCtrl = this.fb.control('', Validators.required);
    this.montantCapitalCtrl = this.fb.control('', [
      Validators.required,
      Validators.min(0),
    ]);
    this.montantCommissionCtrl = this.fb.control('', Validators.required);
    this.montantInteretsCtrl = this.fb.control('', Validators.required);
    this.dureeCtrl = this.fb.control('', [
      Validators.required,
      Validators.min(1),
      Validators.max(24),
    ]); // Duree in months
    this.dateDemandeCtrl = this.fb.control('', Validators.required);
    this.dateDecaissementCtrl = this.fb.control('', Validators.required);

    this.agenceCtrl = this.fb.control('', Validators.required);
  }

  initForm(): void {
    this.request = this.fb.group({
      membreId: this.membreIdCtrl,
      montantCapital: this.montantCapitalCtrl,
      montantCommission: this.montantCommissionCtrl,
      montantInterets: this.montantInteretsCtrl,
      duree: this.dureeCtrl,
      dateDemande: this.dateDemandeCtrl,
      dateDecaissement: this.dateDecaissementCtrl,
    });
  }

  initObservables(): void {
    this.agences$ = this.agenceService.agences$;
    this.credit$ = this.creditService.credit$;

    const membreId$ = this.membreIdCtrl.valueChanges.pipe(
      startWith(this.agenceCtrl.value)
    );

    const agence$ = this.agenceCtrl.valueChanges.pipe(
      startWith(this.agenceCtrl.value)
    );

    this.montantCapitalCtrl.valueChanges.subscribe({
      next: (value: number) => {
        const commission = (value * 1) / 100; // 1% commission
        this.montantCommissionCtrl.setValue(
          commission < 1000 ? 1000 : commission
        );
      },
    });

    this.dureeCtrl.valueChanges.subscribe({
      next: (value: number) => {
        const montant = this.montantCapitalCtrl.value;
        const interets = Math.round((montant * value * 2) / 1200); // 2% interest rate
        this.montantInteretsCtrl.setValue(interets);
        this.genererEcheancier();
      },
    });

    this.dateDecaissementCtrl.valueChanges.subscribe({
      next: (value: number) => {
        this.genererEcheancier();
        console.log(this.echeancier);
      },
    });

    this.membres$ = combineLatest([agence$, this.membreService.membres$]).pipe(
      map(([agence, membres]) =>
        membres.filter(
          (membre: Membre) => membre.agenceId === +agence || agence === ''
        )
      )
    );

    this.membre$ = combineLatest([membreId$, this.membres$]).pipe(
      map(
        ([membreId, membres]) =>
          membres.filter((membre: Membre) => membre.id === +membreId)[0] ||
          new Membre()
      )
    );

    this.membres$.subscribe();
    this.membre$.subscribe();
  }

  genererEcheancier(): void {
    let dateDebut = new Date();
    let curDate = new Date();
    let capital: number | undefined = 0;
    let interet: number | undefined = 0;
    let commission: number | undefined = 0;
    let montantCapital: number | undefined = 0;
    let montantInteret: number | undefined = 0;
    let montantCommission: number | undefined = 0;
    let nbrEcheances: number | undefined = 0;
    let resteCapital: number | undefined = 0;
    let resteInteret: number | undefined = 0;
    let resteCommission: number | undefined = 0;
    let differe: number = 0;

    differe = this.differeCtrl.value;

    dateDebut = new Date(this.dateDecaissementCtrl.value);
    if (dateDebut.getDate() < 10) {
      dateDebut.setMonth(dateDebut.getMonth() - 1);
    }
    dateDebut.setDate(25);
    nbrEcheances = this.dureeCtrl.value;
    montantCapital = this.montantCapitalCtrl.value;
    montantCommission = this.montantCommissionCtrl.value;
    montantInteret = this.montantInteretsCtrl.value;

    if (montantCapital && montantCommission && montantInteret && nbrEcheances) {
      capital = Math.round(montantCapital / nbrEcheances);
      resteCapital = montantCapital - capital * nbrEcheances;
      interet = Math.round(montantInteret / nbrEcheances);
      resteInteret = montantInteret - interet * nbrEcheances;
      commission = Math.round(montantCommission / nbrEcheances);
      resteCommission = montantCommission - commission * nbrEcheances;
    }

    if (this.dureeCtrl.valid && this.dateDecaissementCtrl.valid) {
      this.echeancier = [];
      curDate = dateDebut;
      if (nbrEcheances) {
        for (let i = 1; i <= nbrEcheances; i++) {
          if (curDate.getMonth() == 11) {
            curDate.setFullYear(curDate.getFullYear() + 1);
            curDate.setMonth(differe);
          } else {
            curDate.setMonth(curDate.getMonth() + differe + 1);
          }
          let echeance: Echeance = new Echeance();
          echeance.dateEcheance =
            this.datePipe.transform(curDate, 'yyyy-MM-dd') ?? '';
          echeance.montantCapital = capital;
          echeance.montantInterets = interet;
          echeance.montantCommission = commission;
          if (i === 1) {
            echeance.montantCapital = capital + resteCapital;
            echeance.montantInterets = interet + resteInteret;
            echeance.montantCommission = commission + resteCommission;
          }
          this.echeancier.push(echeance);
        }
      }
    }
  }

  onCancel(): void {
    this.router.navigateByUrl('/credit');
  }

  submitForm(): void {
    if (this.request.valid) {
      this.creditService
        .add(this.request.value as CreditRequest, this.echeancier as Echeance[])
        .subscribe({
          next: () => {
            this.toastr.success("L'enregistrement a réussie!", 'Succès');
            this.onCancel();
          },
          error: (error) => {
            console.log(error);
            if (error.status === 400) {
              this.toastr.error(error.error, 'Erreur de validation');
            } else {
              this.toastr.error('Une erreur est survenue!', 'Erreur');
            }
          },
        });
    } else {
      this.toastr.error('Tous les champs doivent être renseignés.');
    }
  }
}
