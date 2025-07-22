import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { UpperCasePipe } from '@angular/common';
import { environment } from '../../../../../environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-membre-card',
  imports: [UpperCasePipe],
  templateUrl: './membre-card.component.html',
  styleUrl: './membre-card.component.scss',
})
export class MembreCardComponent {
  @Input()
  membre!: Membre;
  baseUrl: string = environment.imagesUrl;

  constructor(private router: Router) {}

  onEdit(): void {
    this.router.navigateByUrl('/membre/add/' + this.membre.id);
  }

  onView(): void {
    this.router.navigateByUrl('/membre/view/' + this.membre.id);
  }
}
