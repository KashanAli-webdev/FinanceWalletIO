import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./shared/layout/navbar/navbar";
import { Sidebar } from "./shared/layout/sidebar/sidebar";
import { Footer } from "./shared/layout/footer/footer";
import { AlertPopup } from "./shared/layout/alert-popup/alert-popup";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Sidebar, Footer, AlertPopup],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('FinanceWalletIOUI');
}
