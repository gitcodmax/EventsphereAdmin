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

    }
}

