import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Avance } from '../../../../core/models/avance';
import { combineLatest, map, Observable } from 'rxjs';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { CommonModule } from '@angular/common';
import { AvanceService } from '../../../../core/services/avance.service';
import { Router, RouterOutlet } from '@angular/router';
import { MembreService } from '../../../../core/services/membre.service';
import { Membre } from '../../../../core/models/membre';
import { InfosPretComponent } from '../../../composants/infos-pret/infos-pret.component';
import { EcheanceService } from '../../../../core/services/echeance.service';

@Component({
  selector: 'app-view',
  imports: [CommonModule, RouterOutlet, InfosPretComponent],
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ViewComponent implements OnInit {
  avance$!: Observable<Avance>;
  membre$!: Observable<Membre>;
  avanceSolder: boolean = false;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;

  constructor(private avanceService: AvanceService, private router: Router) {}

  ngOnInit(): void {
    this.avance$ = this.avanceService.avance$;
  }

  onAnticipePaiement(id: number): void {
    this.router.navigateByUrl('/avance/view/' + id + '/paiement/' + id);
  }

  onBack(): void {
    this.router.navigateByUrl('/avance');
  }
}
