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
  credit$!: Observable<Credit>;
  echeancier$!: Observable<Echeance[]>;

  constructor(
    private creditService: CreditService,
    private echeanceService: EcheanceService
  ) {}

  ngOnInit(): void {
    this.credit$ = this.creditService.credit$;
    this.echeancier$ = combineLatest([
      this.creditService.credit$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([credit, echeances]) => {
        return echeances.filter((e) => e.creditId === credit.id);
      })
    );
  }
}
