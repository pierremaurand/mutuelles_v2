import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Credit } from '../../../../core/models/credit';
import { combineLatest, map, Observable } from 'rxjs';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { CommonModule } from '@angular/common';
import { CreditService } from '../../../../core/services/credit.service';
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
  credit$!: Observable<Credit>;
  membre$!: Observable<Membre>;
  creditSolder: boolean = false;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;

  constructor(
    private creditService: CreditService,
    private membreService: MembreService,
    private echeanceService: EcheanceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.credit$ = this.creditService.credit$;
    this.membre$ = combineLatest([
      this.creditService.credit$,
      this.membreService.membres$,
    ]).pipe(
      map(([credit, membres]) => {
        return membres.find((m) => m.id === credit.membreId) || new Membre();
      })
    );

    combineLatest([this.creditService.credit$, this.echeanceService.echeances$])
      .pipe(
        map(([credit, echeances]) => {
          const totalEcheances = echeances.filter(
            (e) => e.creditId === credit.id && !e.montantRestant
          );
          return totalEcheances.length === 0;
        })
      )
      .subscribe((soldered) => (this.creditSolder = soldered));
  }

  onAnticipePaiement(id: number): void {
    this.router.navigateByUrl('/credit/view/' + id + '/paiement/' + id);
  }
}
