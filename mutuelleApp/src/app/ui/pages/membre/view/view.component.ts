import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { Observable } from 'rxjs';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { AsyncPipe, CommonModule, UpperCasePipe } from '@angular/common';
import { MembreService } from '../../../../core/services/membre.service';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MembreRequest } from '../../../../core/models/membre-request';
import { Sexe } from '../../../../core/models/sexe';

@Component({
  selector: 'app-view',
  imports: [CommonModule, UpperCasePipe, RouterOutlet, AsyncPipe],
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ViewComponent implements OnInit {
  membre$!: Observable<Membre>;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;

  constructor(
    private membreService: MembreService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.membre$ = this.membreService.membre$;
    this.membre$.subscribe();
  }

  onActivated(membre: Membre): void {
    membre.estActif = !membre.estActif;
    this.membreService
      .addOrUpdate(membre.id, membre as MembreRequest)
      .subscribe({
        next: () => {
          this.toastr.success('Membre mis à jour avec succès', 'Succès');
          this.membreService.getMembreFromServer(membre.id);
        },
        error: (err) => {
          this.toastr.error(err, 'Erreur');
        },
      });
  }

  onChangeImage(id: number): void {
    this.router.navigateByUrl('/membre/view/' + id + '/image/' + id);
  }

  afficheSexe(sexe: Sexe): string {
    switch (sexe) {
      case Sexe.Masculin:
        return 'Homme';
      case Sexe.Feminin:
        return 'Femme';
      default:
        return 'Non spécifié';
    }
  }
}
