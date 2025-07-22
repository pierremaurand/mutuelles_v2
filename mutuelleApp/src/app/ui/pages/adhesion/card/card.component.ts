import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Adhesion } from '../../../../core/models/adhesion';
import { DecimalPipe, UpperCasePipe } from '@angular/common';
import { Router } from '@angular/router';
import { environment } from '../../../../../environments/environment';
import { Membre } from '../../../../core/models/membre';

@Component({
  selector: 'app-card',
  imports: [UpperCasePipe, DecimalPipe],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss',
})
export class CardComponent {
  @Input()
  adhesion!: Adhesion;
  @Input()
  membre!: Membre;
  baseUrl: string = environment.imagesUrl;

  constructor(private router: Router) {}

  onEdit(): void {}

  onView(): void {}
}
