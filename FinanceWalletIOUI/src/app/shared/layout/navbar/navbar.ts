import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FeaturesMenu } from "../features-menu/features-menu";
import { NgClass } from '@angular/common';
import { AuthService } from '../../../core/services/auth.service';
import { Constant } from '../../../core/constants/constant';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FeaturesMenu],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
  service = inject(AuthService);
  router = inject(Router);

  isDarkTheme = true;
  themeIcone = 'sun-fill';
  themeName = 'light';

  ngOnInit() {
    const saved = localStorage.getItem('FinanceWalletIOTheme') || 'dark';
    this.isDarkTheme = saved === 'dark';
    document.body.setAttribute('data-bs-theme', saved);

    this.ThemeProps();
  }

  ThemeToggler() {
    this.isDarkTheme = !this.isDarkTheme;
    const theme = this.isDarkTheme ? 'dark' : 'light';
    document.body.setAttribute('data-bs-theme', theme);

    this.ThemeProps();
    localStorage.setItem('FinanceWalletIOTheme', theme);
  }

  ThemeProps() {
    this.themeIcone = this.isDarkTheme ? 'sun-fill' : 'moon-stars-fill';
    this.themeName = this.isDarkTheme ? 'light' : 'dark';
  }

  UserSignOut() {
    this.service.SignOut().subscribe({
      next: () => {
        localStorage.removeItem(Constant.KEY_NAME.TOKEN_KEY);
        this.router.navigate(['/']);
      },
      error: (err) => console.error(err)
    });
  }

}
