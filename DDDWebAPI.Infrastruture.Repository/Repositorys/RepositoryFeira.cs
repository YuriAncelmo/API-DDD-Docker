using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Models;
using DDDWebAPI.Infrastructure.Data;

namespace DDDWebAPI.Infrastruture.Repository.Repositorys
{
    public class RepositoryFeira : RepositoryBase<Feira>, IRepositoryFeira
    {
        private readonly MySqlContext _context;
        public RepositoryFeira(MySqlContext Context)//injeção da dependência
            : base(Context)
        {
            _context = Context;
        }
        public IEnumerable<Feira> GetAllByNome(string nome_feira)
        {
            return _context.Feiras.Where(feira => feira.nome_feira == nome_feira).ToList();
        }
        public Feira GetByRegistro(string registro)
        {
            return _context.Feiras.Where(feira => feira.registro == registro).FirstOrDefault();
        }
    }
}
