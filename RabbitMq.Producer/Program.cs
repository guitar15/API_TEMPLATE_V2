using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using JwtTokenAuthentication;
using RabbitMq.Common.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.addJwtAuthentication();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5005";

app.Run($"http://0.0.0.0:{port}");