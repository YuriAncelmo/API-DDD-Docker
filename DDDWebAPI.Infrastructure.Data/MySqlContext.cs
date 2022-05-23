
using DDDWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;

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
                databaseCreator.EnsureCreated();
                databaseCreator.CreateTables();
                populateTables();

            }
            catch { }
        }

        private void populateTables()
        {
            string content = File.ReadAllText("dump.json");
            if (content != null)
            {
                Feira[] feiras = JsonConvert.DeserializeObject<Feira[]>(content);
                Feiras.AddRange(feiras);
                SaveChanges();
            }
        }

        #endregion

        #region Data Sets
        public DbSet<Feira> Feiras { get; set; }
        #endregion

    }
}
