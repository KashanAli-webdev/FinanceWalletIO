import { Component, inject } from '@angular/core';
import { IncomeCreate } from "../income-create/income-create";
import { IncomeUpdate } from "../income-update/income-update";
import { IncomeDetails } from "../income-details/income-details";
import { IncomeSourceService } from '../../../core/services/income-source.service';
import { IncomeListDto } from '../../../core/models/income.model';
import { IncomeStreams, TimeInterval } from '../../../core/enums/enums';
import { ToasterService } from '../../../core/services/toaster.service';
import { NgbPagination, NgbPaginationPages } from "@ng-bootstrap/ng-bootstrap";
import { ListQueryParams } from '../../../core/models/query-params.model';
import { FormControl, FormGroup } from '@angular/forms';
import { ImportedModules } from '../../../shared/imports/imports.shared';

@Component({
  selector: 'app-income-home',
  imports: [IncomeCreate, IncomeUpdate, IncomeDetails, NgbPagination, NgbPaginationPages, ImportedModules],
  templateUrl: './income-home.html',
  styleUrl: './income-home.css'
})
export class IncomeHome {
  service = inject(IncomeSourceService);
  toaster = inject(ToasterService);

  incomeStreams = Object.values(IncomeStreams).filter(v => typeof v === 'string');
  timeIntervals = Object.values(TimeInterval).filter(v => typeof v === 'string');

  dto: IncomeListDto[] = [];
  totalCount = 0;
  pageNum = 1;
  pageSize = 2;
  filterForm: FormGroup = new FormGroup({
    category: new FormControl<number | null>(null),
    interval: new FormControl<number | null>(null),
    from: new FormControl<Date | null>(null),
    to: new FormControl<Date | null>(null)
  });

  ngOnInit(): void {
    this.GetList(this.pageNum);
  }

  GetList(page: number): void {
    const params: ListQueryParams<IncomeStreams> = {
      pageNum: page,
      pageSize: this.pageSize,
      category: this.filterForm.value.category,
      interval: this.filterForm.value.interval,
      from: this.filterForm.value.from ? new Date(this.filterForm.value.from) : null,
      to: this.filterForm.value.to ? new Date(this.filterForm.value.to) : null,
    };

    this.service.GetList(params).subscribe({
      next: (res) => {
        this.dto = res.dtoList;
        this.totalCount = res.totalCount;
        this.pageNum = res.pageNum;
      }
    });
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  onPageChange(page: number): void {
    this.GetList(page);
  }

  selectPage(value: string | number): void {
    const page = Number(value);
    const totalPages = Math.ceil(this.totalCount / this.pageSize);

    if (!isNaN(page) && page >= 1 && page <= totalPages) {
      this.onPageChange(page);
    }
  }

  Delete(id: string): void {
    this.service.Delete(id).subscribe({
      next: (res: any) => {
        this.GetList(this.pageNum);
        this.toaster.TriggerNotify(res.msg, 'success');
      },
      error: err => {
        this.toaster.TriggerNotify(err.msg, 'success');
        console.error("Create failed", err.error.errors)
      }
    });
  }

}