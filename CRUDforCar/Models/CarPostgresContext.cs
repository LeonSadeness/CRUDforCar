using CRUDforCar.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CRUDforCar.Models
{
    public class CarPostgresContext : DbContext
    {
        private readonly string _connectionString;

        public CarPostgresContext(ICarsDatabaseSettings settings) : base() 
        {
            _connectionString = settings.ConnectionString;
        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}