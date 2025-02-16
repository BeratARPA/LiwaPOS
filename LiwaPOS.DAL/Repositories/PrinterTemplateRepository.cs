using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiwaPOS.DAL.Repositories
{
    public class PrinterTemplateRepository : GenericRepository<PrinterTemplate>, IPrinterTemplateRepository
    {
        public PrinterTemplateRepository(DataContext context) : base(context) { }
    }
}
