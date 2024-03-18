using HomeOfficeCheckin.Data;
using HomeOfficeCheckin.Models;
using HomeOfficeChecking.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<HomeOfficeTimeDbContext>(option =>
{
    //option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));

    // using SQLite for development
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultSQLiteConnection"));
});

// get mail server configuration
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailServerSettings"));

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173", "http://localhost:4200");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();



// ######################### Create and init database ###########################
// Initialize and create database by startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var context = services.GetRequiredService<HomeOfficeTimeDbContext>();
    try
    {
        await context.Database.MigrateAsync();
        await HomeOfficeSeedDbContext.SeedDataAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during migration");
    }
}

// ###############################################################################

app.Run();
