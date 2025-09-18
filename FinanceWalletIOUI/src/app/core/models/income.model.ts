import { IncomeStreams, TimeInterval } from "../enums/enums";

export interface IncomeListDto {
  id: string;
  incomeType: string;
  name: string;
  repeatInterval: string;
}

export interface PaginationDto {
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  dtoList: IncomeListDto[];
}

export interface IncomeDetailsDto {
  incomeType: string;
  name: string;
  autoRepeat: boolean;
  repeatInterval: string;
  notes?: string;
  createdAt: Date;
}

export interface CreateIncomeDto {
  incomeType: IncomeStreams;
  name: string;
  autoRepeat: boolean;
  repeatInterval: TimeInterval;
  notes?: string;
}

export interface UpdateIncomeDto {
  id: string;
  incomeType: IncomeStreams;
  name: string;
  autoRepeat: boolean;
  repeatInterval: TimeInterval;
  notes?: string;
}