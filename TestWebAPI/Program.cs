using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DATABASE CONFIG
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Detect PostgreSQL (Railway)
if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("Host="))
{
    builder.Services.AddDbContext<TestDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // Local SQL Server (Windows only)
    builder.Services.AddDbContext<TestDbContext>(options =>
        options.UseSqlServer(connectionString));
}

var app = builder.Build();

// Swagger ENABLED for Railway
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWebAPI v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
