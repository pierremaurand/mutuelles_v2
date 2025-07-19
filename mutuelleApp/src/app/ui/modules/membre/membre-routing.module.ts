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
      import('../../pages/membre/liste/liste.component').then((m) => m.default),
  },
  {
    path: 'add/:id',
    loadComponent: () =>
      import('../../pages/membre/add/add.component').then((m) => m.default),
  },
  {
    path: 'view/:id',
    loadComponent: () =>
      import('../../pages/membre/view/view.component').then((m) => m.default),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('../../pages/membre/membre-infos/membre-infos.component'),
      },
      {
        path: 'image:/id',
        loadComponent: () =>
          import('../../composants/image-add/image-add.component'),
        data: {
          origin: 'membre',
          backUrl: 'membre/view',
        },
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MembreRoutingModule {}
