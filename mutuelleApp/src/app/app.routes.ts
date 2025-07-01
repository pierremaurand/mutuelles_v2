import { Routes } from '@angular/router';
import { authGuard } from './core/guard/auth.guard';

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
        canActivate: [authGuard],
      },
    ],
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./ui/modules/auth/auth.module').then((m) => m.AuthModule),
  },
];
