using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Data;
using EmployeeAPI.Services;
using EmployeeAPI.MiddleWare;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add database service
builder.Services.AddDbContext<EmployeeAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseLogMiddleware();

app.Use(async (context, next) =>
{
    Console.WriteLine("Hi");
    Console.WriteLine("This is middleware in main program");
    await next(context);

    Console.WriteLine("This is the end of middleware in program");
});

app.MapControllers();

app.Run();
