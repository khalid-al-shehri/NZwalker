
using Microsoft.EntityFrameworkCore;
using NZwalker.Models.Domain;

namespace NZwalker.Data{

    public class NZWalksDbContext: DbContext {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) {

        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walks> Walks { get; set; }
    }
}