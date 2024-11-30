global using Contract;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Services.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);
ConfigureDatabase(builder.Services, builder.Configuration);
ConfigureJwtAuthentication(builder.Services, builder.Configuration);
ConfigureCors(builder.Services);

var app = builder.Build();

ConfigureApp(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddScoped<IRepositoryManager, RepositoryManager>();
    services.AddScoped<IServiceManager, ServiceManager>();
    services.AddScoped<ITokenService, TokenService>();

    services.AddIdentity<Account, AccountRole>(opt =>
    {
        opt.Password.RequiredLength = 7;
        opt.Password.RequireDigit = true;
        opt.Password.RequireUppercase = true;
        opt.SignIn.RequireConfirmedEmail = true;
    })
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();
}

void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
    services.AddDbContextPool<DataContext>(options =>
    {
        options.UseMySql(configuration.GetConnectionString("MainDB"), serverVersion,
            mysqlOptions => { mysqlOptions.EnableRetryOnFailure(1, TimeSpan.FromSeconds(5), null); });
    });
}

void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("JwtSettings");

    services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
        };
    });
}

void ConfigureCors(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("Storage", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

void ConfigureApp(WebApplication app, IHostEnvironment environment)
{
    app.UseCors("Storage");

    if (environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}