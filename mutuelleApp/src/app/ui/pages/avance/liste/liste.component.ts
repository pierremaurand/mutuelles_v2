import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Avance } from '../../../../core/models/avance';
import { combineLatest, map, Observable } from 'rxjs';
import { AvanceService } from '../../../../core/services/avance.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CardComponent } from '../card/card.component';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { SearchService } from '../../../../core/services/search.service';
import { Echeance } from '../../../../core/models/echeance';
import { EcheanceService } from '../../../../core/services/echeance.service';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, CardComponent],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  avances$!: Observable<Avance[]>;
  membres$!: Observable<Membre[]>;
  search$!: Observable<string>;
  membres: Membre[] = [];

  constructor(
    private avanceService: AvanceService,
    private searchService: SearchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.search$ = this.searchService.search$;
    this.search$.subscribe();

    this.avances$ = combineLatest([
      this.searchService.search$,
      this.avanceService.avances$,
    ]).pipe(
      map(([search, avances]) =>
        avances.filter((avance: Avance) =>
          avance.nomMembre.toLowerCase().includes(search)
        )
      )
    );
    this.avances$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/avance/add');
  }

  getMembreById(id: number): Membre {
    return this.membres.find((m) => m.id === id) || new Membre();
  }
}
