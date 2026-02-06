using Microsoft.EntityFrameworkCore;
using Npgsql;
using TestWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Read Railway DATABASE_URL
var databaseUrl = builder.Configuration["DATABASE_URL"];

if (string.IsNullOrWhiteSpace(databaseUrl))
{
    throw new Exception("DATABASE_URL not found");
}

// 🔥 Convert URL → Connection String
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');

var connectionString = new NpgsqlConnectionStringBuilder
{
    Host = uri.Host,
    Port = uri.Port,
    Username = userInfo[0],
    Password = userInfo[1],
    Database = uri.AbsolutePath.Trim('/'),
    SslMode = SslMode.Require,
    TrustServerCertificate = true
}.ConnectionString;

builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
