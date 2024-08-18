using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);              
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AppAction> AppActions { get; set; }
        public DbSet<AppRule> AppRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }        
    }
}
