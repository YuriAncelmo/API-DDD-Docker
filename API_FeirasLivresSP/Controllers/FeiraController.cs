using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modelo_FeiraLivre;
using Serilog;
using System;
using System.Collections.Generic;
using Util_FeirasLivres;

namespace API_FeirasLivresSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeiraController : ControllerBase
    {
        private readonly ILogger<FeiraController> _logger;

        public FeiraController(ILogger<FeiraController> logger)
        {
            _logger = logger;
        }

        #region Inserir
        [HttpPost]
        public string Post([FromBody] FeiraModel model)
        {
            using (var context = new BancoDeDadosContext())
            {
                try
                {
                    context.Inserir(model);
                    return "Ok";
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 422; //https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/422
                    _logger.LogError(ex.Message, ex.InnerException);
                    return ex.Message + ", Exceção interna: " + ex.InnerException;
                }
            }
        }
        #endregion 

        #region Buscas
        [HttpGet]
        [Route("distrito/{nome_distrito}")]
        public List<FeiraModel> GetDistrito([FromRoute] string nome_distrito)
        {
            List<FeiraModel> feiras_por_distrito = new List<FeiraModel>();
            using (var context = new BancoDeDadosContext())
            {
                feiras_por_distrito = context.BuscaPorDistrito(nome_distrito);
            }
            if (feiras_por_distrito.Count == 0)
                Response.StatusCode = 204;
            _logger.LogInformation("Feiras retornadas " + feiras_por_distrito.Count);
            return feiras_por_distrito;
        }

        [HttpGet]
        [Route("bairro/{nome_bairro}")]
        public List<FeiraModel> GetBairro([FromRoute] string nome_bairro)
        {
            List<FeiraModel> feiras_por_bairro = new List<FeiraModel>();
            using (var context = new BancoDeDadosContext())
            {
                feiras_por_bairro = context.BuscaPorBairro(nome_bairro);
            }
            if (feiras_por_bairro.Count == 0)
                Response.StatusCode = 204;
            _logger.LogInformation("Feiras retornadas " + feiras_por_bairro.Count);

            return feiras_por_bairro;
        }

        [HttpGet]
        [Route("regiao5/{regiao5}")]
        public List<FeiraModel> GetRegiao5([FromRoute] string regiao5)
        {
            List<FeiraModel> feiras_por_regiao5 = new List<FeiraModel>();
            using (var context = new BancoDeDadosContext())
            {
                feiras_por_regiao5 = context.BuscaPorRegiao5(regiao5);
            }
            if (feiras_por_regiao5.Count == 0)
                Response.StatusCode = 204;
            _logger.LogInformation("Feiras retornadas " + feiras_por_regiao5.Count);

            return feiras_por_regiao5;
        }

        [HttpGet]
        [Route("nome/{nome_feira}")]
        public List<FeiraModel> GetNomeFeira([FromRoute] string nome_feira)
        {
            List<FeiraModel> feiras_por_nome = new List<FeiraModel>();
            using (var context = new BancoDeDadosContext())
            {
                feiras_por_nome = context.BuscaPorNomeFeira(nome_feira);
            }
            if (feiras_por_nome.Count == 0)
                Response.StatusCode = 204;
            _logger.LogInformation("Feiras retornadas " + feiras_por_nome.Count);

            return feiras_por_nome;
        }
        #endregion

        #region Alteração
        [HttpPatch]
        public void Patch([FromBody] FeiraModel model)
        {
            using (var context = new BancoDeDadosContext())
            {
                try
                {
                    int linhasafetadas = context.Alterar(model);
                    if (linhasafetadas == 0)
                        Response.StatusCode = 404;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex.InnerException);

                }
            }
        }
        #endregion

        #region Remover
        [HttpDelete]
        [Route("{registro}")]
        public void Delete([FromRoute] string registro)
        {
            using (var context = new BancoDeDadosContext())
            {
                try
                {
                    context.DeletarPorCodigoRegistro(registro);
                }
                catch (DbUpdateConcurrencyException)
                {
                    Response.StatusCode = 200;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex.InnerException);

                    throw;//pode ser um ponto de vulnerabilidade.
                }

            }
        }
        #endregion 
    }
}
