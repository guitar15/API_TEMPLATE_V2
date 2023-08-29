using JwtTokenAuthentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, false);

builder.Services.AddOcelot(builder.Configuration);
builder.Services.addJwtAuthentication();

var app = builder.Build();


await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

app.Run($"http://0.0.0.0:{port}");
