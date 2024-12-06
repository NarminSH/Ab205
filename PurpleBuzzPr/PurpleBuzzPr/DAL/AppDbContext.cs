using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
       
        public AppDbContext(DbContextOptions options): base(options) { }
        
        public DbSet<Work> Works { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWork> EmployeeWorks { get; set; }
        public DbSet<WorkPhotos> WorkPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Work>()
                .HasOne(e => e.Service)
                .WithMany(e => e.Works)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<EmployeeWork>().HasOne(w=> w.Work)
                .WithMany(e=>e.EmployeeWorks)
            .HasForeignKey(w=>w.WorkId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeWork>().HasOne(w => w.Employee)
               .WithMany(e => e.EmployeeWorks)
           .HasForeignKey(w => w.EmployeeId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
