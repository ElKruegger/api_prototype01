using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

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
    // ping direto ao PostgreSQL (SELECT 1)
    .AddNpgSql(
        connectionString: builder.Configuration.GetConnectionString("Postgres"),
        name: "postgres-raw",
        timeout: TimeSpan.FromSeconds(5))
    // mantém o check do DbContext (opcional, mas útil)
    .AddDbContextCheck<apiAzure.Data.ApiDbContext>(name: "postgres-db");
// Repository via EF
builder.Services.AddScoped<apiAzure.Repositories.IPersonRepository, apiAzure.Repositories.EfPersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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
    Predicate = _ => true,
    ResponseWriter = async (ctx, report) =>
    {
        ctx.Response.ContentType = "application/json";
        var payload = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new {
                name = e.Key,
                status = e.Value.Status.ToString(),
                error = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString()
            })
        };
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
});


app.Run();



/// Commit aleatorio pra ver o CI/CD do github actions em a��o, sou foda