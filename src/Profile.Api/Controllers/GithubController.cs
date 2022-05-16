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

    public GithubController(IGithubService githubService)
    {
        _githubService = githubService;
    }

    [HttpGet]
    public async Task<ActionResult<GithubUser>> Get([Required] string user)
    {
        var result = await _githubService.GetUserAsync(user);

        if (result is not null)
            return Ok(result);

        return NotFound();
    }
}