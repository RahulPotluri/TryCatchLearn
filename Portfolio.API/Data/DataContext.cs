using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models.Login;
using Portfolio.API.Resume;

namespace Portfolio.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}