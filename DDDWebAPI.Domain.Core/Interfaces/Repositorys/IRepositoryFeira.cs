using DDDWebAPI.Domain.Models;
using System.Collections.Generic;
namespace DDDWebAPI.Domain.Core.Interfaces.Repositorys
{
    public interface IRepositoryFeira:IRepositoryBase<Feira>
    {
        IEnumerable<Feira> GetAllByNome(string nome_feira);
        Feira GetByRegistro(string registro);
    }
}
