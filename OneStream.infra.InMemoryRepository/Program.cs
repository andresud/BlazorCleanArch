
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OneStream.infra.InMemoryRepository.AutoMapper;
using OneStream.infra.InMemoryRepository.Data;
using OneStream.infra.InMemoryRepository.FiltersApiSecurity;
using OneStream.infra.InMemoryRepository.Services;

namespace OneStream.infra.InMemoryRepository
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IApiKeyValidation, ApikeyValidation>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ApiKeyAuthFilter>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
