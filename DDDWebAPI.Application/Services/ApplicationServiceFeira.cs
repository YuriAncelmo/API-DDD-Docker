using DDDWebAPI.Application.DTO.DTO;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Domain.Core.Interfaces.Services;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDWebAPI.Application.Services
{
    
        public class ApplicationServiceFeira : IApplicationServiceFeira
        {
            private readonly IServiceFeira _serviceFeira;
            private readonly IMapperFeira _mapperFeira;

            public ApplicationServiceFeira(IServiceFeira ServiceFeira
                                                     , IMapperFeira MapperFeira)

            {
                _serviceFeira = ServiceFeira;
                _mapperFeira = MapperFeira;
            }


            public void Add(FeiraDTO obj)
            {
                var objFeira = _mapperFeira.MapperToEntity(obj);
                _serviceFeira.Add(objFeira);
            }

            public void Dispose()
            {
                _serviceFeira.Dispose();
            }

            public IEnumerable<FeiraDTO> GetAll()
            {
                var objProdutos = _serviceFeira.GetAll();
                return _mapperFeira.MapperListFeiras(objProdutos);
            }
            public IEnumerable<FeiraDTO> GetAllByNome(string nome )
            {
                var objFeira = _serviceFeira.GetAllByNome(nome);
                return _mapperFeira.MapperListFeiras(objFeira);
            }
            public FeiraDTO GetByRegistro(string registro)
            {
                var objFeira = _serviceFeira.GetByRegistro(registro);
                return _mapperFeira.MapperToDTO(objFeira);
            }

            public void Remove(FeiraDTO obj)
            {
                var objFeira = _mapperFeira.MapperToEntity(obj);
                _serviceFeira.Remove(objFeira);
            }

            public void Update(FeiraDTO obj)
            {
                var objFeira = _mapperFeira.MapperToEntity(obj);
                _serviceFeira.Update(objFeira);
            }
        }
    }
