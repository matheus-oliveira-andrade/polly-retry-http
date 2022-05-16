namespace Profile.Api.Models;

public class GithubUser
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Blog { get; set; }
    public DateOnly CreatedAt { get; set; }
    
    public string AvatarUrl { get; set; }
    public string ProfileUrl { get; set; }
}