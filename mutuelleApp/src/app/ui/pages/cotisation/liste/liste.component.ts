import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Cotisation } from '../../../../core/models/cotisation';
import { combineLatest, map, Observable } from 'rxjs';
import { CotisationService } from '../../../../core/services/cotisation.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CardComponent } from '../card/card.component';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';
import { SearchService } from '../../../../core/services/search.service';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, CardComponent],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  cotisations$!: Observable<Cotisation[]>;
  membres$!: Observable<Membre[]>;
  search$!: Observable<string>;

  constructor(
    private cotisationService: CotisationService,
    private membreService: MembreService,
    private searchService: SearchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.membres$ = this.membreService.membres$;
    this.membres$.subscribe({
      next: (membres) => {
        this.cotisationService.setMembres(membres);
      },
    });

    this.search$ = this.searchService.search$;
    this.search$.subscribe();

    this.cotisations$ = combineLatest([
      this.searchService.search$,
      this.cotisationService.cotisations$,
      this.membreService.membres$,
    ]).pipe(
      map(([search, cotisations, membres]) =>
        cotisations.filter((cotisation: Cotisation) =>
          membres.find(
            (m) =>
              m.id === cotisation.membreId &&
              m.nom.toLowerCase().includes(search)
          )
        )
      )
    );
    this.cotisations$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/cotisation/add');
  }

  getMembreById(id: number): Membre {
    return this.cotisationService.getMembreById(id);
  }
}
