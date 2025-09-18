import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./shared/layout/navbar/navbar";
import { Sidebar } from "./shared/layout/sidebar/sidebar";
import { Footer } from "./shared/layout/footer/footer";
import { Constant } from './core/constants/constant';
import { AlertPopup } from "./shared/layout/alert-popup/alert-popup";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Sidebar, Footer, AlertPopup],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('FinanceWalletIOUI');

  // get anyName(): datatype{} :- this is typescript getter of any datatype.
  get hasToken(): boolean {
    return this.CheckToken();
  }

  CheckToken(): boolean {
    // (!!) this called bool coercion, it return ture on "acb" and false on "" or null.(like: isNullOrEmpty)
    return !!localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY) && 
    localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY) != 'undefined';
  }
}
