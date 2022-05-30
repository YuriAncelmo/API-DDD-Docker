using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Models;
using DDDWebAPI.Infrastructure.Data;
using System.Reflection;

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
       
        
        public override void Update(Feira feira)
        {
            //TODO: base.Update(feira);Não sei por que não funciona
            try
            {
                var entity = _context.Feiras.SingleOrDefault(o => o.registro == feira.registro);
                //entity.registro = feira.registro; Não deve ter alteração 
                if (entity == null)
                    return;
                entity.id = feira.id;
                entity.numero = feira.numero;
                entity.subprefe = feira.subprefe;
                entity.areap = feira.areap;
                entity.bairro = feira.bairro;
                entity.coddist = feira.coddist;
                entity.codsubpref = feira.codsubpref;
                entity.distrito = feira.distrito;
                entity.latitude = feira.latitude;
                entity.logradouro = feira.logradouro;
                entity.longitude = feira.longitude;
                entity.nome_feira = feira.nome_feira;
                entity.numero = feira.numero;

                entity.referencia = feira.referencia;
                entity.regiao5 = feira.regiao5;
                entity.regiao8 = feira.regiao8;
                entity.setcens = feira.setcens;
                entity.subprefe = feira.subprefe;

                _context.Entry(entity).Property(c => c.registro).IsModified = false;//Duplo check para não alterar mesmo a chave, apesar de o entity já fazer este trabalho 
                                                                                //this.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;//Necessário pois foi feito uma operação de inserção e depois remoção na sequencia

                _context.Feiras.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception) { throw; }
        }
    }
}
