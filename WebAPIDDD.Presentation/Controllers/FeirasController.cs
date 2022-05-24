using Microsoft.AspNetCore.Mvc;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Application.DTO.DTO;
using Microsoft.AspNetCore.Diagnostics;
using DDDWebAPI.Domain.Models;
using Newtonsoft.Json;

namespace WebAPIDDD.Presentation.Controllers
{
    public class FeirasController : Controller
    {
        private readonly ILogger<FeirasController> _logger;

        private readonly IApplicationServiceFeira _applicationServiceFeira;
        public FeirasController(IApplicationServiceFeira ApplicationServiceFeira, ILogger<FeirasController> logger)
        {
            _applicationServiceFeira = ApplicationServiceFeira;
            _logger = logger;

            //Init database
            if (_applicationServiceFeira.GetAll().Count() == 0)
                PopulateTable();

        }

        private void PopulateTable()
        {
            string content = System.IO.File.ReadAllText("dump.json");
            if (content != null)
            {
                FeiraDTO[] feiras = JsonConvert.DeserializeObject<FeiraDTO[]>(content);
                foreach (FeiraDTO feira in feiras)
                {
                    if (feira != null)
                        if (_applicationServiceFeira.GetByRegistro(feira.registro) == null)
                            _applicationServiceFeira.Add(feira);
                }
            }
        }
        #region Inserir
        /// <summary>
        /// Insere uma nova feira
        /// </summary>
        /// <param name="model">FeiraDTO a ser inserida</param>
        /// <returns>Se a feira foi criada ou não</returns>
        /// <remarks>ue</remarks>
        /// <response code="201">retorna a feira criada</response>
        /// <response code="422">feira nao enviada no body</response>
        /// <response code="422">feira duplicada, retorna vazio</response>
        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeiraDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] FeiraDTO model)
        {
            _logger.LogInformation("Validando se a entidade já existe");
            if (model == null)
                return NotFound();
            if (_applicationServiceFeira.GetByRegistro(model.registro) != null)
            {
                _logger.LogInformation("A feira com código de registro {0} já existe", model.registro);
                return UnprocessableEntity("Esta feira já existe.");
            }
            else
            {
                _logger.LogInformation("Tentando incluir uma feira", model);
                _applicationServiceFeira.Add(model);
                return Ok("Feira cadastrada com sucesso!");
            }
        }
        #endregion 

        #region Buscas
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
            IEnumerable<FeiraDTO> feiras_por_nome;
            _logger.LogInformation("Tentando buscar uma feira pelo nome da feira " + nome_feira);

            feiras_por_nome = _applicationServiceFeira.GetAllByNome(nome_feira);


            if (feiras_por_nome == null)
            {
                _logger.LogInformation("Nada encontrado");

                return NoContent();
            }
            else
            {
                _logger.LogInformation("Feiras retornadas " + feiras_por_nome.Count());
                return Ok(feiras_por_nome);
            }
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
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Patch([FromBody] FeiraDTO model)
        {
            if (model == null || _applicationServiceFeira.GetByRegistro(model.registro) == null)
            {
                _logger.LogInformation("FeiraDTO não existe, por isto não foi alterada.");

                return NotFound();
            }
            _logger.LogInformation("Tentando alterar a feira com código de registro " + model.registro);

            _applicationServiceFeira.Update(model);
            
            _logger.LogInformation("FeiraDTO com código de registro " + model.registro + " alterada.");
            return Ok(model);
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
            _logger.LogInformation("Tentando deletar a feira com código de registro " + registro);
            FeiraDTO obj = _applicationServiceFeira.GetByRegistro(registro);
            if (obj != null)
            {
                _logger.LogInformation("A feira existe");

                _applicationServiceFeira.Remove(obj);
                _logger.LogInformation("A feira foi deletada");

                return Accepted();
            }
            else
            {
                _logger.LogInformation("A feira não existe");

                return NoContent();
            }
        }
        #endregion

        #region Error 
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            _logger.LogInformation("Ocorreu algum erro");

            return Problem(
                detail: exceptionHandlerFeature.Error.InnerException.ToString(),

                title: exceptionHandlerFeature.Error.Message
                );

        }
        #endregion
    }
}
