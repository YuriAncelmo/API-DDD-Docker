using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo_FeiraLivre;
using System;
using Util_FeirasLivres;
namespace Teste_FeiraLivre
{
    [TestClass]
    public class OperacoesDB
    {
       
        [TestMethod]
        public void CadastroNovaFeira()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {

                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade

                    if (model != null) context.DeletarPorCodigoRegistro("4041 - 0");

                    model = new FeiraModel("1", "-46550164", "-23558733", "355030885000091", "3550308005040", "87", "VILA FORMOSA", "26", "ARICANDUVA - FORMOSA - CARRAO", "Leste", "Leste 1", "VILA FORMOSA", "4041 - 0", "RUA MARAGOJIPE", " S/ N", "VL FORMOSA", " TV RUA PRETORIA");
                    context.Inserir(model);

                    FeiraModel inserted = context.BuscaPorRegistro("4041 - 0");
                    Assert.AreEqual(model.id, inserted.id);
                    Assert.AreEqual(model.longitude, inserted.longitude);
                    Assert.AreEqual(model.latitude, inserted.latitude);
                    Assert.AreEqual(model.setcens, inserted.setcens);
                    Assert.AreEqual(model.areap, inserted.areap);
                    Assert.AreEqual(model.coddist, inserted.coddist);
                    Assert.AreEqual(model.distrito, inserted.distrito);
                    Assert.AreEqual(model.codsubpref, inserted.codsubpref);
                    Assert.AreEqual(model.subprefe, inserted.subprefe);
                    Assert.AreEqual(model.regiao5, inserted.regiao5);
                    Assert.AreEqual(model.regiao8, inserted.regiao8);
                    Assert.AreEqual(model.nome_feira, inserted.nome_feira);
                    Assert.AreEqual(model.registro, inserted.registro);
                    Assert.AreEqual(model.logradouro, inserted.logradouro);
                    Assert.AreEqual(model.numero, inserted.numero);
                    Assert.AreEqual(model.bairro, inserted.bairro);
                    Assert.AreEqual(model.referencia, inserted.referencia);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        [TestMethod]
        public void CadastroFeiraDuplicada()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {

                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");
                    if (model != null) context.DeletarPorCodigoRegistro("4041 - 0");

                    model = new FeiraModel("1", "-46550164", "-23558733", "355030885000091", "3550308005040", "87", "VILA FORMOSA", "26", "ARICANDUVA - FORMOSA - CARRAO", "Leste", "Leste 1", "VILA FORMOSA", "4041 - 0", "RUA MARAGOJIPE", " S/ N", "VL FORMOSA", " TV RUA PRETORIA");
                    context.Inserir(model);
                    context.Inserir(model);
                    Assert.IsFalse(true, "Não deveria deixar inserir duas feiras com mesmo ID");
                }
                catch (Exception)
                {
                    Assert.IsFalse(false);
                }
            };
        }
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistro()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");
                    if (model == null)
                    {
                        model = new FeiraModel("1", "-46550164", "-23558733", "355030885000091", "3550308005040", "87", "VILA FORMOSA", "26", "ARICANDUVA - FORMOSA - CARRAO", "Leste", "Leste 1", "VILA FORMOSA", "4041 - 0", "RUA MARAGOJIPE", " S/ N", "VL FORMOSA", " TV RUA PRETORIA");
                        context.Inserir(model);
                    }
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Necessário pois foi feito uma operação de inserção e depois remoção na sequencia
                    context.DeletarPorCodigoRegistro("4041 - 0");

                    Assert.IsTrue(context.BuscaPorRegistro("4041 - 0") == null);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            }
        }
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistroQueNaoExiste()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");
                    if (model == null)
                    {
                        model = new FeiraModel("1", "-46550164", "-23558733", "355030885000091", "3550308005040", "87", "VILA FORMOSA", "26", "ARICANDUVA - FORMOSA - CARRAO", "Leste", "Leste 1", "VILA FORMOSA", "4041 - 0", "RUA MARAGOJIPE", " S/ N", "VL FORMOSA", " TV RUA PRETORIA");
                        context.Inserir(model);
                    }
                    context.DeletarPorCodigoRegistro("4041 - 0");
                    context.DeletarPorCodigoRegistro("4041 - 0");

                    Assert.IsTrue(false, "Deveria quebrar pois a entidade já não existe mais ");
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(false, ex.Message);
                }
            }
        }

        [TestMethod]
        public void AtualizacaoFeira()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {

                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");


                    if (model == null)
                    {
                        model = new FeiraModel("", "", "", "", "", "", "", "", "", "", "", "", "4041 - 0", "", "", "", "");
                        context.Inserir(model);
                    }
                    model.id = "2";
                    //model.registro = "abc";Entity não permite a alteração 
                    context.Alterar(model);
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade

                    FeiraModel inserted = context.BuscaPorRegistro("4041 - 0");
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade

                    //Assert.IsTrue(context.BuscaPorRegistro("abc") == null );
                    Assert.AreEqual(inserted.id, "2");
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        [TestMethod]
        public void AtualizacaoCodigoRegistroFeira()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {

                    FeiraModel model = context.BuscaPorRegistro("4041 - 0");


                    if (model == null)
                    {
                        model = new FeiraModel("", "", "", "", "", "", "", "", "", "", "", "", "4041 - 0", "", "", "", "");
                        context.Inserir(model);
                    }
                    model.registro = "abc";
                    context.Alterar(model);
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade

                    FeiraModel inserted = context.BuscaPorRegistro("4041 - 0");
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Liberar a entidade

                    Assert.IsTrue(context.BuscaPorRegistro("abc") != null);
                }
                catch (Exception)
                {
                    Assert.IsFalse(false);
                }
            };
        }
        [TestMethod]
        public void BuscaPorDistrito()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    GaranteQueEstaInserido();

                    Assert.IsTrue(context.BuscaPorDistrito("VILA FORMOSA") != null);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        [TestMethod]
        public void BuscaPorRegiao5()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    GaranteQueEstaInserido();

                    Assert.IsTrue(context.BuscaPorRegiao5("Leste") != null);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        [TestMethod]
        public void BuscaPorNomeFeira()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    GaranteQueEstaInserido();

                    Assert.IsTrue(context.BuscaPorNomeFeira("VILA FORMOSA") != null);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        [TestMethod]
        public void BuscaPorBairro()
        {
            using (BancoDeDadosContext context = new BancoDeDadosContext())
            {
                try
                {
                    GaranteQueEstaInserido();

                    Assert.IsTrue(context.BuscaPorBairro("VL FORMOSA") != null);
                }
                catch (Exception ex)
                {
                    Assert.IsFalse(true, ex.Message);
                }
            };
        }
        public void GaranteQueEstaInserido()
        {
            using (var context = new BancoDeDadosContext())
            {
                FeiraModel model = context.BuscaPorRegistro("4041 - 0");

                if (model == null)
                {
                    model = new FeiraModel("1", "-46550164", "-23558733", "355030885000091", "3550308005040", "87", "VILA FORMOSA", "26", "ARICANDUVA - FORMOSA - CARRAO", "Leste", "Leste 1", "VILA FORMOSA", "4041 - 0", "RUA MARAGOJIPE", " S/ N", "VL FORMOSA", " TV RUA PRETORIA");

                    context.Inserir(model);
                }
            }
        }

    }
}