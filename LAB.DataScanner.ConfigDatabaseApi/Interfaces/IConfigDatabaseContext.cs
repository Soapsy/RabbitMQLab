using Microsoft.EntityFrameworkCore;
using LAB.DataScanner.ConfigDatabaseApi.Models;

namespace LAB.DataScanner.ConfigDatabaseApi.Interfaces
{
    interface IConfigDatabaseContext
    {
            DbSet<ApplicationInstance> ApplicationInstances { get; set; }
            DbSet<ApplicationType> ApplicationTypes { get; set; }
            DbSet<Binding> Bindings { get; set; }
        
    }
}
