using LiwaPOS.DAL.Context;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Extensions;
using LiwaPOS.Shared.Helpers;
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
                Console.WriteLine("MDF dosyası bulunamadı. Mevcut veritabanı varsa siliniyor ve yeniden oluşturuluyor...");
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
                    Console.WriteLine($"Hata: {ex.Message}");
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

            Console.WriteLine("Veritabanı başarıyla yeniden oluşturuldu.");
        }

        private void SeedData()
        {
            if (!_context.UserRoles.Any())
            {
                var userRole = new UserRole
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Admin"
                };

                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var user = new User
                {
                    EntityGuid = Guid.NewGuid(),
                    Name = "Admin",
                    PinCode = "1234",
                    UserRole = _context.UserRoles.FirstOrDefault(x => x.Name == "Admin")
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }
    }
}
