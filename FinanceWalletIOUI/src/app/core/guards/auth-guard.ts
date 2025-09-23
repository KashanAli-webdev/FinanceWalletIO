import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constant } from '../constants/constant';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const authService = inject(AuthService);

  authService.clearExpiredToken();

  const token = localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY);

  if (!token || token === 'undefined') {
    router.navigateByUrl('/');
    console.log('if (!token)');
    return false;
  }
  
  return true;
};