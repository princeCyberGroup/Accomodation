/*
 *  Url: http://localhost:5217/swagger/index.html,
         https://localhost:7057/swagger/index.html
 *
 */
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Add sqllite to the container
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.Run();
