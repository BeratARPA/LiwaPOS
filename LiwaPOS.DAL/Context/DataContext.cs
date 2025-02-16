using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Enums;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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

            optionsBuilder.UseSqlServer(ConnectionService.GetFullConnectionString());
        }

        public DbSet<AppAction> AppActions { get; set; }
        public DbSet<AppRule> AppRules { get; set; }
        public DbSet<AutomationCommand> AutomationCommands { get; set; }
        public DbSet<AutomationCommandMap> AutomationCommandMaps { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityCustomField> EntityCustomFields { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemPortion> MenuItemPortions { get; set; }
        public DbSet<MenuItemPrice> MenuItemPrices { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<PrinterMap> PrinterMaps { get; set; }
        public DbSet<PrinterTemplate> PrinterTemplates { get; set; }
        public DbSet<PrintJob> PrintJobs { get; set; }
        public DbSet<ProgramSettingValue> ProgramSettingValues{ get; set; }
        public DbSet<RuleActionMap> RuleActionMaps { get; set; }
        public DbSet<ScreenMenu> ScreenMenus { get; set; }
        public DbSet<ScreenMenuCategory> ScreenMenuCategories { get; set; }
        public DbSet<ScreenMenuItem> ScreenMenuItems { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseType> WarehouseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);
        }
    }
}
