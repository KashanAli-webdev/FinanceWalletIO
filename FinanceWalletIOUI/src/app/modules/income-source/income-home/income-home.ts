import { Component, inject } from '@angular/core';
import { IncomeCreate } from "../income-create/income-create";
import { IncomeUpdate } from "../income-update/income-update";
import { IncomeDetails } from "../income-details/income-details";
import { IncomeSourceService } from '../../../core/services/income-source.service';
import { IncomeListDto } from '../../../core/models/income.model';
import { IncomeStreams } from '../../../core/enums/enums';
import { ToasterService } from '../../../core/services/toaster.service';
import { NgbPagination, NgbPaginationPages } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-income-home',
  imports: [IncomeCreate, IncomeUpdate, IncomeDetails, NgbPagination, NgbPaginationPages ],
  templateUrl: './income-home.html',
  styleUrl: './income-home.css'
})
export class IncomeHome {
  service = inject(IncomeSourceService);
  toaster = inject(ToasterService);

  incomeStreams = Object.values(IncomeStreams).filter(v => typeof v === 'string');

  dto: IncomeListDto[] = [];
  totalCount = 0;
  pageNumber = 1;
  pageSize = 2;

  ngOnInit(): void {
    this.GetList(this.pageNumber);
  }

  GetList(page: number): void {
    this.service.GetList(page).subscribe({
      next: (res) => {
        this.dto = res.dtoList;
        this.totalCount = res.totalCount;
        this.pageNumber = res.pageNumber;
        this.pageSize = res.pageSize;
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
        this.GetList(this.pageNumber);
        this.toaster.TriggerNotify(res.msg, 'success');
      },
      error: err => {
        this.toaster.TriggerNotify(err.msg, 'success');
        console.error("Create failed", err.error.errors)
      }
    });
  }

}