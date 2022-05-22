
using DDDWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DDDWebAPI.Infrastructure.Data
{
    public class MySqlContext : DbContext
    {
        
        #region Construtores
        public MySqlContext() { }
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

        #endregion

        #region Data Sets
        public DbSet<Feira> Feiras { get; set; }
        #endregion

    }
}
