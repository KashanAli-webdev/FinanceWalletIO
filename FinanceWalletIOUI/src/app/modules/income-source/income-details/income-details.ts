import { Component, Input, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IncomeSourceService } from '../../../core/services/income-source.service';
import { IncomeDetailsDto } from '../../../core/models/income.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-income-details',
  templateUrl: './income-details.html',
  styleUrls: ['./income-details.css']
})
export class IncomeDetails {
  @Input() id!: string;
  service = inject(IncomeSourceService);
  modalService = inject(NgbModal);
    toaster = inject(ToasterService);

  dto: IncomeDetailsDto = {
    incomeType: '',
    name: '',
    autoRepeat: false,
    repeatInterval: '',
    notes: '',
    createdAt: new Date()
  };

  openModal(content: any) {
    this.GetDetails(() => {
      // modal doesnt open untill data loads
      this.modalService.open(content, { size: 'md', centered: false });
    });
  }

  GetDetails(callback?: () => void) {
    this.service.GetById(this.id).subscribe({
      next: (res) => {
        this.dto = res;
        console.log(this.id, this.dto, 'loaded');
        if (callback) callback(); // make sure data load first then open modal.
      },
      error: err => {
        this.toaster.TriggerNotify(err.error.msg, false);
        console.error("Failed to fetch details", err.error.errors);
      }
    });
  }
}