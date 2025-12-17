using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TaskItem> Tasks { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       

        // In Shaa Allah, connect to a local database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-BNJU8IC;Initial Catalog=TaskManagment;Trusted_Connection=True;Integrated Security=SSPI; TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedTasks) // User has many AssignedTasks
                .WithOne(t => t.Assignee) 
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict); // Can't delete a User if they have assigned tasks

            base.OnModelCreating(modelBuilder);
        }
    }
}



