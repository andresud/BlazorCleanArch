using Microsoft.AspNetCore.Mvc;

namespace OneStream.infra.InMemoryRepository.FiltersApiSecurity
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
            : base(typeof(ApiKeyAuthFilter))
        {
        }
    }
}
