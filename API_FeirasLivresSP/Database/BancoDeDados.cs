using System;
using System.Collections.Generic;
using Modelo_FeiraLivre;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;


namespace Util_FeirasLivres
{
    public class BancoDeDadosContext : DbContext
    {
        #region Propriedades
        public DbSet<FeiraModel> Feiras { get; set; }
        private string _connectionString;
        #endregion
        
        #region Construtores
        private BancoDeDadosContext() { }//Não instancia sem string de conexão
        public BancoDeDadosContext(string connectionString)
        {
            this._connectionString = connectionString;
            try//Cria as tabelas se necessário
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.EnsureCreated();
                databaseCreator.CreateTables();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {//Se cair aqui, quer dizer que as tabelas já existem ;)
            }
        }
        #endregion
        
        #region Estruturas Auxiliares
        private struct StringConexao
        {
            public string server { get; set; }
            public string port { get; set; }
            public string database { get; set; }
            public string uid { get; set; }
            public string password { get; set; }
        }
        #endregion

        #region Configurações com Banco de Dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FeiraModel>(entity =>
            {
                entity.HasKey(e => e.registro);
                entity.Property(e => e.id);
                entity.Property(e => e.numero).IsRequired();//Não há nenhum tipo de especificação se os dados são obrigatórios, porém , deixarei apenas o nome da feira e o endereco 
                entity.Property(e => e.subprefe);
                entity.Property(e => e.areap);
                entity.Property(e => e.bairro).IsRequired();
                entity.Property(e => e.coddist);
                entity.Property(e => e.codsubpref);
                entity.Property(e => e.distrito);
                entity.Property(e => e.latitude).IsRequired();
                entity.Property(e => e.logradouro);
                entity.Property(e => e.longitude).IsRequired();
                entity.Property(e => e.nome_feira).IsRequired();
                entity.Property(e => e.numero).IsRequired();
                entity.Property(e => e.referencia);
                entity.Property(e => e.regiao5);
                entity.Property(e => e.regiao8);
                entity.Property(e => e.setcens);
                entity.Property(e => e.subprefe);
            });
        }
        #endregion
        
        #region Inclusão
        public int Inserir(FeiraModel feira)
        {
            //Garante que a base está criada
            this.Database.EnsureCreated();

            this.Feiras.Add(feira);
            try
            {
                return this.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InserirLote(List<FeiraModel> feiras)
        {
            //Garante que a base está criada
            this.Database.EnsureCreated();

            feiras.ForEach(feira => this.Feiras.Add(feira));

            this.SaveChanges();

        }
        #endregion
        
        #region Remover
        public void DeletarPorCodigoRegistro(string codigo_registro)
        {
            try
            {
                FeiraModel feira = new FeiraModel() { registro = codigo_registro };
                this.Feiras.Attach(feira);
                this.Feiras.Remove(feira);
                this.SaveChanges();
                this.Entry(feira).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Necessário pois foi feito uma operação de inserção e depois remoção na sequencia
            }
            catch (Exception) { throw; }
        }
        #endregion
        
        #region Alteração 
        public int Alterar(FeiraModel feira)
        {
            try
            {
                var entity = this.Feiras.SingleOrDefault(o => o.registro == feira.registro);
                //entity.registro = feira.registro; Não deve ter alteração 
                if (entity == null)
                    return 0;
                entity.id = feira.id;
                entity.numero = feira.numero;
                entity.subprefe = feira.subprefe;
                entity.areap = feira.areap;
                entity.bairro = feira.bairro;
                entity.coddist = feira.coddist;
                entity.codsubpref = feira.codsubpref;
                entity.distrito = feira.distrito;
                entity.latitude = feira.latitude;
                entity.logradouro = feira.logradouro;
                entity.longitude = feira.longitude;
                entity.nome_feira = feira.nome_feira;
                entity.numero = feira.numero;

                entity.referencia = feira.referencia;
                entity.regiao5 = feira.regiao5;
                entity.regiao8 = feira.regiao8;
                entity.setcens = feira.setcens;
                entity.subprefe = feira.subprefe;

                this.Entry(entity).Property(c => c.registro).IsModified = false;//Duplo check para não alterar mesmo a chave, apesar de o entity já fazer este trabalho 
                                                                                //this.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Necessário pois foi feito uma operação de inserção e depois remoção na sequencia

                this.Feiras.Update(entity);
                return this.SaveChanges();
            }
            catch (Exception) { throw; }
        }
        #endregion
        
        #region Busca
        public FeiraModel BuscaPorRegistro(string codigo_registro)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.registro == codigo_registro
                                  select feira;

                return feira_Model.FirstOrDefault<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public List<FeiraModel> BuscaPorDistrito(string distrito)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.distrito == distrito
                                  select feira;

                return feira_Model.ToList<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public List<FeiraModel> BuscaPorRegiao5(string regiao5)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.regiao5 == regiao5
                                  select feira;

                return feira_Model.ToList<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public List<FeiraModel> BuscaPorNomeFeira(string nome_feira)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.nome_feira == nome_feira
                                  select feira;

                return feira_Model.ToList<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public List<FeiraModel> BuscaPorBairro(string bairro)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.bairro == bairro
                                  select feira;

                return feira_Model.ToList<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public bool FeiraExiste(FeiraModel model)
        {
            try
            {
                return this.Feiras.Where(o => o.registro == model.registro).Count() > 0;
            }
            catch
            {
                throw;
            }
        }
        public bool FeiraExiste(string registro)
        {
            try
            {
                return this.Feiras.Where(o => o.registro == registro).Count() > 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
