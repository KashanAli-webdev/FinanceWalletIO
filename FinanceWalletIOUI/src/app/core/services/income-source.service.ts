import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Constant } from '../constants/constant';
import { CreateIncomeDto, IncomeDetailsDto, IncomeListDto, UpdateIncomeDto } from '../models/income.model';
import { PaginationDto } from '../models/pagination.model';
import { ListQueryParams } from '../models/query-params.model';
import { IncomeStreams } from '../enums/enums';

@Injectable({
  providedIn: 'root'
})
export class IncomeSourceService {
  constructor(private http: HttpClient) { }
  private baseUrl = environment.apiUrl + Constant.MODULE_NAME.INCOME_SOURCE;

  GetList(params: ListQueryParams<IncomeStreams>) {
    return this.http.get<PaginationDto<IncomeListDto>>(this.baseUrl, {
      params: {
        pageNum: params.pageNum,
        pageSize: params.pageSize,
        category: params.category ?? '',
        interval: params.interval ?? ''
      }
    });
  }

  GetById(id: string) {
    return this.http.get<IncomeDetailsDto>(`${this.baseUrl}/${id}`);
  }

  Create(createIncomeDto: CreateIncomeDto) {
    return this.http.post(this.baseUrl, createIncomeDto);
  }

  Update(id: string, updateIncomeDto: UpdateIncomeDto) {
    return this.http.put(`${this.baseUrl}/${id}`, updateIncomeDto);
  }

  Delete(id: string) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

}
