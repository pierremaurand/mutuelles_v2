import { Component, Input } from '@angular/core';
import { Membre } from '../../../core/models/membre';
import { environment } from '../../../../environments/environment';
import { CommonModule, DecimalPipe, UpperCasePipe } from '@angular/common';

@Component({
  selector: 'app-infos-pret',
  imports: [CommonModule, DecimalPipe, UpperCasePipe],
  templateUrl: './infos-pret.component.html',
  styleUrl: './infos-pret.component.scss',
})
export class InfosPretComponent {
  @Input()
  membre!: Membre;
  @Input()
  montant!: number;
  @Input()
  commission!: number;
  @Input()
  interets!: number;
  @Input()
  duree!: number;
  baseUrl: string = environment.imagesUrl;
}
