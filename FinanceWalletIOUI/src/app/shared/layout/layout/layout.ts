import { Component } from '@angular/core';
import { Navbar } from "../navbar/navbar";
import { Sidebar } from "../sidebar/sidebar";
import { Footer } from "../footer/footer";
import { RouterModule } from "@angular/router";

@Component({
  selector: 'app-layout',
  imports: [Navbar, Sidebar, Footer, RouterModule],
  templateUrl: './layout.html',
  styleUrl: './layout.css'
})
export class Layout {
  
  // constructor(private service: AuthService) {
  //   service.clearExpiredToken() // runs as soon as root app loads
  // }

  // CheckToken(): boolean {
  //   // (!!) this called bool coercion, it return ture on "acb" and false on "" or null.(like: isNullOrEmpty)
  //   return !!localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY) &&
  //     localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY) != 'undefined';
  // }

}
