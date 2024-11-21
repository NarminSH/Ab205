using Microsoft.EntityFrameworkCore;
using PurpleBuzzPr.Models;

namespace PurpleBuzzPr.DAL
{
    public class AppDbContext:DbContext
    {
       
        public AppDbContext(DbContextOptions options): base(options) { }
        
        public DbSet<Work> Works { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
