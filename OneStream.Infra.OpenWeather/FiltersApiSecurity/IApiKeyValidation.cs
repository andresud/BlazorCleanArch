namespace OneStream.infra.OpenWeather.FiltersApiSecurity
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string apiKey);
    }
}
