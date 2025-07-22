import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  avanceResolver,
  avancesResolver,
} from '../../../core/resolvers/avance.resolver';

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
    resolve: [avancesResolver],
  },
  {
    path: 'add/:id',
    loadComponent: () =>
      import('../../pages/avance/add/add.component').then((m) => m.default),
    resolve: [avanceResolver],
  },
  {
    path: 'view/:id',
    loadComponent: () =>
      import('../../pages/avance/view/view.component').then((m) => m.default),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AvanceRoutingModule {}
