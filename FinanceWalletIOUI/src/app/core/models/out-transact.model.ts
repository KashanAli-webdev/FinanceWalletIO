export interface OutTransactListDto {
  id: string;
  expenseName: string;
  amount: number;
  deductDate: Date;
}

export interface OutTransactDetailsDto {
  expenseType: string;
  expenseName: string;
  amount: number;
  deductDate: Date;
  notes?: string;
  isAutoAdded: boolean;
  createdAt: Date;
}

export interface CreateOutTransactDto {
  expenseSourceId: string;
  amount: number;
  deductDate: Date;
  notes?: string;
}

export interface UpdateOutTransactDto {
  id: string;
  expenseSourceId: string;
  amount: number;
  deductDate: Date;
  notes?: string;
}