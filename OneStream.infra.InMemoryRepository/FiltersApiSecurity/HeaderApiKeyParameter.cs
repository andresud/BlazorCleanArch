using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneStream.infra.InMemoryRepository.FiltersApiSecurity
{
    public class HeaderApiKeyParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            OpenApiParameter parameter = new OpenApiParameter
            {
                Name = "XApiKey",
                In = ParameterLocation.Header,
                Description = "APIKey header for authentication",
                Required = true
            };

            operation.Parameters.Add(parameter);
        }
    }
}
