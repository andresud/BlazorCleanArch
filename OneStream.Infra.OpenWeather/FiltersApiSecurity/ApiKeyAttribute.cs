using Microsoft.AspNetCore.Mvc;

namespace OneStream.infra.OpenWeather.FiltersApiSecurity
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
