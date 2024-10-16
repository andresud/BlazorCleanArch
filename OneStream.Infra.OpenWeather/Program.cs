
using Microsoft.OpenApi.Models;
using OneStream.Domain.Interfaces;
using OneStream.infra.OpenWeather.FiltersApiSecurity;
using OneStream.Infra.OpenWeather.Entities;
using OneStream.Infra.OpenWeather.Services;

namespace OneStream.Infra.OpenWeather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IApiKeyValidation, ApikeyValidation>();           
            builder.Services.AddScoped<ApiKeyAuthFilter>();

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

            builder.Services.AddSwaggerGen(
    s => {
        s.AddSecurityDefinition("XApiKey", new OpenApiSecurityScheme()
        {
            Type = SecuritySchemeType.ApiKey,
            Name = "XApiKey",
            In = ParameterLocation.Header,
            Scheme = "XApiKey"
        });
        s.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
                                     {
                                        new OpenApiSecurityScheme
                                        {
                                            Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = "XApiKey"
                                            }
                                        },
                                        new string[] {}
                                }
    });
        //s.OperationFilter<HeaderApiKeyParameter>();
    });
            
            builder.Services.AddHttpClient<OpenWeatherService>();
            builder.Services.AddScoped<IWeatherService, OpenWeatherService>();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppSettings"));

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
