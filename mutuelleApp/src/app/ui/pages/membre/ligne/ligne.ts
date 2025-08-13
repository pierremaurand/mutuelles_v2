import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-ligne',
  imports: [CommonModule],
  templateUrl: './ligne.html',
  styleUrl: './ligne.scss',
})
export class Ligne {
  @Input()
  membre!: Membre;
  @Output()
  selectedEvent = new EventEmitter<Membre>();
  baseUrl: string = environment.imagesUrl;

  onSelected(): void {
    this.selectedEvent.emit(this.membre);
  }

  get photo(): string {
    return this.membre.sexe ? './assets/images/default_man.jpg' : '';
  }
}
