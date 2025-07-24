import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  avanceResolver,
  avancesResolver,
} from '../../../core/resolvers/avance.resolver';
import { membresResolver } from '../../../core/resolvers/membre.resolver';
import { agencesResolver } from '../../../core/resolvers/agence.resolver';
import { echeancesResolver } from '../../../core/resolvers/echeance.resolver';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'liste',
    pathMatch: 'full',
  },
  {
    path: 'liste',
    loadComponent: () =>
      import('../../pages/avance/liste/liste.component').then((m) => m.default),
    resolve: [avancesResolver, membresResolver, echeancesResolver],
  },
  {
    path: 'add',
    loadComponent: () =>
      import('../../pages/avance/add/add.component').then((m) => m.default),
    resolve: [membresResolver, agencesResolver],
  },
  {
    path: 'view/:id',
    loadComponent: () =>
      import('../../pages/avance/view/view.component').then((m) => m.default),
    children: [
      {
        path: '',
        loadComponent: () => import('../../pages/avance/infos/infos.component'),
        resolve: [
          avanceResolver,
          membresResolver,
          agencesResolver,
          echeancesResolver,
        ],
      },
      {
        path: 'paiement/:id',
        loadComponent: () =>
          import('../../pages/avance/paiement/paiement.component'),
        resolve: [
          avanceResolver,
          membresResolver,
          agencesResolver,
          echeancesResolver,
        ],
      },
    ],
    resolve: [
      avanceResolver,
      membresResolver,
      agencesResolver,
      echeancesResolver,
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AvanceRoutingModule {}
