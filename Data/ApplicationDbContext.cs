using BarberRatingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberRatingSystem.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            new Role { Id = 1, Name = "Admin" };
            new Role { Id = 2, Name = "User" };
        }
    }
}
