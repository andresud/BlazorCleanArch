using Microsoft.EntityFrameworkCore;

namespace OneStream.infra.InMemoryRepository.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext
    {
        public DbSet<WeatherRecords> WeatherRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyWeatherDb");
        }
    }
}
