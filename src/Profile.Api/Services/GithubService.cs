using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using Profile.Api.Models;

namespace Profile.Api.Services;

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GithubService> _logger;

    public GithubService(HttpClient httpClient, ILogger<GithubService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<GithubUser?> GetUserAsync(string user)
    {
        _logger.LogInformation("Searching for user {User}", user);

        var response = await _httpClient.GetAsync($"/users/{user}");

        if (response.StatusCode == HttpStatusCode.NotFound) 
        {
            _logger.LogInformation("User {User} not found", user);
            
            return null;
        }

        if (!response.IsSuccessStatusCode)
            throw new Exception("Unexpected error");

        var result = await response.Content.ReadAsStringAsync();

        var githubUser = JsonConvert.DeserializeObject<GithubUser>(result);
        
        _logger.LogInformation("Github user {@User} found", githubUser);

        return githubUser;
    }
}