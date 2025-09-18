import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { IncomeStreams, TimeInterval } from '../../../core/enums/enums';
import { UpdateIncomeDto } from '../../../core/models/income.model';
import { IncomeSourceService } from '../../../core/services/income-source.service';
import { ImportedModules } from '../../../shared/imports/imports.shared';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-income-update',
  imports: [ImportedModules],
  templateUrl: './income-update.html',
  styleUrl: './income-update.css'
})
export class IncomeUpdate {
  @Input() id!: string;
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
    this.service.GetById(this.id).subscribe({
      next: (res) => {
        const incomeTypeNumber = this.getIncomeTypeNumber(res.incomeType);
        const repeatIntervalNumber = this.getIntervalNumber(res.repeatInterval);

        this.incomeForm.patchValue({
          incomeType: incomeTypeNumber,
          name: res.name,
          autoRepeat: res.autoRepeat,
          repeatInterval: repeatIntervalNumber,
          notes: res.notes ?? ''
        });

        this.modalService.open(content, { size: 'md', centered: false });
      },
      error: (err) => console.error('Failed to load income details', err)
    });
  }

  onSubmit(modal: any) {
    if (this.incomeForm.invalid)
      return this.incomeForm.markAllAsTouched();

    const dto: UpdateIncomeDto = {
      id: this.id,
      incomeType: Number(this.incomeForm.value.incomeType),
      name: this.incomeForm.value.name!,
      autoRepeat: this.incomeForm.value.autoRepeat!,
      repeatInterval: Number(this.incomeForm.value.repeatInterval),
      notes: this.incomeForm.value.notes!
    };

    this.service.Update(this.id, dto).subscribe({
      next: (res: any) => {
        this.event.emit();
        modal.close();
        this.toaster.TriggerNotify(res.msg, 'success');
      },
      error: err => {
        this.toaster.TriggerNotify(err.msg, 'success');
        console.error("Update failed", err.error.errors)
      }
    });
  }

  getIncomeTypeNumber(type: string): number {
    return Object.values(IncomeStreams).indexOf(type);
  }
  getIntervalNumber(interval: string): number {
    return Object.values(TimeInterval).indexOf(interval);
  }

}