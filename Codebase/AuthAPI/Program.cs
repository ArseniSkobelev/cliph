using System.Text;
using cliph.Middleware;
using cliph.Services.ApiKeyService;
using cliph.Services.AuthService;
using cliph.Services.StatsService;
using cliph.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// add services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStatsService, StatsService>();

var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration.GetValue<string>("JWT:Issuer"),
                ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret") ?? throw new InvalidOperationException("JWT:Secret ENV is not set. Please check your configuration.")))
            };
        }
    );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cliph API", Version = "v1" });

    c.AddSecurityDefinition("Bearer token", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
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

var app = builder.Build();

app.UseWhen(httpContext => !httpContext.Request.Path.StartsWithSegments("/api/v1/auth"),
    subApp =>
    {
        subApp.UseMiddleware<CrossServiceCommunicationAuthenticationMiddleware>();
    }
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
