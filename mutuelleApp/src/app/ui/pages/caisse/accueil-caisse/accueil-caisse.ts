import {
  ChangeDetectionStrategy,
  Component,
  inject,
  Input,
} from '@angular/core';
import { combineLatest, map, Observable, tap } from 'rxjs';
import { Caisse } from '../../../../core/models/caisse';
import { Mouvement } from '../../../../core/models/mouvement';
import { CaisseService } from '../../../../core/services/caisse.service';
import { MouvementService } from '../../../../core/services/mouvement.service';
import { AsyncPipe, UpperCasePipe } from '@angular/common';
import { LigneMouvement } from '../ligne-mouvement/ligne-mouvement';
import { CardMouvement } from '../card-mouvement/card-mouvement';

@Component({
  selector: 'app-accueil-caisse',
  imports: [AsyncPipe, UpperCasePipe, LigneMouvement, CardMouvement],
  templateUrl: './accueil-caisse.html',
  styleUrl: './accueil-caisse.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class AccueilCaisse {
  private caisseService = inject(CaisseService);
  private mouvementService = inject(MouvementService);
  caisse$!: Observable<Caisse>;
  mouvements$!: Observable<Mouvement[]>;
  selectedMouvement!: Mouvement;

  @Input()
  set id(value: number) {
    this.caisse$ = this.caisseService.caisse$;
    this.caisseService.getCaisseFromServer(value);
    this.mouvements$ = combineLatest([
      this.caisse$,
      this.mouvementService.mouvements$,
    ]).pipe(
      map(([caisse, mouvements]) =>
        mouvements
          .filter(
            (mouvement) =>
              mouvement.caisseId === +value &&
              mouvement.dateMouvement.includes(caisse.dateCaisse ?? '')
          )
          .sort((a, b) => (a.id < b.id ? 1 : -1))
      )
    );
    this.mouvementService.getAllMouvementsFromServer();
  }

  onSelected(mouvement: Mouvement): void {
    this.selectedMouvement = mouvement;
  }
}
