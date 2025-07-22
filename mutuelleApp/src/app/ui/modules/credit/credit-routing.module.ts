import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  creditResolver,
  creditsResolver,
} from '../../../core/resolvers/credit.resolver';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'liste',
    pathMatch: 'full',
  },
  {
    path: 'liste',
    loadComponent: () =>
      import('../../pages/credit/liste/liste.component').then((m) => m.default),
    resolve: [creditsResolver],
  },
  {
    path: 'add/:id',
    loadComponent: () =>
      import('../../pages/credit/add/add.component').then((m) => m.default),
    resolve: [creditResolver],
  },
  {
    path: 'view/:id',
    loadComponent: () =>
      import('../../pages/credit/view/view.component').then((m) => m.default),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CreditRoutingModule {}
