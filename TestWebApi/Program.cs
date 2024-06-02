using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonitorDataBaseContext>(options =>
                                                          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/api/users" , (MonitorDbContext context) => context.Users.ToList());

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
