export interface PaginationDto<T> {
  dtoList: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}