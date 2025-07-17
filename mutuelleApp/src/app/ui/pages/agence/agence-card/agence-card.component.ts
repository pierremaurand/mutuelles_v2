import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Agence } from '../../../../core/models/agence';
import { UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-agence-card',
  imports: [UpperCasePipe],
  templateUrl: './agence-card.component.html',
  styleUrl: './agence-card.component.scss',
})
export class AgenceCardComponent {
  @Input()
  agence!: Agence;
  @Output()
  edit = new EventEmitter<number>();
  @Output()
  view = new EventEmitter<number>();

  onEdit(): void {
    this.edit.emit(this.agence.id);
  }

  onView(): void {
    this.view.emit(this.agence.id);
  }
}
