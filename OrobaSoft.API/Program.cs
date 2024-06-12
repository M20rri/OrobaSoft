using Microsoft.EntityFrameworkCore;
using OrobaSoft.API.Interfaces;
using OrobaSoft.Shared.Context;
using OrobaSoft.Shared.Interfaces;
using OrobaSoft.Shared.Mapping;
using OrobaSoft.Shared.Models;
using OrobaSoft.Shared.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<InvoiceDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();
builder.Services.AddScoped<IRepository<InvoiceItem>, Repository<InvoiceItem>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
