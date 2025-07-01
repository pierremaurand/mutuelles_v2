import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../core/services/auth.service';
import { AuthRequest } from '../../../../core/models/auth-request';
import { Router } from '@angular/router';
import { TokenService } from '../../../../core/token/token.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export default class LoginComponent {
  authRequest: AuthRequest = { login: '', password: '' };

  constructor(
    private toastr: ToastrService,
    private authService: AuthService,
    private tokenService: TokenService,
    private router: Router
  ) {}

  onSubmitForm(form: NgForm): void {
    if (form.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }

    this.authService.login(form.value).subscribe({
      next: (response) => {
        this.toastr.success('Login successful!');
        this.tokenService.token = response.token as string;
        this.tokenService.refreshToken = response.refreshToken as string;
        this.router.navigateByUrl('/home');
      },
      error: (error) => {
        this.toastr.error('Login failed. Please check your credentials.');
        console.error('Login error:', error);
      },
    });
  }
}
