import { ExpenseStreams, TimeInterval } from "../enums/enums";

export interface ExpenseListDto {
  id: string;
  expenseType: string;
  name: string;
  repeatInterval: string;
  notes?: string;
}

export interface ExpenseDetailsDto {
  expenseType: string;
  name: string;
  autoRepeat: boolean;
  repeatInterval: string;
  notes?: string;
  createdAt: Date;
}

export interface CreateExpenseDto {
  expenseType: ExpenseStreams;
  name: string;
  autoRepeat: boolean;
  repeatInterval: TimeInterval;
  notes?: string;
}

export interface UpdateExpenseDto {
  id: string;
  expenseType: ExpenseStreams;
  name: string;
  autoRepeat: boolean;
  repeatInterval: TimeInterval;
  notes?: string;
}