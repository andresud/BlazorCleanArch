using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

namespace OneStream.infra.OpenWeather.FiltersApiSecurity
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IApiKeyValidation _apiKeyValidation;
        const string APIKEY = "XApiKey";

        public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
        {
            _apiKeyValidation = apiKeyValidation;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                context.Result = new BadRequestResult();
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(APIKEY);

            if (!_apiKeyValidation.IsValidApiKey(extractedApiKey))
                context.Result = new UnauthorizedResult();
        }
    }
}