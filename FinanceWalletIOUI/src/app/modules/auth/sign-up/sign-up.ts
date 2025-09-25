import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ImportedModules } from '../../../shared/imports/imports.shared';
import { AuthService } from '../../../core/services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterDto } from '../../../core/models/auth.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-sign-up',
  imports: [ImportedModules],
  templateUrl: './sign-up.html',
  styleUrl: './sign-up.css'
})
export class SignUp {
  service = inject(AuthService);
  router = inject(Router);
  toaster = inject(ToasterService);

  signupForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3),
    Validators.maxLength(30), Validators.pattern('^[A-Za-z0-9\s\-]+$')]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(4)])
  });

  onSubmit() {
    if (this.signupForm.invalid)
      return this.signupForm.markAllAsTouched();

    const dto: RegisterDto = this.signupForm.value as RegisterDto;
    this.service.SignUp(dto).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.toaster.TriggerNotify(err.error.msg, false);
        console.error("Signup failed", err.error.errors)
      }
    });
  }

}
