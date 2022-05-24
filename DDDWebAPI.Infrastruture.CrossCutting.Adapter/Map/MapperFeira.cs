using DDDWebAPI.Application.DTO.DTO;
using DDDWebAPI.Domain.Models;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Interfaces;


namespace DDDWebAPI.Infrastruture.CrossCutting.Adapter.Map
{
    public class MapperFeira : IMapperFeira
    {

        #region properties

        List<FeiraDTO> feirasDTOs = new List<FeiraDTO>();

        #endregion


        #region methods

        public Feira MapperToEntity(FeiraDTO feiraDTO)
        {
            Feira feira = new Feira
            {

                id = feiraDTO.id,
                numero = feiraDTO.numero,
                subprefe = feiraDTO.subprefe,
                areap = feiraDTO.areap,
                bairro = feiraDTO.bairro,
                coddist = feiraDTO.coddist,
                codsubpref = feiraDTO.codsubpref,
                distrito = feiraDTO.distrito,
                latitude = feiraDTO.latitude,
                logradouro = feiraDTO.logradouro,
                longitude = feiraDTO.longitude,
                nome_feira = feiraDTO.nome_feira,
                registro = feiraDTO.registro,
                referencia = feiraDTO.referencia,
                regiao5 = feiraDTO.regiao5,
                regiao8 = feiraDTO.regiao8,
                setcens = feiraDTO.setcens,
            };
            return feira;

        }


        public IEnumerable<FeiraDTO> MapperListFeiras(IEnumerable<Feira> feiras)
        {
            foreach (var item in feiras)
            {
                FeiraDTO feiraDTO = new FeiraDTO
                {
                    id = item.id,
                    numero = item.numero,
                    subprefe = item.subprefe,
                    areap = item.areap,
                    bairro = item.bairro,
                    coddist = item.coddist,
                    codsubpref = item.codsubpref,
                    distrito = item.distrito,
                    latitude = item.latitude,
                    logradouro = item.logradouro,
                    longitude = item.longitude,
                    nome_feira = item.nome_feira,
                    registro = item.registro,
                    referencia = item.referencia,
                    regiao5 = item.regiao5,
                    regiao8 = item.regiao8,
                    setcens = item.setcens,
                };


                feirasDTOs.Add(feiraDTO);

            }

            return feirasDTOs;
        }

        public FeiraDTO MapperToDTO(Feira feira)
        {
            if (feira == null)
                return null;
            FeiraDTO clienteDTO = new FeiraDTO
            {
                id = feira.id,
                numero = feira.numero,
                subprefe = feira.subprefe,
                areap = feira.areap,
                bairro = feira.bairro,
                coddist = feira.coddist,
                codsubpref = feira.codsubpref,
                distrito = feira.distrito,
                latitude = feira.latitude,
                logradouro = feira.logradouro,
                longitude = feira.longitude,
                nome_feira = feira.nome_feira,
                registro = feira.registro,

                referencia = feira.referencia,
                regiao5 = feira.regiao5,
                regiao8 = feira.regiao8,
                setcens = feira.setcens,
            };

            return clienteDTO;

        }
        #endregion
    }
}
