using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class PrinterRepository : GenericRepository<Printer>, IPrinterRepository
    {
        public PrinterRepository(DataContext context) : base(context) { }
    }
}
