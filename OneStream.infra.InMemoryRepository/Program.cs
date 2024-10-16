
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OneStream.infra.InMemoryRepository.AutoMapper;
using OneStream.infra.InMemoryRepository.Data;
using OneStream.infra.InMemoryRepository.Services;
using OneStream.Infra.InMemoryRepository.Auth;
using System.Text;
using OneStream.Infra.InMemoryRepository;
using OneStream.Domain.Services;

namespace OneStream.infra.InMemoryRepository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<TokenService>();

            builder.Services.AddControllers();

            // CORS options

            var specificOrgins = "LocalHost";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(specificOrgins,
                    policy =>
                    {
                        policy.SetIsOriginAllowed(origin =>
                            new Uri(origin).Host == "localhost")  // Allow any localhost origin with any port
                            .AllowAnyMethod()                     // Allow any HTTP method (GET, POST, etc.)
                            .AllowAnyHeader()                     // Allow any header
                            .AllowCredentials();                  // Allow credentials (optional)
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "OneStream.OpenWeather", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });

            // JWT Configuration

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppSettings"));

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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddEndpointsApiExplorer();            

            builder.Services.AddControllers();
            builder.Services.AddScoped<IWeatherRecordServices, WeatherRecordService>();
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("WeatherDb"))
            );
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseCors(specificOrgins);
            app.UseHttpsRedirection();            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
