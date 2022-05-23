
using DDDWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace DDDWebAPI.Infrastructure.Data
{
    public class MySqlContext : DbContext
    {
        
        #region Construtores
        //public MySqlContext() { }
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) {
            //ensure data base is created 
            //var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
            //databaseCreator.EnsureCreated();
            //databaseCreator.CreateTables();

        }

        #endregion

        #region Data Sets
        public DbSet<Feira> Feiras { get; set; }
        #endregion

    }
}
