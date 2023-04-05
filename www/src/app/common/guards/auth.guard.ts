import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services';

export function authGuard() {
  inject(AuthService)
    .checkToken()
    .subscribe({
      next: () => true,
      error: () => {
        inject(Router).navigate(['auth']);
        return false;
      },
    });
}
