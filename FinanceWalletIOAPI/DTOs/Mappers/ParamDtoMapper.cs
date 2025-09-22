using FinanceWalletIOAPI.DTOs.Base;

namespace FinanceWalletIOAPI.DTOs.Mappers
{
    public sealed class ParamDtoMapper
    {
        public PaginationDto<IncomeListDto> ParamMap(
            List<IncomeListDto> dtos, int totalCount, int pageNum, int pageSize)
        {
            return new PaginationDto<IncomeListDto> // Pass Generic type
            {
                DtoList = dtos,
                TotalCount = totalCount,
                PageNum = pageNum,
                PageSize = pageSize
            };
        }
    }
}
