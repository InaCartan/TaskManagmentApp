using DataAccess.Context;
using DataAccess.Repositories;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//repositories
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<UserRepository>();

// business logic
builder.Services.AddScoped<TaskItemBLL>();
builder.Services.AddScoped<UserBLL>(); 

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
