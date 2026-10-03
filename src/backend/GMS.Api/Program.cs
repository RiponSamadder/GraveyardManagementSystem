using System.Text;
using GMS.Application.Interfaces;
using GMS.Infrastructure.Data;
using GMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<GraveyardDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Application Services (DI)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGraveyardService, GraveyardService>();
builder.Services.AddScoped<IGraveService, GraveService>();
builder.Services.AddScoped<IBurialService, BurialService>();
builder.Services.AddScoped<IDeceasedService, DeceasedService>();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IMemorialService, MemorialService>();

// 3. CORS (allow Flutter Web & local development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 4. JWT Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? "DefaultSecretKey12345678901234567890!";
var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// 5. Controllers & JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// 6. Swagger / OpenAPI with Bearer Auth
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Digital Graveyard & Memorial Management API",
        Version = "v1",
        Description = "Sprint 2 — Full CRUD API: Auth · Graveyards · Graves · Burials · Deceased · Finance · Memorials · Dashboard"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 7. HTTP Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DGMMS API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Health Check Endpoint
app.MapGet("/api/health", async (GraveyardDbContext db) =>
{
    var canConnect = await db.Database.CanConnectAsync();
    return Results.Ok(new
    {
        status = "Healthy",
        sprint = "Sprint 2 — Complete",
        databaseConnected = canConnect,
        server = "DESKTOP-B6E86RN",
        database = "Graveyard",
        endpoints = new[]
        {
            "POST /api/auth/login",
            "POST /api/auth/register",
            "GET  /api/auth/me",
            "GET  /api/graveyards",
            "GET  /api/graves",
            "GET  /api/burials",
            "GET  /api/deceased",
            "GET  /api/finance/donations",
            "GET  /api/finance/expenses",
            "GET  /api/finance/funds",
            "GET  /api/memorials",
            "GET  /api/dashboard"
        },
        timestamp = DateTime.UtcNow
    });
}).WithTags("Health");

app.Run();
