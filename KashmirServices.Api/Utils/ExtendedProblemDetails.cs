using KashmirServices.Application.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Utils;

public sealed class ExtendedProblemDetails : ProblemDetails
{
    public List<APIError> Errors { get; set; } = new List<APIError>();
}
