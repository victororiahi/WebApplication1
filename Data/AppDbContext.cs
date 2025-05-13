using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetAttraction> Attractions { get; set; }
        public DbSet<PetCareTaker> CareTakers { get; set; }
        public DbSet<PetOwner> Owners { get; set; }
    }
}
