namespace OneStream.infra.InMemoryRepository.FiltersApiSecurity
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string apiKey);
    }
}
