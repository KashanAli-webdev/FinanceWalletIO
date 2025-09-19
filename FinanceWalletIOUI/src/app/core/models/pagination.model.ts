export interface PaginationDto<T> {
  dtoList: T[];
  totalCount: number;
  pageNum: number;
  pageSize: number;
}