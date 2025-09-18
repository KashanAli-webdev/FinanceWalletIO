import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { Constant } from '../constants/constant';
import { catchError, throwError } from 'rxjs';
import { inject } from '@angular/core';
import { Router } from '@angular/router';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const token = localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY);

  if (!token || token == 'undefined')
    return next(req);

  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });
  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        localStorage.removeItem(Constant.KEY_NAME.TOKEN_KEY);
        router.navigateByUrl('/');
      }
      return throwError(() => error);
    })
  );

};

