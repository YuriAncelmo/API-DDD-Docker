using DDDWebAPI.Application.DTO.DTO;
using System;
using System.Collections.Generic;

namespace DDDWebAPI.Application.Interfaces
{
    public interface IApplicationServiceFeira
    {
        void Add(FeiraDTO obj);

        FeiraDTO GetByRegistro(string registro);

        IEnumerable<FeiraDTO> GetAll();
        IEnumerable<FeiraDTO> GetAllByNome(string nome);

        void Update(FeiraDTO obj);

        void Remove(FeiraDTO obj);

        void Dispose();
    }
}
