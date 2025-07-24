import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { EcheancierPretComponent } from '../../../composants/echeancier-pret/echeancier-pret.component';
import { combineLatest, map, Observable } from 'rxjs';
import { Credit } from '../../../../core/models/credit';
import { CreditService } from '../../../../core/services/credit.service';
import { EcheanceService } from '../../../../core/services/echeance.service';
import { Echeance } from '../../../../core/models/echeance';

@Component({
  selector: 'app-infos',
  imports: [CommonModule, EcheancierPretComponent],
  templateUrl: './infos.component.html',
  styleUrl: './infos.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class InfosComponent implements OnInit {
  echeancier$!: Observable<Echeance[]>;

  constructor(
    private creditService: CreditService,
    private echeanceService: EcheanceService
  ) {}

  ngOnInit(): void {
    this.echeancier$ = combineLatest([
      this.creditService.credit$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([credit, echeances]) => {
        return echeances.filter((e) => e.creditId === credit.id);
      })
    );
  }

  calculSoldeCapital(echeances: Echeance[]): number {
    return echeances
      .filter((e) => !e.estPaye)
      .reduce((acc, e) => acc + e.montantCapital, 0);
  }

  calculSoldeInterets(echeances: Echeance[]): number {
    return echeances
      .filter((e) => !e.estPaye)
      .reduce((acc, e) => acc + e.montantInterets, 0);
  }

  calculNombreEcheancesPayees(echeances: Echeance[]): number {
    return echeances.filter((e) => e.estPaye).reduce((acc, e) => acc + 1, 0);
  }

  calculNombreEcheancesNonPayees(echeances: Echeance[]): number {
    return echeances.filter((e) => !e.estPaye).reduce((acc, e) => acc + 1, 0);
  }
}
