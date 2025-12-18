using DataAccess.Context;
using DataAccess.Repositories;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use SQL connection resilience and retries for transient failures
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// repositories
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<UserRepository>();

// business logic
builder.Services.AddScoped<TaskItemBLL>();
builder.Services.AddScoped<UserBLL>();

var app = builder.Build();

// Apply migrations with retry while SQL Server finishes starting
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    const int maxAttempts = 12;
    var delay = TimeSpan.FromSeconds(5);

    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            db.Database.Migrate();
            logger.LogInformation("Database migration succeeded.");
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database migration attempt {Attempt} failed.", attempt);
            if (attempt == maxAttempts)
            {
                logger.LogError(ex, "Exceeded migration attempts. Throwing.");
                throw;
            }
            Thread.Sleep(delay);
        }
    }
}

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
