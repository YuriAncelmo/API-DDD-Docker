﻿using System;
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
        public BancoDeDadosContext()
        {
            try//Cria as tabelas se necessário
            {
                var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
                databaseCreator.CreateTables();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {//Se cair aqui, quer dizer que as tabelas já existem;)
            }
        }

        public DbSet<FeiraModel> Feiras { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=Unico;user=root;password=admin123");//Configurando para usar MySQL
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
        public void Inserir(FeiraModel feira)
        {
            //Garante que a base está criada
            this.Database.EnsureCreated();

            this.Feiras.Add(feira);
            try
            {
                this.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //public void Deletar(string id_feira)
        //{
        //    try
        //    {
        //        FeiraModel feira = new FeiraModel() { id = id_feira };
        //        this.Feiras.Attach(feira);
        //        this.Feiras.Remove(feira);
        //        this.SaveChanges();
        //        //this.Entry(feira).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Necessário pois foi feito uma operação de inserção e depois remoção na sequencia

        //    }
        //    catch { throw; }
        //}
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
            catch { throw; }
        }
        public void Alterar(FeiraModel feira)
        {
            try
            {
                this.Entry(feira).Property(c => c.registro).IsModified = false;
                this.Feiras.Update(feira);
                this.SaveChanges();
            }
            catch { throw; }
        }
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
        public FeiraModel BuscaPorDistrito(string distrito)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.distrito == distrito
                                  select feira;

                return feira_Model.FirstOrDefault<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public FeiraModel BuscaPorRegiao5(string regiao5)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.regiao5  == regiao5
                                  select feira;

                return feira_Model.FirstOrDefault<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public FeiraModel BuscaPorNomeFeira(string nome_feira)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.nome_feira == nome_feira
                                  select feira;

                return feira_Model.FirstOrDefault<FeiraModel>();
            }
            catch
            {
                throw;
            }
        }
        public FeiraModel BuscaPorBairro(string bairro)
        {
            try
            {
                var feira_Model = from feira in this.Feiras
                                  where feira.bairro == bairro
                                  select feira;

                return feira_Model.FirstOrDefault<FeiraModel>();
            }
            catch
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

    }
}