using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JwtTokenAuthentication;
using RabbitMq.Consumer.HostedServices;
using RabbitMq.Consumer.Services;
using RabbitMq.Common.Models;
using RabbitMq.Common.Services;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.addJwtAuthentication();

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IConsumerService, ConsumerService>();
builder.Services.AddHostedService<ConsumerHostedService>();

var app = builder.Build();


//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5004";

app.Run($"http://0.0.0.0:{port}");



