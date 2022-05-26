
using DDDWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DDDWebAPI.Infrastructure.Data
{
    public class MySqlContext : DbContext
    {

        #region Construtores
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            //ensure data base is created 
            try
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                if (databaseCreator.Exists())
                    databaseCreator.EnsureCreated();
                if (!databaseCreator.HasTables())
                    databaseCreator.CreateTables();
            }
            catch { }
        }

        #endregion

        #region Data Sets
        public DbSet<Feira> Feiras { get; set; }
        #endregion

    }
}
