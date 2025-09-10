export interface InTransactListDto {
  id: string;
  incomeName: string;
  amount: number;
  receivedDate: Date;
}

export interface InTransactDetailsDto {
  incomeType: string;
  incomeName: string;
  amount: number;
  receivedDate: Date;
  notes?: string;
  isAutoAdded: boolean;
  createdAt: Date;
}

export interface CreateInTransactDto {
  incomeSourceId: string;
  amount: number;
  receivedDate: Date;
  notes?: string;
}

export interface UpdateInTransactDto {
  id: string;
  incomeSourceId: string;
  amount: number;
  receivedDate: Date;
  notes?: string;
}