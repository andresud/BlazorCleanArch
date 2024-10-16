namespace OneStream.Domain.Entities
{
    public class WeatherRecord
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public double Temp { get; set; }       
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }      
        public double WindSpeed { get; set; }      
        public int WindDeg { get; set; }
        public double WindGust { get; set; }
    }
}
