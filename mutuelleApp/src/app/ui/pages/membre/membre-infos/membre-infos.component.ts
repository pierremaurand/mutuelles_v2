import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MembreService } from '../../../../core/services/membre.service';
import { AgenceService } from '../../../../core/services/agence.service';
import { CotisationService } from '../../../../core/services/cotisation.service';
import { Router } from '@angular/router';
import { AvanceService } from '../../../../core/services/avance.service';
import { CreditService } from '../../../../core/services/credit.service';
import { EcheanceService } from '../../../../core/services/echeance.service';
import { Observable } from 'rxjs';
import { Membre } from '../../../../core/models/membre';
import { AsyncPipe, CommonModule, UpperCasePipe } from '@angular/common';
import { Sexe } from '../../../../core/models/sexe';

@Component({
  selector: 'app-membre-infos',
  imports: [CommonModule, AsyncPipe, UpperCasePipe],
  templateUrl: './membre-infos.component.html',
  styleUrl: './membre-infos.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class MembreInfosComponent implements OnInit {
  membre$!: Observable<Membre>;
  Sexe: any;
  constructor(
    private membreService: MembreService,
    private agenceService: AgenceService,
    private cotisationService: CotisationService,
    private avanceService: AvanceService,
    private creditService: CreditService,
    private echeanceService: EcheanceService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initObservable();
  }

  initObservable(): void {
    // Initialize observables here if needed
    this.membre$ = this.membreService.membre$;
  }

  afficheSexe(sexe: Sexe): string {
    switch (sexe) {
      case Sexe.Masculin:
        return 'M';
      case Sexe.Feminin:
        return 'F';
      default:
        return 'Non spécifié';
    }
  }
}
