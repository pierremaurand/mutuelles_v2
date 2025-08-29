import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CaisseService } from '../../../../core/services/caisse.service';
import { Observable } from 'rxjs';
import { Caisse } from '../../../../core/models/caisse';
import { Widget } from '../../../composants/widget/widget';
import { Menu } from '../menu/menu';

@Component({
  selector: 'app-view-caisse',
  imports: [CommonModule, RouterOutlet, Widget, AsyncPipe, DatePipe, Menu],
  templateUrl: './view-caisse.html',
  styleUrl: './view-caisse.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ViewCaisse implements OnInit {
  caisse$!: Observable<Caisse>;
  labelSolde: string = 'Solde caisse';

  constructor(private caisseService: CaisseService) {}

  ngOnInit(): void {
    this.initObservables();
  }

  initObservables(): void {
    this.caisse$ = this.caisseService.caisse$;
  }
}
