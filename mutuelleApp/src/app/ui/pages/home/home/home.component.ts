import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MembreService } from '../../../../core/services/membre.service';
import { CotisationService } from '../../../../core/services/cotisation.service';
import { AvanceService } from '../../../../core/services/avance.service';
import { CreditService } from '../../../../core/services/credit.service';
import { AgenceService } from '../../../../core/services/agence.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { Membre } from '../../../../core/models/membre';
import { Sexe } from '../../../../core/models/sexe';
import { Cotisation } from '../../../../core/models/cotisation';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
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

  constructor(
    private membreService: MembreService,
    private cotisationService: CotisationService,
    private avanceService: AvanceService,
    private creditService: CreditService,
    private agenceService: AgenceService
  ) {}
  ngOnInit(): void {
    this.initObservables();
  }

  initObservables(): void {
    this.membres$ = this.membreService.membres$;
    this.membres$.subscribe({
      next: (membres: Membre[]) => {
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
      },
    });

    this.cotisations$ = this.cotisationService.cotisations$;
    this.cotisations$.subscribe({
      next: (cotisations: Cotisation[]) => {
        cotisations.forEach((x) => (this.montantTotalCotisation += x.retenue));
        this.montant10PourCent = Math.round(this.montantTotalCotisation * 0.1);
        this.montant90PourCent = Math.round(this.montantTotalCotisation * 0.9);
      },
    });
  }
}
