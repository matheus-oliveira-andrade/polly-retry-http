using Profile.Api.Models;
using Profile.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

// DI container.
builder.Services.AddHttpClient<IGithubService, GithubService>(opt =>
{
    opt.BaseAddress = new Uri("https://api.github.com");
    opt.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    opt.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
});
builder.Services.AddSingleton<Counter>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();