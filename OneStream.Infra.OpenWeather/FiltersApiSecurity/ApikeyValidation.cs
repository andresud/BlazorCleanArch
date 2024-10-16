namespace OneStream.infra.OpenWeather.FiltersApiSecurity
{
    public class ApikeyValidation : IApiKeyValidation
    {
        public bool IsValidApiKey(string apiKey)
        {
            if (apiKey == null)
            {
                return false;
            }

            return true;
        }
    }
}
