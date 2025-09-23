import { Component, signal } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AlertPopup } from "./shared/layout/alert-popup/alert-popup";
import { Constant } from './core/constants/constant';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AlertPopup],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('FinanceWalletIOUI');

  constructor(private auth: AuthService, private router: Router) {
    this.auth.clearExpiredToken();
    const token = localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY);
    if (token) {
      this.router.navigate(['/layout/income-home']);
    }
  }

}