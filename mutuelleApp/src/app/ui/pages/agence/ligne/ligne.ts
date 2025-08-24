import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Agence } from '../../../../core/models/agence';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-ligne',
  imports: [UpperCasePipe],
  templateUrl: './ligne.html',
  styleUrl: './ligne.scss',
})
export class Ligne {
  @Input()
  agence!: Agence;
  @Output()
  selectedEvent = new EventEmitter<Agence>();

  onSelected(): void {
    this.selectedEvent.emit(this.agence);
  }
}
