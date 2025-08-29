import { Component, Input } from '@angular/core';
import { Mouvement } from '../../../../core/models/mouvement';
import { DatePipe, DecimalPipe, UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-card-mouvement',
  imports: [UpperCasePipe, DecimalPipe, DatePipe],
  templateUrl: './card-mouvement.html',
  styleUrl: './card-mouvement.scss',
})
export class CardMouvement {
  @Input()
  mouvement!: Mouvement;

  get montant(): number {
    return (
      (this.mouvement.montantCredit ?? 0) + (this.mouvement.montantDebit ?? 0)
    );
  }
}
