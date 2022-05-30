using AlphaGenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlphaGenAPI.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAlan>().HasKey(sc => new { sc.UserId, sc.AlanId });
            modelBuilder.Entity<AlanHareket>().HasKey(sc => new { sc.AlanId, sc.HareketId });
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserParams> UserParams { get; set; }
        public virtual DbSet<UserInApp> UserInApps { get; set; }
        public virtual DbSet<Alan> Alans { get; set; }

        public virtual DbSet<UserAlan> UserAlans { get; set; }
        public virtual DbSet<Hareket> Harekets { get; set; }
        public virtual DbSet<AlanHareket> AlanHarekets { get; set; }
    }

   
}
