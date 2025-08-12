import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Echeance } from '../../../core/models/echeance';
import { environment } from '../../../../environments/environment';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-ligne-echeance',
  imports: [UpperCasePipe],
  templateUrl: './ligne-echeance.html',
  styleUrl: './ligne-echeance.scss',
})
export class LigneEcheance {
  @Input()
  item!: { i: number; checked: boolean; echeance: Echeance };
  @Output()
  onCheckedEvent = new EventEmitter<{ i: number; echeance: Echeance }>();
  baseUrl: string = environment.imagesUrl;

  checked: boolean = false;

  onCheck($event: any) {
    this.checked = $event.target.checked;
    this.onCheckedEvent.emit(this.item);
  }
}
