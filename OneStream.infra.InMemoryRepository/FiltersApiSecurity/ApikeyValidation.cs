namespace OneStream.infra.InMemoryRepository.FiltersApiSecurity
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
