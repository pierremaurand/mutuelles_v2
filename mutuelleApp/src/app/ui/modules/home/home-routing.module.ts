import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('../../pages/home/home/home.component'),
  },
  {
    path: 'profile',
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
        path: 'image',
        loadComponent: () =>
          import('../../composants/image-add/image-add.component'),
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
