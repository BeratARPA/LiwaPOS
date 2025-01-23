using LiwaPOS.DAL.Context;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LiwaPOS.DAL.Services
{
    public class DatabaseInitializer
    {
        private readonly DataContext _context;

        public DatabaseInitializer(DataContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            // mdf dosyasının mevcut olup olmadığını kontrol et
            if (!FileExtension.Exists(FolderLocationsHelper.LocalDBPath))
            {
                LoggingService.LogInfoAsync("MDF dosyası bulunamadı. Mevcut veritabanı varsa siliniyor ve yeniden oluşturuluyor...").Wait();
                RecreateDatabase();
            }
            else
            {
                try
                {
                    // Migrationları uygulayarak veritabanını güncel tut
                    _context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    LoggingService.LogInfoAsync($"Hata: {ex.Message}").Wait();
                    // Hata durumunda mdf dosyasını sil ve yeniden oluştur
                    if (!FileExtension.Exists(FolderLocationsHelper.LocalDBPath))
                    {
                        RecreateDatabase();
                    }
                }
            }
        }

        private void RecreateDatabase()
        {
            using (var connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DROP DATABASE IF EXISTS [LiwaPOS]";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Unable to open the physical file"))
                        connection.Close();
                }
                connection.Close();
            }

            if (_context.Database.EnsureCreated())
                SeedData();

            LoggingService.LogInfoAsync("Veritabanı başarıyla yeniden oluşturuldu.").Wait();
        }

        private void SeedData()
        {
            UserRole? currentUserRole = null;

            if (!_context.UserRoles.Any())
            {
                var userRole = new UserRole
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Admin",
                    IsAdmin = true
                };

                currentUserRole = _context.UserRoles.Add(userRole).Entity;
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var user = new User
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Administrator",
                    PinCode = "1234",
                    UserRole = currentUserRole
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            if (!_context.Departments.Any())
            {
                var department = new Department
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Restaurant",
                    WarehouseId = 0,
                    ScreenMenuId = 0
                };

                _context.Departments.Add(department);
                _context.SaveChanges();
            }

            if (!_context.Terminals.Any())
            {
                var terminal = new Terminal
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Server",
                    IsDefault = true,
                    ReportPrinterId = 0,
                    TransactionPrinterId = 0
                };

                _context.Terminals.Add(terminal);
                _context.SaveChanges();
            }

            if (!_context.Printers.Any())
            {
                string defaultPrinterName = PrinterHelper.GetDefaultPrinter();

                var printers = new List<Printer>
                {
                    new Printer
                    {
                        EntityGuid = Guid.NewGuid(),
                        Name = "Ticket Printer",
                        ShareName = defaultPrinterName,
                        RTLMode = false,
                        CharReplacement = ""
                    },
                    new Printer
                    {
                        EntityGuid = Guid.NewGuid(),
                        Name = "Kitchen Printer",
                        ShareName = defaultPrinterName,
                        RTLMode = false,
                        CharReplacement = ""
                    },
                    new Printer
                    {
                        EntityGuid = Guid.NewGuid(),
                        Name = "Invoice Printer",
                        ShareName = defaultPrinterName,
                        RTLMode = false,
                        CharReplacement = ""
                    },
                };

                _context.Printers.AddRange(printers);
                _context.SaveChanges();
            }
        }
    }
}
