using System.Net;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;

namespace Consumer.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class IntegrationController : ControllerBase
{
    private readonly ILogger<IntegrationController> _logger;

    public IntegrationController(ILogger<IntegrationController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(string user)
    {
        var response = await Policy
            .Handle<FlurlHttpException>(x => x.StatusCode == (int) HttpStatusCode.BadRequest)
            .RetryAsync(3,
                onRetry: (exception, retryCount, context) =>
                {
                    _logger.LogWarning("Attempts => {Count}, Exception {Exception}", retryCount, exception.Message);
                })
            .ExecuteAsync(() =>
                "https://localhost:7045"
                    .AppendPathSegment("github")
                    .SetQueryParam("user", user)
                    .GetAsync()
            );

        return Ok(new
        {
            statusCode = response.StatusCode,
            response = await response.ResponseMessage.Content.ReadAsStringAsync(),
        });
    }
}