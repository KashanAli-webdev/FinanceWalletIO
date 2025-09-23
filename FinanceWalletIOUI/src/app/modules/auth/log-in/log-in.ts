import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ImportedModules } from '../../../shared/imports/imports.shared';
import { AuthService } from '../../../core/services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginDto } from '../../../core/models/auth.model';
import { Constant } from '../../../core/constants/constant';

@Component({
  selector: 'app-log-in',
  imports: [ImportedModules],
  templateUrl: './log-in.html',
  styleUrl: './log-in.css'
})
export class LogIn {
  service = inject(AuthService);
  router = inject(Router);

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(4)])
  });

  OnSubmit() {
    if (this.loginForm.invalid)
      return this.loginForm.markAllAsTouched(); // highlight errors

    const dto: LoginDto = this.loginForm.value as LoginDto;
    this.service.SignIn(dto).subscribe({
      next: (res: any) => {
        localStorage.setItem(Constant.KEY_NAME.TOKEN_KEY, res.msg);
        this.router.navigate(['/layout/income-home']);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

}
