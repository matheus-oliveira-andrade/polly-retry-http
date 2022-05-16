using Newtonsoft.Json;

namespace Profile.Api.Models;

public class GithubUser
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Blog { get; set; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }
    
    [JsonProperty("url")]
    public string ProfileUrl { get; set; }
}