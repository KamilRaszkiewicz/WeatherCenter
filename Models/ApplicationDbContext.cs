using Microsoft.EntityFrameworkCore;
using WeatherCenter.Models.Entities;

namespace WeatherCenter.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Widget> Widgets { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts)
        {
        }
    }
}
