import { Component, inject } from '@angular/core';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-alert-popup',
  imports: [],
  templateUrl: './alert-popup.html',
  styleUrl: './alert-popup.css'
})
export class AlertPopup {
  toaster = inject(ToasterService);
}