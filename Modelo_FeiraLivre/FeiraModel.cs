using System;

namespace Modelo_FeiraLivre
{
    public class FeiraModel
    {
        public string ID;
        public string LONG; 
        public string LAT;
        public string SETCENS; 
        public string AREAP; 
        public string CODDIST; 
        public string DISTRITO; 
        public string CODSUBPREF; 
        public string SUBPREFE; 
        public string REGIAO5; 
        public string REGIAO8; 
        public string NOME_FEIRA; 
        public string REGISTRO;
        public string LOGRADOURO; 
        public string NUMERO; 
        public string BAIRRO;
        public string REFERENCIA;

        public FeiraModel(string iD, string lONG, string lAT, string sETCENS, string aREAP, string cODDIST, string dISTRITO, string cODSUBPREF, string sUBPREFE, string rEGIAO5, string rEGIAO8, string nOME_FEIRA, string rEGISTRO, string lOGRADOURO, string nUMERO, string bAIRRO, string rEFERENCIA)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
            LONG = lONG ?? throw new ArgumentNullException(nameof(lONG));
            LAT = lAT ?? throw new ArgumentNullException(nameof(lAT));
            SETCENS = sETCENS ?? throw new ArgumentNullException(nameof(sETCENS));
            AREAP = aREAP ?? throw new ArgumentNullException(nameof(aREAP));
            CODDIST = cODDIST ?? throw new ArgumentNullException(nameof(cODDIST));
            DISTRITO = dISTRITO ?? throw new ArgumentNullException(nameof(dISTRITO));
            CODSUBPREF = cODSUBPREF ?? throw new ArgumentNullException(nameof(cODSUBPREF));
            SUBPREFE = sUBPREFE ?? throw new ArgumentNullException(nameof(sUBPREFE));
            REGIAO5 = rEGIAO5 ?? throw new ArgumentNullException(nameof(rEGIAO5));
            REGIAO8 = rEGIAO8 ?? throw new ArgumentNullException(nameof(rEGIAO8));
            NOME_FEIRA = nOME_FEIRA ?? throw new ArgumentNullException(nameof(nOME_FEIRA));
            REGISTRO = rEGISTRO ?? throw new ArgumentNullException(nameof(rEGISTRO));
            LOGRADOURO = lOGRADOURO ?? throw new ArgumentNullException(nameof(lOGRADOURO));
            NUMERO = nUMERO ?? throw new ArgumentNullException(nameof(nUMERO));
            BAIRRO = bAIRRO ?? throw new ArgumentNullException(nameof(bAIRRO));
            REFERENCIA = rEFERENCIA ?? throw new ArgumentNullException(nameof(rEFERENCIA));
        }
        public string transformarParaInsert()
        {
            return string.Concat("(",ID, ",'", LONG, "','", LAT, "','", SETCENS, "','", AREAP, "','", CODDIST, "','", DISTRITO, "','", CODSUBPREF,
                "','", SUBPREFE, "','", REGIAO5, "','", REGIAO8, "','", NOME_FEIRA, "','", REGISTRO, "','", LOGRADOURO, "','", NUMERO, "','", BAIRRO, "','", REFERENCIA,"')");
        }
    }
}