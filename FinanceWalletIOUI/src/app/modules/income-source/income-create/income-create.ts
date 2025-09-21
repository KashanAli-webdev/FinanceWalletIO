import { Component, EventEmitter, inject, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IncomeStreams, TimeInterval } from '../../../core/enums/enums';
import { CreateIncomeDto } from '../../../core/models/income.model';
import { IncomeSourceService } from '../../../core/services/income-source.service';
import { ImportedModules } from '../../../shared/imports/imports.shared';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-income-create',
  imports: [ImportedModules],
  templateUrl: './income-create.html',
  styleUrl: './income-create.css'
})
export class IncomeCreate {
  service = inject(IncomeSourceService);
  modalService = inject(NgbModal);
  toaster = inject(ToasterService);

  @Output() event = new EventEmitter<void>();

  incomeStreams = Object.values(IncomeStreams).filter(v => typeof v === 'string');
  timeIntervals = Object.values(TimeInterval).filter(v => typeof v === 'string');

  incomeForm = new FormGroup({
    incomeType: new FormControl<number | null>(null, [Validators.required]),
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(30),
      Validators.pattern('^[A-Za-z0-9\\s\\-]+$')
    ]),
    autoRepeat: new FormControl(false),
    repeatInterval: new FormControl<number | null>(null, [Validators.required]),
    notes: new FormControl('', [Validators.maxLength(500)])
  });

  openModal(content: any) {
    this.modalService.open(content, { size: 'md', centered: false });
  }

  onSubmit(modal: any) {
    if (this.incomeForm.invalid)
      return this.incomeForm.markAllAsTouched();

    const dto: CreateIncomeDto = {
      incomeType: Number(this.incomeForm.value.incomeType),
      name: this.incomeForm.value.name!,
      autoRepeat: this.incomeForm.value.autoRepeat!,
      repeatInterval: Number(this.incomeForm.value.repeatInterval),
      notes: this.incomeForm.value.notes || undefined
    };

    this.service.Create(dto).subscribe({
      next: (res: any) => {
        this.event.emit();        
        this.incomeForm.reset({  // reset the form values
          incomeType: null,
          name: '',
          autoRepeat: false,
          repeatInterval: null,
          notes: ''
        });
        modal.close();
        this.toaster.TriggerNotify(res.msg, 'success');
      },
      error: err => {
        this.toaster.TriggerNotify(err.msg, 'danger');
        console.error("Create failed", err.error.errors)
      }
    });

  }
}