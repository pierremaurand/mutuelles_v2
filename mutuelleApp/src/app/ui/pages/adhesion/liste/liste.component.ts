import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Adhesion } from '../../../../core/models/adhesion';
import { Observable } from 'rxjs';
import { AdhesionService } from '../../../../core/services/adhesion.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CardComponent } from '../card/card.component';
import { Membre } from '../../../../core/models/membre';
import { MembreService } from '../../../../core/services/membre.service';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, CardComponent],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  adhesions$!: Observable<Adhesion[]>;
  membres$!: Observable<Membre[]>;

  constructor(
    private adhesionService: AdhesionService,
    private membreService: MembreService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.adhesions$ = this.adhesionService.adhesions$;
    this.adhesions$.subscribe();
  }

  add(): void {
    this.router.navigateByUrl('/adhesion/add');
  }

  getMembreById(id: number): Membre {
    return this.adhesionService.getMembreById(id);
  }
}
