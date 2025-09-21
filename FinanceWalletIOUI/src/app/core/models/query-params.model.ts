import { TimeInterval } from "../enums/enums";

export interface ListQueryParams<T> {
  pageNum: number;
  pageSize: number;
  category?: T | null;
  interval?: TimeInterval | null;
  from?: Date | null;
  to?: Date | null;
}