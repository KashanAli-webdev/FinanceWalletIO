import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Constant } from '../constants/constant';
import { CreateIncomeDto, IncomeDetailsDto, IncomeListDto, PaginationDto, UpdateIncomeDto } from '../models/income.model';

@Injectable({
  providedIn: 'root'
})
export class IncomeSourceService {
  constructor(private http: HttpClient) { }
  private baseUrl = environment.apiUrl + Constant.MODULE_NAME.INCOME_SOURCE;

  GetList(pageNum: number) {
    return this.http.get<PaginationDto>(`${this.baseUrl}?pageNum=${pageNum}`);
  }

  // GetList() {
  //   return this.http.get<IncomeListDto[]>(this.baseUrl);
  // }

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
