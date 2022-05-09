using System;
using System.Collections.Generic;
using Modelo_FeiraLivre;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;

namespace Util_FeirasLivres
{
    public class BancoDeDadosContext : DbContext
    {
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
                entity.HasKey(e => e.id);
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
        public void Alterar(FeiraModel feira)
        {
            using (var context= this)
            {
                context.Feiras.Update(feira);
            }
        }
        public void Inserir(FeiraModel feira)
        {
            //TODO: Tratar erros de duplicação de dados 
            using (var context = this)
            {
                //Garante que a base está criada
                context.Database.EnsureCreated();

                context.Feiras.Add(feira);

                context.SaveChanges();
            }
        }
        public void InserirLote(List<FeiraModel> feiras)
        {
            using (var context = this)
            {
                //Garante que a base está criada
                context.Database.EnsureCreated();

                feiras.ForEach(feira => context.Feiras.Add(feira));

                context.SaveChanges();
            }
        }

    }
}
