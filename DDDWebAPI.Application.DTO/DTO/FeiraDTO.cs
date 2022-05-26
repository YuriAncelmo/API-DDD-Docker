
using System.ComponentModel.DataAnnotations;

namespace DDDWebAPI.Application.DTO.DTO
{
    public class FeiraDTO
    {
        #region Propriedades 
        [Key]
        public string? registro { get; set; }
        public string? id { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? setcens { get; set; }
        public string? areap { get; set; }
        public string? coddist { get; set; }
        public string? distrito { get; set; }
        public string? codsubpref { get; set; }
        public string? subprefe { get; set; }
        public string? regiao5 { get; set; }
        public string? regiao8 { get; set; }
        public string? nome_feira { get; set; }
        public string? logradouro { get; set; }
        public string? numero { get; set; }
        public string? bairro { get; set; }
        public string? referencia { get; set; }
        #endregion
        
    }
}
