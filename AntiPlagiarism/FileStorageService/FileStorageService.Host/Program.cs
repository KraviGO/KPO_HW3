using Microsoft.EntityFrameworkCore;
using FileStorageService.Infrastructure.Data;
using FileStorageService.Infrastructure;
using FileStorageService.Presentation.Endpoints;
using FileStorageService.UseCases;

var builder = WebApplication.CreateBuilder(args);

string storagePath = builder.Configuration["StoragePath"] ?? "uploads";
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddFileStorageUseCases();
builder.Services.AddFileStorageInfrastructure(connectionString, storagePath);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AntiPlagiarismDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapWorkEndpoints();
app.MapFileEndpoints();

app.Run();