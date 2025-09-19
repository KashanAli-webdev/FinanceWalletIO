import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Constant } from '../constants/constant';
import { CreateIncomeDto, IncomeDetailsDto, IncomeListDto, UpdateIncomeDto } from '../models/income.model';
import { PaginationDto } from '../models/pagination.model';

@Injectable({
  providedIn: 'root'
})
export class IncomeSourceService {
  constructor(private http: HttpClient) { }
  private baseUrl = environment.apiUrl + Constant.MODULE_NAME.INCOME_SOURCE;

  GetList(pageNum: number, pageSize: number) {
    return this.http.get<PaginationDto<IncomeListDto>>
      (`${this.baseUrl}?pageNum=${pageNum}&pageSize=${pageSize}`);
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
