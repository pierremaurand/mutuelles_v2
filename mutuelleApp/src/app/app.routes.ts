import { Routes } from '@angular/router';
import { AuthService } from './core/services/auth.service';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./core/layout/layout/layout.component'),
    children: [
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full',
      },
      {
        path: 'home',
        loadChildren: () =>
          import('./ui/modules/home/home.module').then((m) => m.HomeModule),
        canActivate: [AuthService],
      },
    ],
  },

  {
    path: 'auth',
    loadChildren: () =>
      import('./ui/modules/auth/auth.module').then((m) => m.AuthModule),
  },
];
