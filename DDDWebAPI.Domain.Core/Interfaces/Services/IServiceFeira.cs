
using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Models;

namespace DDDWebAPI.Domain.Core.Interfaces.Services
{
    public interface IServiceFeira:IServiceBase<Feira>
    {
        IEnumerable<Feira> GetAllByNome(string nome);
        Feira GetByRegistro(string registro);

    }

}
