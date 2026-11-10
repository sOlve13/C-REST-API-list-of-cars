using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using TestApi.Api.Dtos;
using TestApi.Api.Cars;
using TestApi.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<FilePath>(builder.Configuration.GetSection("CarFileStorage"));
builder.Services.AddScoped<ICarRepo, CarRepo>();
builder.Services.AddScoped<ICarService, CarService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(
            "null"
        )
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
var app = builder.Build();
app.MapControllers();


app.UseCors("FrontendPolicy");
app.Run();
