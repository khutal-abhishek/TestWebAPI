using Microsoft.EntityFrameworkCore;
using TestWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration["ConnectionStrings__DefaultConnection"];

if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<TestDbContext>(options =>
        options.UseNpgsql(connectionString)
    );
}
else
{
    Console.WriteLine("❌ Database connection string is missing.");
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestWebAPI v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();
app.Run();
