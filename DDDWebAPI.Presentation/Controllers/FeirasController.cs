using Microsoft.AspNetCore.Mvc;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Application.DTO.DTO;
using Microsoft.AspNetCore.Diagnostics;
using DDDWebAPI.Domain.Models;
using Newtonsoft.Json;

namespace DDDWebAPI.Presentation.Controllers
{
    [Route("feira")]
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
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT feira/
        ///     {
        ///       "registro": "4044-3",
        ///       "id": "1234",
        ///       "longitude": "-12312312",
        ///       "latitude": "-12312312",
        ///       "setcens": "gabryur",
        ///       "areap": "jd umarizal",
        ///       "coddist": "1234",
        ///       "distrito": "São Paulo",
        ///       "codsubpref": "43312",
        ///       "subprefe": "Campo limpo",
        ///       "regiao5": "12334",
        ///       "regiao8": "123345",
        ///       "nome_feira": "Campo Limpo",
        ///       "logradouro": "Fundo",
        ///       "numero": "43211",
        ///       "bairro": "Jd Umarizal",
        ///       "referencia": "Poste cinza"
        ///     }
        /// <param name="model">FeiraDTO a ser inserida</param>
        /// <returns>Se a feira foi criada ou não</returns>
        /// <remarks>ue</remarks>
        /// <response code="200">Feira criada</response>
        /// <response code="400">feira nao enviada no body</response>
        /// <response code="422">feira enviada com problemas, veja a mensagem de erro</response>
        /// <response code="500">erro desconhecido</response>
        [HttpPost]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FeiraDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FeiraDTO> Post([FromBody] FeiraDTO model)
        {
            _logger.LogInformation("Validando se a entidade já existe");
            
            if (model == null)
                return BadRequest("Feira inválida");
            if (model.registro == null)
                return UnprocessableEntity("É necessário ter um registro para cadastrar");
            
            if (_applicationServiceFeira.GetByRegistro(model.registro) != null)
            {
                _logger.LogInformation("A feira com código de registro {0} já existe", model.registro);
                return UnprocessableEntity("Esta feira já existe");
            }
            else
            {
                _logger.LogInformation("Tentando incluir uma feira", model);
                _applicationServiceFeira.Add(model);
                return Ok(model);
            }
        }
        #endregion 

        #region Buscas
        /// <summary>
        /// Busca uma feira pelo nome 
        /// </summary>
        /// <param name="nome_feira">Nome da feira que será utilizado para a busca</param>
        /// <returns>As feiras que foram encontradas com aquele nome </returns>
        /// <response code="200">Retorna as feiras encontradas</response>
        /// <response code="204">Nenhuma feira foi encontrada</response>
        /// <response code="400">Requisição mal formada,verifique a mensagem de erro</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("nome/{nome_feira}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FeiraDTO>> GetNomeFeira([FromRoute] string nome_feira)
        {
            if (nome_feira == null || nome_feira == "")
                return BadRequest("Informe um nome de feira");

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
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH feira/
        ///     {
        ///       "registro": "4044-3",
        ///       "id": "1233123",
        ///       "longitude": "-12312399",
        ///       "latitude": "-12312399",
        ///       "setcens": "cadacal",
        ///       "areap": "vila santa rosa",
        ///       "coddist": "123",
        ///       "distrito": "Mococa",
        ///       "codsubpref": "pref",
        ///       "subprefe": "taboao",
        ///       "regiao5": "1233",
        ///       "regiao8": "12334",
        ///       "nome_feira": "Santa Rosa",
        ///       "logradouro": "Fundo",
        ///       "numero": "432",
        ///       "bairro": "Santa rosa",
        ///       "referencia": "Casa azul"
        ///     }
        /// </remarks>
        /// <param name="model">feira que será atualizada</param>
        /// <returns>As feiras que foram encontradas com aquele nome </returns>
        /// <response code="200">Retorna que a feira foi alterada</response>
        /// <response code="400">Requisição mal formada</response>
        /// <response code="404">Nenhuma feira foi encontrada</response>
        /// <response code="500">Erro interno</response>
        [HttpPatch]
        [Route("")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Patch([FromBody] FeiraDTO model)
        {
            if (model == null)
                return BadRequest();

            if ( _applicationServiceFeira.GetByRegistro(model.registro) == null)
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
        /// <response code="400">Requisição mal formada</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [Route("{registro}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete([FromRoute] string registro)
        {
            if (registro == null || registro == "")
                return BadRequest();

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
