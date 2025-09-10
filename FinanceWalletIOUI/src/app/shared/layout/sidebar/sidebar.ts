import { Component } from '@angular/core';
import { FeaturesMenu } from "../features-menu/features-menu";

@Component({
  selector: 'app-sidebar',
  imports: [FeaturesMenu],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {

}
