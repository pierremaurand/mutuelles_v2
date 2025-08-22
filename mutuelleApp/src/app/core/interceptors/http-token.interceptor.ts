import { TokenService } from './../token/token.service';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

export const httpTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);

  if (tokenService.isAuthenticateUser()) {
    const token = tokenService.token;
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }
  return next(req);
};
