using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Application.Mappings;
using RecruitmentSystem.Application.Services;
using RecruitmentSystem.Infrastructure.Data;
using RecruitmentSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recruitment System API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
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
            new string[] { }
        }
    });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
Console.WriteLine("DEBUGGING CONFIGURATION:");
foreach (var config in builder.Configuration.AsEnumerable())
{
    if (config.Key.Contains("JwtSettings"))
        Console.WriteLine($"{config.Key} = {config.Value}");
}

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var keyStr = jwtSettings["Key"];
if (string.IsNullOrEmpty(keyStr))
{
    Console.WriteLine("CRITICAL: JwtSettings:Key IS NULL!");
    keyStr = "TemporarySuperSecretKeyForDevelopmentOnly123!"; // fallback for debug
}
var key = Encoding.ASCII.GetBytes(keyStr);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

// Dependency Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

try
{
    var app = builder.Build();
    app.UseDeveloperExceptionPage();
    
    // Test Database Connection
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            Console.WriteLine("Testing database connection...");
            if (context.Database.CanConnect())
            {
                Console.WriteLine("DATABASE CONNECTION SUCCESSFUL!");
            }
            else
            {
                Console.WriteLine("CRITICAL: CANNOT CONNECT TO THE DATABASE! Check connection string.");
            }
        }
        catch (Exception dbEx)
        {
            Console.WriteLine($"DATABASE CONNECTION ERROR: {dbEx.Message}");
        }
    }
    
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // app.UseHttpsRedirection();
    app.UseCors("AllowAngular");
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllers();
    
    Console.WriteLine("Application is starting...");
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"AN ERROR OCCURRED: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    if (ex.InnerException != null)
    {
        Console.WriteLine($"INNER EXCEPTION: {ex.InnerException.Message}");
        Console.WriteLine(ex.InnerException.StackTrace);
    }
}
