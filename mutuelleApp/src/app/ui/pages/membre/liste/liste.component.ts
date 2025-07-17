import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { Observable } from 'rxjs';
import { MembreService } from '../../../../core/services/membre.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MembreCardComponent } from '../membre-card/membre-card.component';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, MembreCardComponent],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  membres$!: Observable<Membre[]>;

  constructor(private membreService: MembreService, private router: Router) {}

  ngOnInit(): void {
    this.membreService.getAllMembreFromServer();
    this.membres$ = this.membreService.membres$;
    this.membres$.subscribe();
  }

  add(id: number): void {
    this.membreService.getMembre(id);
    this.router.navigateByUrl('/membre/add');
  }

  edit(id: number): void {
    this.add(id);
  }

  view(id: number): void {
    this.membreService.getMembre(id);
    this.router.navigateByUrl('/membre/view');
  }
}
