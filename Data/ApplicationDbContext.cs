using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Worktastic.Models;

namespace Worktastic.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(e => e.Firstname)
                .HasMaxLength(250);

            modelBuilder.Entity<User>()
                .Property(e => e.Lastname)
            .HasMaxLength(250);

            modelBuilder.Entity<User>()
                .HasOne(e => e.Company)
                .WithOne(e => e.User)
                .HasForeignKey<Company>();

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Jobs)
                .WithOne(e => e.Company)
                .HasForeignKey("CompanyId")
                .IsRequired();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}