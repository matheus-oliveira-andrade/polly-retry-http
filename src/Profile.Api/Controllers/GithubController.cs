using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Profile.Api.Models;
using Profile.Api.Services;

namespace Profile.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GithubController : ControllerBase
{
    private readonly IGithubService _githubService;
    private readonly Counter _counter;
    private readonly ILogger<GithubController> _logger;

    public GithubController(IGithubService githubService, Counter counter, ILogger<GithubController> logger)
    {
        _githubService = githubService;
        _counter = counter;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<GithubUser>> Get([Required] string user)
    {
        if (_counter.Quantity < 3)
        {
            _logger.LogWarning("Counter quantity {Quantity}", _counter.Quantity);
            
            _counter.Increment();
            return BadRequest();
        }

        var result = await _githubService.GetUserAsync(user);

        if (result is not null)
            return Ok(result);

        return NotFound();
    }
}