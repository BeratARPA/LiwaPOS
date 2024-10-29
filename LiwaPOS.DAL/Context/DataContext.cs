using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionService.GetConnectionString());
        }

        public DbSet<AppAction> AppActions { get; set; }
        public DbSet<AppRule> AppRules { get; set; }
        public DbSet<RuleActionMap> RuleActionMaps { get; set; }
        public DbSet<AutomationCommand> AutomationCommands { get; set; }
        public DbSet<AutomationCommandMap> AutomationCommandMaps { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
