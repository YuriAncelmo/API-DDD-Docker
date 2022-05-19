using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Modelo_FeiraLivre;
using System.Collections.Generic;
using Util_FeirasLivres;

namespace API_FeirasLivresSP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeiraController : ControllerBase
    {
        #region Propriedades
        private readonly ILogger<FeiraController> _logger;
        private readonly BancoDeDadosContext _context;
        #endregion

        #region Construtores
        public FeiraController(IConfiguration _configuration, ILogger<FeiraController> logger)
        {
            var host = _configuration["DBHOST"] ?? "localhost";
            var port = _configuration["DBPORT"] ?? "3306";
            var password = _configuration["MYSQL_PASSWORD"] ?? _configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = _configuration["MYSQL_USER"] ?? _configuration.GetConnectionString("MYSQL_USER");
            var usersDataBase = _configuration["MYSQL_DATABASE"] ?? _configuration.GetConnectionString("MYSQL_DATABASE");

            string connString = $"server={host}; userid={userid};pwd={password};port={port};database={usersDataBase}";

            _context = new BancoDeDadosContext(connString);
            _logger = logger;
        }
        #endregion 

        #region Inserir
        /// <summary>
        /// Insere uma nova feira
        /// </summary>
        /// <param name="model">Feira a ser inserida</param>
        /// <returns>Se a feira foi criada ou não</returns>
        /// <remarks>ue</remarks>
        /// <response code="201">retorna a feira criada</response>
        /// <response code="422">feira duplicada, retorna vazio</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeiraModel))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] FeiraModel model)
        {
            _logger.LogInformation("Validando se a entidade já existe");
            if (_context.FeiraExiste(model))
            {
                _logger.LogInformation("A feira com código de registro {0} já existe", model.registro);
                return UnprocessableEntity("Esta feira já existe.");
            }
            else
            {
                _logger.LogInformation("Tentando incluir uma feira", model);
                _context.Inserir(model);
                return Ok(model);
            }
        }
        #endregion 

        #region Buscas
        /// <summary>
        /// Busca uma feira pelo nome do distrito
        /// </summary>
        /// <param name="nome_distrito">nome do distrito que será utilizado para a busca</param>
        /// <returns>As feiras que foram encontradas naquele distrito</returns>
        /// <response code="200">Retorna as feiras encontradas</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Produces("application/json")]
        [Route("distrito/{nome_distrito}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDistrito([FromRoute] string nome_distrito)
        {
            List<FeiraModel> feiras_por_distrito = new List<FeiraModel>();
            _logger.LogInformation("Tentando buscar uma feira pelo distrito " + nome_distrito);

            feiras_por_distrito = _context.BuscaPorDistrito(nome_distrito);

            _logger.LogInformation("Feiras retornadas " + feiras_por_distrito.Count);

            if (feiras_por_distrito.Count == 0)
                return NoContent();
            else
                return Ok(feiras_por_distrito);

        }
        /// <summary>
        /// Busca uma feira pelo nome do bairro
        /// </summary>
        /// <param name="nome_bairro">nome do bairro que será utilizado para a busca</param>
        /// <returns>As feiras que foram encontradas naquele bairro </returns>
        /// <response code="200">Retorna as feiras encontradas</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("bairro/{nome_bairro}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBairro([FromRoute] string nome_bairro)
        {
            List<FeiraModel> feiras_por_bairro = new List<FeiraModel>();
            _logger.LogInformation("Tentando buscar uma feira pelo bairro " + nome_bairro);

            feiras_por_bairro = _context.BuscaPorBairro(nome_bairro);

            _logger.LogInformation("Feiras retornadas " + feiras_por_bairro.Count);

            if (feiras_por_bairro.Count == 0)
                return NoContent();
            else
                return Ok(feiras_por_bairro);
        }
        /// <summary>
        /// Busca uma feira pela regiao5
        /// </summary>
        /// <param name="regiao5">regiao5 que será utilizado para a busca</param>
        /// <returns>As feiras que foram encontradas naquela regiao5</returns>
        /// <response code="200">Retorna as feiras encontradas</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("regiao5/{regiao5}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult GetRegiao5([FromRoute] string regiao5)
        {

            List<FeiraModel> feiras_por_regiao5 = new List<FeiraModel>();
            _logger.LogInformation("Tentando buscar uma feira pela regiao5 " + regiao5);


            feiras_por_regiao5 = _context.BuscaPorRegiao5(regiao5);

            _logger.LogInformation("Feiras retornadas " + feiras_por_regiao5.Count);

            if (feiras_por_regiao5.Count == 0)
                return NoContent();
            else
                return Ok(feiras_por_regiao5);
        }
        /// <summary>
        /// Busca uma feira pelo nome da feira
        /// </summary>
        /// <param name="nome_feira">nome da feira que será utilizado para a busca</param>
        /// <returns>As feiras que foram encontradas com aquele nome </returns>
        /// <response code="200">Retorna as feiras encontradas</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("nome/{nome_feira}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetNomeFeira([FromRoute] string nome_feira)
        {
            List<FeiraModel> feiras_por_nome = new List<FeiraModel>();
            _logger.LogInformation("Tentando buscar uma feira pelo nome da feira " + nome_feira);

            feiras_por_nome = _context.BuscaPorNomeFeira(nome_feira);

            _logger.LogInformation("Feiras retornadas " + feiras_por_nome.Count);

            if (feiras_por_nome.Count == 0)
                return NoContent();
            else
                return Ok(feiras_por_nome);
        }
        #endregion

        #region Alteração
        /// <summary>
        /// Atualiza uma feira pelo codigo de registro
        /// </summary>
        /// <param name="model">feira que será atualizada</param>
        /// <returns>As feiras que foram encontradas com aquele nome </returns>
        /// <response code="200">Retorna que a feira foi alterada</response>
        /// <response code="404">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult Patch([FromBody] FeiraModel model)
        {
            int linhasafetadas = _context.Alterar(model);
            if (linhasafetadas == 0)
                return NotFound();
            else return Ok(model);
        }
        #endregion

        #region Remover
        /// <summary>
        /// Remove uma feira pelo codigo de registro
        /// </summary>
        /// <param name="registro">código de registro da feira que será removida</param>
        /// <returns>Se foi deletado</returns>
        /// <response code="202">Ok</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [Route("{registro}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] string registro)
        {
            if (_context.FeiraExiste(registro))
            {
                _context.DeletarPorCodigoRegistro(registro);
                return Accepted();
            }
            else
                return NoContent();

        }
        #endregion

        #region Error 
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.InnerException.ToString(),

                title: exceptionHandlerFeature.Error.Message);

        }
        #endregion
    }
}
