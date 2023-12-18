using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers.Common;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected internal string RequestedUri => Request.GetEncodedUrl();

    protected CreatedResult Created<T>(T obj)
    {
        return Created(RequestedUri, obj);
    }
}
