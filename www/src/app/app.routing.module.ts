import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './common/components';
import { authGuard } from './common/guards';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'devices',
        loadComponent: () => import('./devices/devices.component'),
      },
      {
        path: 'accounts',
        loadComponent: () => import('./accounts/accounts.component'),
      },
      {
        path: 'access-cards',
        loadComponent: () => import('./access-cards/access-cards.component'),
      },
      {
        path: 'sessions',
        loadComponent: () => import('./sessions/sessions.component'),
      },
    ],
  },
  { path: 'auth', loadComponent: () => import('./auth/login.component') },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
