using System;
using Microsoft.EntityFrameworkCore;

namespace hospital.Entities
{
    public class HospitalDbContext: DbContext
    {
        private string _connectionString =
             @"
   Data Source=tcp:hospital-server.database.windows.net,1433;
Initial Catalog=HospitalDatabase;
User ID=student-ms;
Password=PolapPolap1;
";

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired().HasMaxLength(250);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName).HasMaxLength(250);
            modelBuilder.Entity<User>()
               .Property(u => u.LastName).HasMaxLength(250);
            modelBuilder.Entity<User>()
               .Property(u => u.Password).HasMaxLength(250);

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired().HasMaxLength(250);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
