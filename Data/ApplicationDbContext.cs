using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EventSphereApp.Models;

namespace EventSphereApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<UsersModel> // ✅ Use IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<EventsModel> EventsFormed { get; set; } // Use the minimal model

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EventsModel>().ToTable("EventsFormed"); // Ensure correct mapping

            base.OnModelCreating(modelBuilder); // Ensure Identity configuration

            // Ensure Identity tables have primary keys
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }

    }
}

