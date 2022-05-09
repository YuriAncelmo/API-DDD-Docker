using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _logger.Log(LogLevel.Information, "Foi iniciada uma chamada ao endpoint");
        }

        [HttpGet]
        [Route("distrito")]
        public string Get()
        {
            //using(var context = new BancoDeDadosContext())
            //{

            //}
            return "200 ok";
        }
    }
}
