import { Component, inject } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../../core/services/auth.service';
import { AuthRequest } from '../../../../core/models/auth-request';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export default class LoginComponent {
  authRequest = new AuthRequest('', '');

  constructor(
    private toastr: ToastrService,
    private authService: AuthService
  ) {}

  onSubmitForm(form: NgForm): void {
    if (form.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }

    this.authService.login(form.value).subscribe({
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
