import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MembreService } from '../../../../core/services/membre.service';
import { CotisationService } from '../../../../core/services/cotisation.service';
import { AvanceService } from '../../../../core/services/avance.service';
import { CreditService } from '../../../../core/services/credit.service';
import { CommonModule } from '@angular/common';
import { Observable, tap } from 'rxjs';
import { Membre } from '../../../../core/models/membre';
import { Sexe } from '../../../../core/models/sexe';
import { Cotisation } from '../../../../core/models/cotisation';
import { Avance } from '../../../../core/models/avance';
import { Credit } from '../../../../core/models/credit';
import { LineChart } from '../../../composants/line-chart/line-chart';
import { PieChart } from '../../../composants/pie-chart/pie-chart';

@Component({
  selector: 'app-home',
  imports: [CommonModule, LineChart, PieChart],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class HomeComponent implements OnInit {
  membres$!: Observable<Membre[]>;
  totalMembre: number = 0;
  totalMembreActifs: number = 0;
  totalMembreFemme: number = 0;
  totalMembreFemmeActifs: number = 0;
  totalMembreHomme: number = 0;
  totalMembreHommeActifs: number = 0;

  cotisations$!: Observable<Cotisation[]>;
  montantTotalCotisation: number = 0;
  montant90PourCent: number = 0;
  montant10PourCent: number = 0;

  avances$!: Observable<Avance[]>;
  montantAvanceDecaisse: number = 0;
  montantAvanceRembourse: number = 0;
  encoursAvance: number = 0;

  credits$!: Observable<Credit[]>;
  montantCreditDecaisse: number = 0;
  montantCreditRembourse: number = 0;
  encoursCredit: number = 0;

  encoursCotisationData = {
    label: 'Encours cotisations',
    data: [
      0, 10000, 5000, 15000, 20000, 10000, 30000, 35000, 40000, 45000, 50000,
      10000,
    ],
    borderColor: '#FF5733',
    backgroundColor: '#FF5733',
    fill: false,
    tension: 0.1,
    pointRadius: 5,
    pointHoverRadius: 7,
  };

  encoursAvanceData = {
    label: 'Encours avances',
    data: [
      0, 10000, 5000, 15000, 1000, 25000, 30000, 35000, 40000, 25000, 50000,
      55000,
    ],
    borderColor: '#33FF57',
    backgroundColor: '#33FF57',
    fill: false,
    tension: 0.1,
    pointRadius: 5,
    pointHoverRadius: 7,
  };

  encoursCreditData = {
    label: 'Encours crédits',
    data: [
      0, 5000, 10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000,
      55000,
    ],
    borderColor: '#3357FF',
    backgroundColor: '#3357FF',
    fill: false,
    tension: 0.1,
    pointRadius: 5,
    pointHoverRadius: 7,
  };

  dataGraph2: any = {
    labels: ['Homme', 'Femme'],
    datasets: [
      {
        data: [300, 500],
      },
    ],
  };

  dataGraph3: any = {};

  dataGraph4: any = {};

  dataGraph1: any = {
    labels: [
      'Janvier',
      'Février',
      'Mars',
      'Avril',
      'Mai',
      'Juin',
      'Juillet',
      'Août',
      'Septembre',
      'Octobre',
      'Novembre',
      'Décembre',
    ],
    datasets: [
      this.encoursCotisationData,
      this.encoursAvanceData,
      this.encoursCreditData,
    ],
  };

  constructor(
    private membreService: MembreService,
    private cotisationService: CotisationService,
    private avanceService: AvanceService,
    private creditService: CreditService
  ) {}

  ngOnInit(): void {
    this.initObservables();
  }

  initObservables(): void {
    this.membreService.getAllMembresFromServer();
    this.cotisationService.getAllCotisationsFromServer();
    this.avanceService.getAllAvancesFromServer();
    this.creditService.getAllCreditsFromServer();

    this.membres$ = this.membreService.membres$;
    this.membres$
      .pipe(
        tap((membres: Membre[]) => {
          this.totalMembre = membres.length;
          this.totalMembreActifs = membres.filter((x) => x.estActif).length;
          this.totalMembreHomme = membres.filter(
            (x) => x.sexe === Sexe.Masculin
          ).length;
          this.totalMembreHommeActifs = membres.filter(
            (x) => x.estActif && x.sexe === Sexe.Masculin
          ).length;
          this.totalMembreFemme = membres.filter(
            (x) => x.sexe === Sexe.Feminin
          ).length;
          this.totalMembreFemmeActifs = membres.filter(
            (x) => x.estActif && x.sexe === Sexe.Feminin
          ).length;
        })
      )
      .subscribe();

    this.cotisations$ = this.cotisationService.cotisations$;
    this.cotisations$
      .pipe(
        tap((cotisations: Cotisation[]) => {
          cotisations.forEach(
            (x) => (this.montantTotalCotisation += x.retenue)
          );
          this.montant10PourCent = Math.round(
            this.montantTotalCotisation * 0.1
          );
          this.montant90PourCent = Math.round(
            this.montantTotalCotisation * 0.9
          );
        })
      )
      .subscribe();

    this.avances$ = this.avanceService.avances$;
    this.avances$
      .pipe(
        tap((avances: Avance[]) => {
          avances.forEach((x) => {
            this.montantAvanceDecaisse += x.montantCapital;
            this.montantAvanceRembourse += x.montantCapital - x.montantRestant;
          });
          this.encoursAvance =
            this.montantAvanceDecaisse - this.montantAvanceRembourse;
        })
      )
      .subscribe();

    this.credits$ = this.creditService.credits$;
    this.credits$
      .pipe(
        tap((credits: Credit[]) => {
          credits.forEach((x) => {
            this.montantCreditDecaisse += x.montantCapital;
            this.montantCreditRembourse += x.montantCapital - x.montantRestant;
          });
          this.encoursCredit =
            this.montantCreditDecaisse - this.montantCreditRembourse;
        })
      )
      .subscribe();
  }
}
