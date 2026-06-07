using Microsoft.AspNetCore.Mvc;

namespace AppointMe.Shared.Pagination;

public class PaginationRequest
{
    [FromQuery(Name = "limit")]
    public int Limit { get; init; } = 10;

    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;
}

public static class PaginationRequestExtensions
{
    extension(PaginationRequest request)
    {
        public PaginationFilter ToPaginationFilter()
        {
            return new PaginationFilter(request.Limit, request.Page);
        }
    }
}
