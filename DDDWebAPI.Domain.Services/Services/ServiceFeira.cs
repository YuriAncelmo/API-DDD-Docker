
using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Core.Interfaces.Services;
using DDDWebAPI.Domain.Models;

namespace DDDWebAPI.Domain.Services.Services
{
    public class ServiceFeira : ServiceBase<Feira>, IServiceFeira
    {
        public readonly IRepositoryFeira _repositoryFeira;

        public ServiceFeira(IRepositoryFeira RepositoryFeira)
            : base(RepositoryFeira)
        {
            _repositoryFeira = RepositoryFeira;
        }

        public IEnumerable<Feira> GetAllByNome(string nome)
        {
           return _repositoryFeira.GetAllByNome(nome);
        }

        public Feira GetByRegistro(string registro)
        {
            return _repositoryFeira.GetByRegistro(registro);
        }
        
    }
}
