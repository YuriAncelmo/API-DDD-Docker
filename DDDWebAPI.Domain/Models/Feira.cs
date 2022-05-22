using System.ComponentModel.DataAnnotations;

namespace DDDWebAPI.Domain.Models
{
    public class Feira : Base
    {
        #region Propriedades 
        [Key]
        public string registro { get; set; }
        public string id { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string setcens { get; set; }
        public string areap { get; set; }
        public string coddist { get; set; }
        public string distrito { get; set; }
        public string codsubpref { get; set; }
        public string subprefe { get; set; }
        public string regiao5 { get; set; }
        public string regiao8 { get; set; }
        public string nome_feira { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string referencia { get; set; }
        #endregion

        #region Construtores
        public Feira(string id, string longitude, string latitude, string setcens, string areap, string coddist, string distrito, string codsubpref, string subprefe, string regiao5, string regiao8, string nome_feira, string registro, string logradouro, string numero, string bairro, string referencia)
        {
            this.id = id;
            this.longitude = longitude;
            this.latitude = latitude;
            this.setcens = setcens;
            this.areap = areap;
            this.coddist = coddist;
            this.distrito = distrito;
            this.codsubpref = codsubpref;
            this.subprefe = subprefe;
            this.regiao5 = regiao5;
            this.regiao8 = regiao8;
            this.nome_feira = nome_feira;
            this.registro = registro;
            this.logradouro = logradouro;
            this.numero = numero;
            this.bairro = bairro;
            this.referencia = referencia;
        }
        public Feira()
        {
            this.id = "";
            this.longitude = "";
            this.latitude = "";
            this.setcens = "";
            this.areap = "";
            this.coddist = "";
            this.distrito = "";
            this.codsubpref = "";
            this.subprefe = "";
            this.regiao5 = "";
            this.regiao8 = "";
            this.nome_feira = "";
            this.registro = "";
            this.logradouro = "";
            this.numero = "";
            this.bairro = "";
            this.referencia = "";
        }
        #endregion

    }
}
