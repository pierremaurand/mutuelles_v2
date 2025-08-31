import { Component, inject, Input } from '@angular/core';
import { MouvementService } from '../../../core/services/mouvement.service';
import { Observable } from 'rxjs';
import { Mouvement } from '../../../core/models/mouvement';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-print-mouvement',
  imports: [AsyncPipe],
  templateUrl: './print-mouvement.html',
  styleUrl: './print-mouvement.scss',
})
export default class PrintMouvement {
  private mouvementService = inject(MouvementService);
  mouvement$!: Observable<Mouvement>;

  @Input()
  set id(value: number) {
    this.mouvement$ = this.mouvementService.mouvement$;
    this.mouvementService.getMouvementFromServer(+value);
  }
}
