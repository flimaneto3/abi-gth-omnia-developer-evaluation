using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int GetCurrentUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());
    }

    protected string GetCurrentUserEmail()
    {
        return User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();
    }

    protected IActionResult Ok<T>(T data)
    {
        return base.Ok(data);
    }

    protected IActionResult Created<T>(string routeName, object routeValues, T data)
    {
        return base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });
    }

    protected IActionResult BadRequest(string message)
    {
        return base.BadRequest(new ApiResponse { Message = message, Success = false });
    }

    protected IActionResult NotFound(string message = "Resource not found")
    {
        return base.NotFound(new ApiResponse { Message = message, Success = false });
    }

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList)
    {
        return Ok(new PaginatedResponse<T>
        {
            Data = pagedList,
            CurrentPage = pagedList.CurrentPage,
            TotalPages = pagedList.TotalPages,
            TotalCount = pagedList.TotalCount,
            Success = true
        });
    }
}