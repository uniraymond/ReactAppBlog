using Microsoft.EntityFrameworkCore;
using ReactAppBlog.Server.Application.Interfaces;
using ReactAppBlog.Server.Application.Services;
using ReactAppBlog.Server.Infrastructure.Persistence;
using ReactAppBlog.Server.Mapping;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddAutoMapper(typeof(PostProfile).Assembly);

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
