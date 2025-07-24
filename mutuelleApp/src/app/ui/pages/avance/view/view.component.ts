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

  constructor(
    private avanceService: AvanceService,
    private membreService: MembreService,
    private echeanceService: EcheanceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.avance$ = this.avanceService.avance$;
    this.membre$ = combineLatest([
      this.avanceService.avance$,
      this.membreService.membres$,
    ]).pipe(
      map(([avance, membres]) => {
        return membres.find((m) => m.id === avance.membreId) || new Membre();
      })
    );

    combineLatest([this.avanceService.avance$, this.echeanceService.echeances$])
      .pipe(
        map(([avance, echeances]) => {
          const totalEcheances = echeances.filter(
            (e) => e.avanceId === avance.id && !e.estPaye
          );
          return totalEcheances.length === 0;
        })
      )
      .subscribe((soldered) => (this.avanceSolder = soldered));
  }

  onAnticipePaiement(id: number): void {
    this.router.navigateByUrl('/avance/view/' + id + '/paiement/' + id);
  }
}
