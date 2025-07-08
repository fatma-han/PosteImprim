using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PosteImprim.Models;

namespace PosteImprim.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Imprimer> Imprimers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Imprimer)
                .WithMany(i => i.Positions)
                .HasForeignKey(p => p.ImprimerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}