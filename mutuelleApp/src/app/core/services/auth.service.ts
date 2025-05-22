import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.baseUrl;
  imagesUrl = environment.imagesUrl;

  http = inject(HttpClient);
  router = inject(Router);
  tokenService = inject(TokenService);
}
