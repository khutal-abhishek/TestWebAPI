using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ READ DATABASE URL (Railway standard)
var connectionString =
    builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration["ConnectionStrings__DefaultConnection"];

// ❌ DO NOT THROW EXCEPTION
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("⚠️ Database connection string not found");
}
else
{
    builder.Services.AddDbContext<TestDbContext>(options =>
        options.UseNpgsql(connectionString));
}

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
