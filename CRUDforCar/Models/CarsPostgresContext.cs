using Microsoft.EntityFrameworkCore;

namespace CRUDforCar.Models
{
    public class CarsPostgresContext : DbContext
    {
        public CarsPostgresContext(DbContextOptions<CarsPostgresContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
    }
}