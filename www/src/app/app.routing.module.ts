import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './common/components';
import { authGuard } from './common/guards';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LayoutComponent,
    canActivate: [authGuard],
  },
  { path: 'auth', loadComponent: () => import('./auth/login.component') },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
