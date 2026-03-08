using GenshinTrial.Generator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using GenshinTrialGenerator.Application.DTOs.Hero;

using GenshinTrialGenerator.Server.Extensions;
using GenshinTrialGenerator.Application.Mappings;

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

//Services
builder.Services.AddApplicationServices();

//AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<BossProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<HeroProfile>());

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
