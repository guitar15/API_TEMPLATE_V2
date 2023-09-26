using JwtTokenAuthentication;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, false);


builder.Services.addJwtAuthentication();
builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();


 app.UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();


var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

app.Run($"http://0.0.0.0:{port}");
