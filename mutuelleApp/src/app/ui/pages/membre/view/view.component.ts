import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Membre } from '../../../../core/models/membre';
import { Observable } from 'rxjs';
import { SafeUrl } from '@angular/platform-browser';
import { environment } from '../../../../../environments/environment';
import { CommonModule, UpperCasePipe } from '@angular/common';
import { MembreService } from '../../../../core/services/membre.service';
import { Router, RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MembreRequest } from '../../../../core/models/membre-request';

@Component({
  selector: 'app-view',
  imports: [CommonModule, UpperCasePipe, RouterOutlet],
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ViewComponent implements OnInit {
  membre$!: Observable<Membre>;
  photo: SafeUrl = './assets/images/default_man.jpg';
  baseUrl: string = environment.imagesUrl;
  membre!: Membre;

  constructor(
    private membreService: MembreService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.membre$ = this.membreService.membre$;
    this.membre$.subscribe({
      next: (membre: Membre) => {
        this.membre = membre;
        if (membre.photo) {
          this.photo = this.baseUrl + '/' + membre.photo;
        }
      },
    });
  }

  onActivated(): void {
    this.membre.estActif = !this.membre.estActif;
    this.membreService
      .addOrUpdate(this.membre.id, this.membre as MembreRequest)
      .subscribe({
        next: () => {
          this.toastr.success('Membre mis à jour avec succès', 'Succès');
          this.membreService.getMembre(this.membre.id);
        },
        error: (err) => {
          this.toastr.error(err.error.message, 'Erreur');
        },
      });
  }

  onChangeImage(): void {
    this.router.navigateByUrl('/membre/view/image');
  }
}
