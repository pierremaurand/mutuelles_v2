import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { membresResolver } from '../../../core/resolvers/membre.resolver';
import { cotisationsResolver } from '../../../core/resolvers/cotisation.resolver';
import { avancesResolver } from '../../../core/resolvers/avance.resolver';
import { creditsResolver } from '../../../core/resolvers/credit.resolver';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../../pages/home/home/home.component'),
    resolve: [
      membresResolver,
      cotisationsResolver,
      avancesResolver,
      creditsResolver,
    ],
  },
  {
    path: 'profile/:id',
    loadComponent: () => import('../../pages/home/profile/profile.component'),
    children: [
      {
        path: '',
        loadComponent: () =>
          import(
            '../../composants/infos-utilisateur/infos-utilisateur.component'
          ),
      },
      {
        path: 'image/:id',
        loadComponent: () =>
          import('../../composants/image-add/image-add.component'),
        data: {
          origin: 'utilisateur',
        },
      },
      {
        path: 'password',
        loadComponent: () =>
          import('../../pages/home/change-password/change-password.component'),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
