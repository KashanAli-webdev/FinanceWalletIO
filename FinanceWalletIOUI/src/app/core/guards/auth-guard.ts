import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constant } from '../constants/constant';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router); 
  
  const token = localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY);

  if (token == null || token == 'undefined') {
    router.navigateByUrl('/');
    return false;
  }
  return true;
};
