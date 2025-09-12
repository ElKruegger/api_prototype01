using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<apiAzure.Services.IPersonService, apiAzure.Services.PersonService>();

// EF Core DbContext
builder.Services.AddDbContext<apiAzure.Data.ApiDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Health Checks
builder.Services
    .AddHealthChecks()  
    .AddDbContextCheck<apiAzure.Data.ApiDbContext>(name: "postgres-db");

// Repository via EF
builder.Services.AddScoped<apiAzure.Repositories.IPersonRepository, apiAzure.Repositories.EfPersonRepository>();

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

// Health check endpoints
app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => false // always healthy if app is running
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _ => true
});

app.Run();



/// Commit aleatorio pra ver o CI/CD do github actions em a��o, sou foda