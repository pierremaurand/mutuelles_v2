import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Agence } from '../../../../core/models/agence';
import { AgenceService } from '../../../../core/services/agence.service';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-view',
  imports: [AsyncPipe, CommonModule],
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ViewComponent implements OnInit {
  agence$!: Observable<Agence>;
  id!: number;

  constructor(private agenceService: AgenceService) {
    // This constructor is intentionally left empty.
  }

  ngOnInit(): void {
    this.agence$ = this.agenceService.agence$;
    this.agence$.subscribe();
  }
}
