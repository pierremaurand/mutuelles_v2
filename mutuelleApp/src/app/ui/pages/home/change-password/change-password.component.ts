import { AuthService } from './../../../../core/services/auth.service';
import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ChangePasswordRequest } from '../../../../core/models/change-password-request';

@Component({
  selector: 'app-change-password',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss',
})
export class ChangePasswordComponent implements OnInit {
  @Input() id!: number | undefined;
  mainForm!: FormGroup;
  @ViewChild('closeModal') modalClose: any;
  @Output()
  changePassword = new EventEmitter<ChangePasswordRequest>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  submitForm(): void {
    if (this.mainForm.valid) {
      this.changePassword.emit(this.mainForm.value);
      this.modalClose.nativeElement.click();
    }
  }

  initForm(): void {
    this.mainForm = this.fb.group(
      {
        motDePasse: ['', [Validators.required]],
        confirmMotDePasse: ['', [Validators.required]],
      },
      {
        validators: this.passwordMatchValidator,
      }
    );
  }

  passwordMatchValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    const password = control.get('motDePasse')?.value;
    const confirmPassword = control.get('confirmMotDePasse')?.value;
    return password && confirmPassword && password !== confirmPassword
      ? { passwordsNotMatch: true }
      : null;
  }
}
