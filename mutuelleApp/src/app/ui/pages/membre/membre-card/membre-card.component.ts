import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { UpperCasePipe } from '@angular/common';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-membre-card',
  imports: [UpperCasePipe],
  templateUrl: './membre-card.component.html',
  styleUrl: './membre-card.component.scss',
})
export class MembreCardComponent {
  @Input()
  membre!: Membre;
  @Output()
  edit = new EventEmitter<number>();
  @Output()
  view = new EventEmitter<number>();
  baseUrl: string = environment.imagesUrl;

  onEdit(): void {
    this.edit.emit(this.membre.id);
  }

  onView(): void {
    this.view.emit(this.membre.id);
  }
}
