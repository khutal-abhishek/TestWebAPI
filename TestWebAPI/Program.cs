using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Read Railway env variable safely
var connectionString =
    builder.Configuration["ConnectionStrings__DefaultConnection"];

// ✅ Do NOT crash if missing
if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<TestDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    Console.WriteLine("⚠️ Database connection string not found. App started without DB.");
}

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
