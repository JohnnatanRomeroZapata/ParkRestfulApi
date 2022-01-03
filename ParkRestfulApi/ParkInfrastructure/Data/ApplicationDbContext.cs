using Microsoft.EntityFrameworkCore;
using ParkCore.Models;

namespace ParkInfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<NationalPark> NationalParks { get; set; }

        public virtual DbSet<Trail> Trails { get; set; }

    }
}
