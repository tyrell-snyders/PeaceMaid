using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Repos
builder.Services.AddScoped<IUser, UserRepo>();

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

app.Run();
