import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Agence } from '../../../../core/models/agence';
import { Observable } from 'rxjs';
import { AgenceService } from '../../../../core/services/agence.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { AgenceCardComponent } from '../agence-card/agence-card.component';

@Component({
  selector: 'app-liste',
  imports: [CommonModule, AgenceCardComponent],
  templateUrl: './liste.component.html',
  styleUrl: './liste.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ListeComponent implements OnInit {
  agences$!: Observable<Agence[]>;

  constructor(private agenceService: AgenceService, private router: Router) {}

  ngOnInit(): void {
    this.agenceService.getAllAgenceFromServer();
    this.agences$ = this.agenceService.agences$;
    this.agences$.subscribe();
  }

  add(id: number): void {
    this.agenceService.getAgence(id);
    this.router.navigateByUrl('/agence/add');
  }

  edit(id: number): void {
    this.add(id);
  }

  view(id: number): void {
    this.agenceService.getAgence(id);
    this.router.navigateByUrl('/agence/view');
  }
}
