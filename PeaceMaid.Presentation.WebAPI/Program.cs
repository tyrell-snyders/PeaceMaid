using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure;
using Microsoft.OpenApi.Models;
using PeaceMaid.Presentation.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PeaceMaid API", Version = "v1" });

    // Security
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your JWT token"
    });

    // Apply Bearer Token conditionally
    c.OperationFilter<AuthorizeCheckOperationFilter>();
});

// DB
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Repos
builder.Services.AddInfrastructure();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
