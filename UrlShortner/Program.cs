using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UrlShortner.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DevDb")
    ?? throw new InvalidOperationException("UrlShortnerContext Not Found");

builder.Services.AddDbContext<UrlShortnerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "URL Shortener", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "URL Shortener");
});

app.MapControllers();

app.Run();
