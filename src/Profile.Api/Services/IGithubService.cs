using Profile.Api.Models;

namespace Profile.Api.Services;

public interface IGithubService
{
    Task<GithubUser?> GetUserAsync(string user);
}