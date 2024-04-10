using Microsoft.EntityFrameworkCore;
using World.API.Models;

namespace World.API.Data
{
    public class WorldAPIDbContext : DbContext
    {
        public WorldAPIDbContext(DbContextOptions<WorldAPIDbContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

	

	}

}
