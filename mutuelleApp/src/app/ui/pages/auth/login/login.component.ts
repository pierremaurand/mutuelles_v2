import { AuthService } from './../../../../core/services/services/auth.service';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthRequestDto } from '../../../../core/services/models';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export default class LoginComponent {
  authRequest: AuthRequestDto = { login: '', password: '' };

  constructor(
    private toastr: ToastrService,
    private authService: AuthService
  ) {}

  onSubmitForm(form: NgForm): void {
    if (form.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }

    this.authService.apiAuthLoginPost({ body: form.value }).subscribe({
      next: (response) => {
        this.toastr.success('Login successful!');
        // Handle successful login, e.g., redirect or store token
      },
      error: (error) => {
        this.toastr.error('Login failed. Please check your credentials.');
        console.error('Login error:', error);
      },
    });
  }
}
