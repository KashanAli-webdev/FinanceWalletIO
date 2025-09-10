import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FeaturesMenu } from "../features-menu/features-menu";
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, FeaturesMenu, NgClass],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
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

}
