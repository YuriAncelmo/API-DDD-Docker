using System;
using System.Collections.Generic;
using Modelo_FeiraLivre;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace Util_FeirasLivres
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BancoDeDadosContext:DbContext
    {
        public DbSet<FeiraModel> Feiras { get; set; }
        protected override void OnConfiguring(DbContextOptions<BancoDeDadosContext> optionsBuilder)
        {

            optionsBuilder.Use(
                @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True");
        }
        private MySqlConnection Conectar()
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = "Server=localhost; Port=3306; Database=Unico; Uid=root; Pwd=admin123;";
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Teve um problema na conexão com banco de dados ... É melhor você reiniciar o aplicativo quando resolver este problema. Detalhes: " + ex.Message);
                return null;
            }
        }
        public bool CriarTabela()
        {
            using (MySqlConnection connection = Conectar())
                try
                {
                    MySqlCommand criacaoTabela = new MySqlCommand(
                        "use Unico;" +
                        "CREATE TABLE Feiras(           " +
                        "           ID int,             " +
                        "    LONGITUDE varchar(50),     " +
                        "     LATITUDE varchar(50),     " +
                        "      SETCENS varchar(255),    " +
                        "        AREAP varchar(255),    " +
                        "      CODDIST varchar(255),    " +
                        "     DISTRITO varchar(255),    " +
                        "   CODSUBPREF varchar(255),    " +
                        "     SUBPREFE varchar(255),    " +
                        "      REGIAO5 varchar(255),    " +
                        "      REGIAO8 varchar(255),    " +
                        "   NOME_FEIRA varchar(255),    " +
                        "     REGISTRO varchar(255),    " +
                        "   LOGRADOURO varchar(255),    " +
                        "       NUMERO varchar(255),    " +
                        "       BAIRRO varchar(255),    " +
                        "   REFERENCIA varchar(255)     " +
                        "); ", connection);
                    criacaoTabela.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public void InserirRegistros(List<FeiraModel> linhas)
        {
            string query = "INSERT INTO FEIRAS (ID,LONGITUDE,LATITUDE,SETCENS,AREAP,CODDIST,DISTRITO,CODSUBPREF,SUBPREFE,REGIAO5,REGIAO8,NOME_FEIRA,REGISTRO,LOGRADOURO,NUMERO,BAIRRO,REFERENCIA) ";
            query  += "VALUES ";
            for(int i = 0;i<linhas.Count;i++ )
            {
                query += linhas[i].transformarParaInsert();
                if (i == linhas.Count - 1)//Ultimo registro
                    query += ";";
                else
                    query += ",";
            }
            using (MySqlConnection connection = Conectar())
            {
                MySqlCommand command = new MySqlCommand(query,connection);
                command.ExecuteNonQuery();
            }
        }

        public void ContarRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
