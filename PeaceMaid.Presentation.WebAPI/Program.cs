using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure.Implementation;
using PeaceMaid.Infrastructure.Implementation.Authentication;

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
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ISProvider, ServiceProviderRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins("https://localhost:7203")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithHeaders(HeaderNames.ContentType);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
