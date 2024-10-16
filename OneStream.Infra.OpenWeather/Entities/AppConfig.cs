namespace OneStream.Infra.OpenWeather.Entities
{
    public class AppConfig
    {
        public string Key { get; set; }
        public string BasePath { get; set; }
        public string JwtKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
    }
}
