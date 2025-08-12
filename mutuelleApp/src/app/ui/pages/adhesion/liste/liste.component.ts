import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Adhesion } from '../../../../core/models/adhesion';
import { combineLatest, map, Observable } from 'rxjs';
import { AdhesionService } from '../../../../core/services/adhesion.service';
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
  adhesions$!: Observable<Adhesion[]>;
  search$!: Observable<string>;
  date$!: Observable<string>;

  constructor(
    private adhesionService: AdhesionService,
    private searchService: SearchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.search$ = this.searchService.search$;
    this.search$.subscribe();

    this.date$ = this.searchService.date$;
    this.date$.subscribe();

    this.adhesions$ = combineLatest([
      this.search$,
      this.date$,
      this.adhesionService.adhesions$,
    ]).pipe(
      map(([search, date, adhesions]) =>
        adhesions.filter(
          (adhesion: Adhesion) =>
            adhesion.nomMembre.toLowerCase().includes(search) &&
            adhesion.dateAdhesion.includes(date)
        )
      )
    );

    this.adhesions$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/adhesion/add');
  }

  getMembreById(id: number): Membre {
    return this.adhesionService.getMembreById(id);
  }
}
