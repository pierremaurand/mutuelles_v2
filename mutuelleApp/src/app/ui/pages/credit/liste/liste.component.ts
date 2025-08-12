import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Credit } from '../../../../core/models/credit';
import { combineLatest, map, Observable } from 'rxjs';
import { CreditService } from '../../../../core/services/credit.service';
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
  credits$!: Observable<Credit[]>;
  membres$!: Observable<Membre[]>;
  search$!: Observable<string>;
  membres: Membre[] = [];

  constructor(
    private creditService: CreditService,
    private membreService: MembreService,
    private searchService: SearchService,
    private echeanceService: EcheanceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.membres$ = this.membreService.membres$;
    this.membres$.subscribe({
      next: (membres) => {
        this.membres = membres;
      },
    });

    this.search$ = this.searchService.search$;
    this.search$.subscribe();

    this.credits$ = combineLatest([
      this.searchService.search$,
      this.creditService.credits$,
      this.membreService.membres$,
      this.echeanceService.echeances$,
    ]).pipe(
      map(([search, credits, membres, echeances]) =>
        credits.filter((credit: Credit) =>
          credit.nomMembre.toLowerCase().includes(search)
        )
      )
    );
    this.credits$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/credit/add');
  }

  getMembreById(id: number): Membre {
    return this.membres.find((m) => m.id === id) || new Membre();
  }
}
