import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import {
  DatePipe,
  DecimalPipe,
  JsonPipe,
  UpperCasePipe,
} from '@angular/common';
import { environment } from '../../../../../environments/environment';
import { Router } from '@angular/router';
import { Sexe } from '../../../../core/models/sexe';

@Component({
  selector: 'app-membre-card',
  imports: [UpperCasePipe, DecimalPipe, DatePipe],
  templateUrl: './membre-card.component.html',
  styleUrl: './membre-card.component.scss',
})
export class MembreCardComponent {
  @Input()
  membre!: Membre;
  @Input()
  showActions: boolean = true;
  baseUrl: string = environment.imagesUrl;

  constructor(private router: Router) {}

  onEdit(): void {
    this.router.navigateByUrl('/membre/add/' + this.membre.id);
  }

  onView(): void {
    this.router.navigateByUrl('/membre/view/' + this.membre.id);
  }

  get photo(): string {
    return this.membre.sexe == Sexe.Feminin
      ? './assets/images/default_woman.jpg'
      : './assets/images/default_man.jpg';
  }

  get bgColor(): string {
    return this.membre.sexe == Sexe.Feminin ? 'bg-danger' : 'bg-primary';
  }

  get dateStart(): string {
    return this.membre.sexe == Sexe.Feminin ? 'Née le ' : 'Né le ';
  }
}
