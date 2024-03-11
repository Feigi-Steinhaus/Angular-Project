using Bll.Functions;
using Bll.Interfaces;
using Bll;
using Microsoft.EntityFrameworkCore;
using Bll.Services;
using Dal.Interfaces;
using Dal.Functions;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//serviceהוספה שלי-ל
builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cors-הוספת ה
builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }
    ));

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//cors-המשך הגדרת ה
app.UseCors("AllowAll");

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