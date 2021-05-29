using Microsoft.EntityFrameworkCore;
using SeleniumSample.Models;

namespace SeleniumSample.Repository
{
    class VaccineDbContext : DbContext
    {
        public VaccineDbContext(DbContextOptions<VaccineDbContext> options) : base(options)
        {
        }

        public DbSet<AvailableSlot> AvailableSlot { get; set; }
    }
}
