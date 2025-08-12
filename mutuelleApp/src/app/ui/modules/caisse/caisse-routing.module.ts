import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { mouvementsResolver } from '../../../core/resolvers/mouvement.resolver';
import { echeancesResolver } from '../../../core/resolvers/echeance.resolver';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'view',
    pathMatch: 'full',
  },
  {
    path: 'view',
    loadComponent: () =>
      import('../../pages/caisse/view/view').then((m) => m.default),
    children: [
      {
        path: '',
        redirectTo: 'liste',
        pathMatch: 'full',
      },
      {
        path: 'liste',
        loadComponent: () =>
          import('../../pages/caisse/liste/liste').then((m) => m.default),
        resolve: [mouvementsResolver],
      },
      {
        path: 'paiement',
        loadComponent: () =>
          import('../../pages/caisse/paiement/paiement').then((m) => m.default),
        resolve: [echeancesResolver],
      },
      {
        path: 'add',
        loadComponent: () =>
          import('../../pages/caisse/add/add').then((m) => m.default),
      },
    ],
    resolve: [mouvementsResolver],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CaisseRoutingModule {}
