using DDDWebAPI.Application.DTO.DTO;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Models;
using DDDWebAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using DDDWebAPI.Presentation.Controllers;

namespace Teste_FeiraLivre
{
    [TestClass]
    public class ControllerTests
    {
        #region Cadastros 
        [TestMethod]
        public void CadastroNovaFeira()
        {
            // Arrange
            FeiraDTO feira = new FeiraDTO();
            feira.registro = "test";

            var mockRepo = new Mock<IApplicationServiceFeira>();
            var mockLog = new Mock<ILogger<FeirasController>>();
            var controller = new FeirasController(mockRepo.Object, mockLog.Object);
            var model = new FeiraDTO { registro = feira.registro };

            // Act
            ActionResult<FeiraDTO> result = controller.Post(model);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        [TestMethod]
        public void CadastroFeiraDuplicada()
        {
            FeiraDTO model = new FeiraDTO();
            model.registro = "test";

            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetByRegistro("test")).Returns(model);

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Post(model);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(UnprocessableEntityObjectResult));
        }
        [TestMethod]
        public void CadastroFeiraNula()
        {
            FeiraDTO model = null;

            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Post(model);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void CadastroFeiraSemRegistro()
        {
            FeiraDTO model = new FeiraDTO();

            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Post(model);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(UnprocessableEntityObjectResult));
        }
        #endregion

        #region Busca
        [TestMethod]
        public void BuscaPorNome()
        {
            List<FeiraDTO> model = new()
            {
                new FeiraDTO() { registro = "test", nome_feira = "liberdade" },
                new FeiraDTO() { registro = "test1", nome_feira = "liberdade" },
                new FeiraDTO() { registro = "test2", nome_feira = "liberdade" },
                new FeiraDTO() { registro = "test3", nome_feira = "liberdade" },
                new FeiraDTO() { registro = "test4", nome_feira = "liberdade" },
                new FeiraDTO() { registro = "test5", nome_feira = "liberdade" },
            };

            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetAllByNome("liberdade")).Returns(model);

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<IEnumerable<FeiraDTO>> result = controller.GetNomeFeira("liberdade");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        [TestMethod]
        public void BuscaPorNomeVazio()
        {
            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<IEnumerable<FeiraDTO>> result = controller.GetNomeFeira("");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void BuscaPorNomeNulo()
        {
            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<IEnumerable<FeiraDTO>> result = controller.GetNomeFeira(null);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void BuscaPorNomeRetornaVazio()
        {
            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);
            mockRepo.Setup(repo => repo.GetAllByNome("Mocoquinha")).Returns((IEnumerable<FeiraDTO>)null);

            // Act
            ActionResult<IEnumerable<FeiraDTO>> result = controller.GetNomeFeira(null);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
        #endregion

        #region Deletar
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistroVazio()
        {
            string registro = "";
            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Delete(registro);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));

        }
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistroNulo()
        {
            string registro = null;
            var mockRepo = new Mock<IApplicationServiceFeira>();

            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Delete(registro);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));

        }
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistro()
        {
            FeiraDTO model = new FeiraDTO() { registro = "test" };
            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetByRegistro(model.registro)).Returns(model);
            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Delete(model.registro);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(AcceptedResult));

        }
        [TestMethod]
        public void DeletarFeiraPorCodigoRegistroQueNaoExiste()
        {
            string registro = "test";
            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetByRegistro(registro)).Returns((FeiraDTO)null);
            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Delete(registro);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));

        }
        #endregion

        #region Alteração
        [TestMethod]
        public void AtualizacaoFeiraNaoExiste()
        {
            FeiraDTO feira = new FeiraDTO() { registro = "test" , id="id"};
            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetByRegistro(feira.registro)).Returns((FeiraDTO)null);
            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Patch(feira);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));

        }
        [TestMethod]
        public void AtualizacaoFeiraNula()
        {
            var mockRepo = new Mock<IApplicationServiceFeira>();
            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Patch(null);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));

        }
        [TestMethod]
        public void AtualizacaoFeira()
        {
            FeiraDTO feira = new FeiraDTO() { registro = "test", id = "id" };
            var mockRepo = new Mock<IApplicationServiceFeira>();
            mockRepo.Setup(repo => repo.GetByRegistro(feira.registro)).Returns(feira);
            var mockLog = new Mock<ILogger<FeirasController>>();

            var controller = new FeirasController(mockRepo.Object, mockLog.Object);

            // Act
            ActionResult<FeiraDTO> result = controller.Patch(feira);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
        #endregion

    }
}