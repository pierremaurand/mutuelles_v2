import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'liste',
    pathMatch: 'full',
  },
  {
    path: 'liste',
    loadComponent: () =>
      import('../../pages/agence/liste/liste.component').then((m) => m.default),
  },
  {
    path: 'add/:id',
    loadComponent: () =>
      import('../../pages/agence/add/add.component').then((m) => m.default),
  },
  {
    path: 'view/:id',
    loadComponent: () =>
      import('../../pages/agence/view/view.component').then((m) => m.default),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AgenceRoutingModule {}
