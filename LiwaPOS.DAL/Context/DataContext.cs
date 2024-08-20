using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Consts;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            string connectionString = ConnectionService.GetConnectionString().Result;

            return !string.IsNullOrEmpty(connectionString) ? connectionString + " TrustServerCertificate=True;" : Defaults.DefaultConnectionString;
        }

        public DbSet<AppAction> AppActions { get; set; }
        public DbSet<AppRule> AppRules { get; set; }
        public DbSet<RuleActionMap> RuleActionMaps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
