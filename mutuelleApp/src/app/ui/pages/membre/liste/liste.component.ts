import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { combineLatest, map, Observable } from 'rxjs';
import { MembreService } from '../../../../core/services/membre.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MembreCardComponent } from '../membre-card/membre-card.component';
import { SearchService } from '../../../../core/services/search.service';
import { Ligne } from '../ligne/ligne';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, MembreCardComponent, Ligne],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  membres$!: Observable<Membre[]>;
  search$!: Observable<string>;
  date$!: Observable<string>;
  selectedMembre!: Membre;

  constructor(
    private membreService: MembreService,
    private searchService: SearchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.search$ = this.searchService.search$;
    this.search$.subscribe();
    this.date$ = this.searchService.date$;
    this.date$.subscribe();

    this.membres$ = combineLatest([
      this.search$,
      this.date$,
      this.membreService.membres$,
    ]).pipe(
      map(([search, date, membres]) =>
        membres.filter(
          (membre: Membre) =>
            membre.nom.toLowerCase().includes(search) &&
            membre.dateAdhesion.includes(date)
        )
      )
    );

    this.membres$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/membre/add/0');
  }

  onSelected(membre: Membre): void {
    this.selectedMembre = membre;
  }
}
