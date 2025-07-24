import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { EcheancierPretComponent } from '../../../composants/echeancier-pret/echeancier-pret.component';
import { combineLatest, map, Observable } from 'rxjs';
import { Avance } from '../../../../core/models/avance';
import { AvanceService } from '../../../../core/services/avance.service';
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
  avance$!: Observable<Avance>;
  echeancier$!: Observable<Echeance[]>;

  constructor(
    private avanceService: AvanceService,
    private echeanceService: EcheanceService
  ) {}

  ngOnInit(): void {
    this.echeancier$ = combineLatest([
      this.avanceService.avance$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([avance, echeances]) => {
        return echeances.filter((e) => e.avanceId === avance.id);
      })
    );
  }

  calculSoldeAvance(echeances: Echeance[]): number {
    return echeances
      .filter((e) => !e.estPaye)
      .reduce((acc, e) => acc + e.montantCapital, 0);
  }

  calculNombreEcheancesPayees(echeances: Echeance[]): number {
    return echeances.filter((e) => e.estPaye).reduce((acc, e) => acc + 1, 0);
  }

  calculNombreEcheancesNonPayees(echeances: Echeance[]): number {
    return echeances.filter((e) => !e.estPaye).reduce((acc, e) => acc + 1, 0);
  }
}
