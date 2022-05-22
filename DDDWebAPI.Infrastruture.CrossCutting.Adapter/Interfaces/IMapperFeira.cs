using DDDWebAPI.Application.DTO.DTO;
using DDDWebAPI.Domain.Models;
using System.Collections.Generic;

namespace DDDWebAPI.Infrastruture.CrossCutting.Adapter.Interfaces
{
    public interface IMapperFeira
    {
        #region Mappers

        Feira MapperToEntity(FeiraDTO clienteDTO);

        IEnumerable<FeiraDTO> MapperListFeiras(IEnumerable<Feira> clientes);

        FeiraDTO MapperToDTO(Feira Cliente);

        #endregion
    }
}
