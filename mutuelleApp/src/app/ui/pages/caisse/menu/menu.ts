import { Component, Input } from '@angular/core';
import { Mouvement } from '../../../../core/models/mouvement';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './menu.html',
  styleUrl: './menu.scss',
})
export class Menu {
  @Input()
  mouvements!: Mouvement[];

  get soldeCaisse(): number {
    return (
      this.mouvements.reduce(
        (sum, x) => sum + x.montantCredit - x.montantDebit,
        0
      ) ?? 0
    );
  }
}
