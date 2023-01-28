using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackAll_BackEnd.Models;

namespace TrackAll_Backend.Database
{
    public class AppDbContext : IdentityDbContext<IdentityModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MarketPlaceMap> MarketPlaceMaps { get; set; }

    }
}
