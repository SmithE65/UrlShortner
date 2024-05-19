using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevDb")
    ?? throw new InvalidOperationException("UrlShortnerContext Not Found");
builder.Services.AddDbContext<UrlShortnerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.MapControllers();

app.Run();
