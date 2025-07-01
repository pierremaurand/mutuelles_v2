import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor(private router: Router, private toastr: ToastrService) {}

  set token(token: string) {
    localStorage.setItem('token', token);
  }

  get token(): string {
    return localStorage.getItem('token') as string;
  }

  set refreshToken(token: string) {
    localStorage.setItem('refresh-token', token);
  }

  get refreshToken(): string {
    return localStorage.getItem('refresh-token') as string;
  }

  isAuthenticateUser(): boolean {
    const token = this.token;
    if (token && !this.tokenExpired(token)) {
      return true;
    }
    this.logout();
    return false;
  }

  private tokenExpired(token: string) {
    const expiry = JSON.parse(atob(token.split('.')[1])).exp;
    return Math.floor(new Date().getTime() / 1000) >= expiry;
  }

  logout(): void {
    localStorage.clear();
    this.router.navigateByUrl('/auth');
    this.toastr.success('Logout successful!');
  }
}
