using GenshinTrial.Generator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Server.Configuration;
using GenshinTrialGenerator.Application.Mappings;
using GenshinTrialGenerator.Infrastructure.Data;
using GenshinTrialGenerator.Infrastructure.Mappings;
using GenshinTrialGenerator.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateHeroRequest>();

//Configurations
builder.Services.Configure<YandexStorageSettings>(
    builder.Configuration.GetSection("YandexStorage"));

//Services || API.Configuration
builder.Services.AddApplicationServices();

//AutoMapper || API.Configuration
builder.Services.AddAutoMapperProfiles();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
