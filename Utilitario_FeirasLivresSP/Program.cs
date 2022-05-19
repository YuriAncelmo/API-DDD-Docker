using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utilitario_FeirasLivresSP
{
    class Program
    {
        static void Main(string[] args)
        {
            ImprimirCabecalho();
            RotinaInsercaoBancoDeDados(RotinaIngestaoArquivo());
            Console.Read();
        }

        #region Impressões de Cabeçalho
        public static void ImprimirCabecalho()
        {
            string header  =
             @"___________              __                      ____ ___      .__              "+"\n" 
            +@"\__    ___/___   _______/  |_  ____             |    |   \____ |__| ____  ____  "+"\n" 
            +@"  |    |_/ __ \ /  ___/\   __\/ __ \    ______  |    |   /    \|  |/ ___\/  _ \ "+"\n" 
            +@"  |    |\  ___/ \___ \  |  | \  ___/   /_____/  |    |  /   |  \  \  \__(  <_> )"+"\n" 
            +@"  |____| \___  >____  > |__|  \___  >           |______/|___|  /__|\___  >____/ "+"\n" 
            +@"             \/     \/            \/                         \/        \/       "+"\n";
            EscrevaComEfeitoRapido(header);
            EscrevaComEfeito("Olá! Este é o aplicativo que vai te ajudar a tirar os registros do Excel, e colocar em um banco de dados!");
            EscrevaComEfeito("Há outras formas de fazer este trabalho, como por exemplo o Azure Data Factory da Microsoft, porém, como não estamos falando de sistemas altamente escaláveis consigo fazer este trabalho.");
        }

        private static void ImprimirCabecalhoArquivo()
        {
            EscrevaComEfeito("Vamos importar os dados das feiras de São Paulo.");

            EscrevaComEfeito("A Unico especificou que o arquivo utilizado deve ser o DEINFO_AB_FEIRASLIVRES_2014.csv.");

            EscrevaComEfeito("Você pode continuar com o arquivo especificado ou selecionar um novo se preferir, o que quer fazer?");

            EscreverOpcao(1, "Selecionar o arquivo DEINFO_AB_FEIRASLIVRES_2014.csv.");

            EscreverOpcao(2, "Selecionar outro arquivo.");
        }
        #endregion

        #region Rotinas
        private static List<FeiraModel> RotinaIngestaoArquivo()
        {
            List<FeiraModel> feiras;
            string localExecucaoAssembly = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string estruturaPastas = @"\FEIRAS_LIVRES\CSV\DEINFO_DADOS_AB_FEIRASLIVRES\";
            string nomeArquivo = "DEINFO_AB_FEIRASLIVRES_2014.csv";

            ImprimirCabecalhoArquivo();

            int opcao = LerOpcao();

            if (opcao == 1)
            {
                EscrevaComEfeito("Ok. Então não vamos inovar por enquanto.");

                while (!File.Exists(localExecucaoAssembly + estruturaPastas + nomeArquivo))
                {
                    EscrevaComEfeito(string.Format("É necessário que você coloque o seu arquivo, na seguinte estrutura de pastas: {0}/FEIRAS_LIVRES/CSV/DEINFO_DADOS_AB_FEIRASLIVRES, e com o nome DEINFO_AB_FEIRASLIVRES_2014.csv", localExecucaoAssembly));
                    EscrevaComEfeito("O caminho é longo né? Não se assuste, ele é o mesmo caminho que você chegou para abrir este utilitário");
                    EscrevaComEfeito("Quando você colocar o arquivo no caminho correto, é só apertar ENTER");
                    Console.Read();
                }

                EscrevaComEfeito("O arquivo está no caminho correto.");

                EscrevaComEfeito("Enquanto você lê essa mensagem, eu já estou importando os dados para memória.");
                feiras = LerCSV(localExecucaoAssembly + estruturaPastas + nomeArquivo);
            }
            else
            {
                EscrevaComEfeito("Então preciso que você escolha um arquivo, dentre os que estão na estrutura de pastas: " + localExecucaoAssembly + estruturaPastas);
                EscrevaComEfeito("Que são os listados a seguir:");
                string[] arquivos = Directory.GetFiles(localExecucaoAssembly + estruturaPastas);
                while (arquivos.Length == 0)
                {
                    EscrevaComEfeito(string.Format("Ué, parece que não tem nenhum arquivo na pasta... Você colocou em {0}? Depois de colocar, aperte ENTER", localExecucaoAssembly + estruturaPastas));
                    Console.Read();
                }
                for (int i = 0; i < arquivos.Length; i++)
                {
                    Console.WriteLine(string.Format("{0}) {1}", i, arquivos[i]));
                }
                opcao = LerOpcao();
                feiras = LerCSV(arquivos[opcao]);
                //Enquanto não for uma opção valida , não deixa seguir;
            }
            return feiras;
        }

        private static void RotinaInsercaoBancoDeDados(List<FeiraModel> feiras)
        {
            EscrevaComEfeito("Para fazer a configuração do banco de dados, realizei o download em https://dev.mysql.com/downloads/file/?id=511553");
            EscrevaComEfeito("Caso não tenha, baixe a versão para desenvolvedor.");
            EscrevaComEfeito("Eu configurei da forma mais básica possível, dado que nosso intuito aqui não é focar apenas na base de dados.");
            EscrevaComEfeito("Se você já estiver instalado o MySQL aperte enter, se não, vai demorar um pouquinho pra instalar... vá tomar um café enquanto espera, depois volte aqui (: (Não esqueça de dar um enter quando estiver tudo OK.)");
            Console.Read();
            EscrevaComEfeito("Estou utilizando para fazer acesso ao banco de dados , a biblioteca MySql.Data.MySqlClient .Net Core Class Library, versão 8.0.29, e também o Entity Framework 6.0.1, eles são ótimas bibliotecas para facilitar a junção de back-ends e bancos de dados");

            Console.WriteLine("Deixa eu tentar conectar com o banco de dados, me dá um minuto.");


            EscrevaComEfeito("Caso você queira se conectar com um banco de dados especifico em seu servidor, altere a string de conexão disponível neste software dentro do arquivo \"StringConexao.json\" na raiz do repositório, presente na mesma pasta que este software");
            EscrevaComEfeito("Eu estou usando o usuario root e senha admin123, as configurações de comunicação com banco de dados, podem ser ajustados no arquivo, na variável com nome MySQL dentro da chave ConnectionStrings");
            EscrevaComEfeito("Estou testando a conexão com o banco agora ...");

            OperacoesBanco(feiras);

            EscrevaComEfeito("Obrigado por utilizar este utilitário, espero que tenha sido útil ;)");

        }
        #endregion

        #region Operações
        private static void OperacoesBanco(List<FeiraModel> feiras)
        {

            //using (BancoDeDadosContext db = new BancoDeDadosContext())
            //{
            //    EscrevaComEfeito("Conectamos ao banco, agora conseguimos inserir os registros do arquivo na base de dados :)");
                
            //    EscrevaComEfeito("Para comunicar com o banco de dados, eu utilizei o Entity Framework Core, e usei como base a documentação -> https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core.html");
            //    db.InserirLote(feiras);
            //    Console.Clear();
                
            //}
            //using (BancoDeDadosContext db = new BancoDeDadosContext())
            //    EscrevaComEfeito(String.Format("Deu certo!. Foram inseridos {0} registros,e a quantidade total de registros no banco é de {1}", feiras.Count, db.Feiras.Count()));

        }
        private static int LerOpcao()
        {
            int opcao = -1;
            while (opcao == -1)
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Digite uma opção válida");
                }
            Console.Clear();
            return opcao;
        }

        private static void EscreverOpcao(int numero, string descricao)
        {
            Console.WriteLine(string.Format("{0}) {1}", numero, descricao));
        }

        private static List<FeiraModel> LerCSV(string file)
        {
            List<FeiraModel> feiras = new List<FeiraModel>();
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int header = 0;
                while (!parser.EndOfData)//O(n)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    //O(n²), no caso de 800 linhas , o pesar de processamento é 640000
                    for (int i = 0; i < fields.Length; i++)//TODO : Essa ferramenta é apenas para intuito de testes, para algo mais performatico, é necessário um ETL
                    {
                        fields[i] = RemoveSpecialCharacters(fields[i]);
                    }
                    if (header > 0)
                        if (fields.Length == 16)
                        {
                            //Ultimo campo não veio 
                            feiras.Add(new FeiraModel(fields[0], fields[1], fields[2],
                                       fields[3], fields[4], fields[5], fields[6], fields[7], fields[8],
                                       fields[9], fields[10], fields[11], fields[12], fields[13], fields[14],
                                       fields[15], ""));
                        }
                        else
                        {
                            feiras.Add(new FeiraModel(fields[0], fields[1], fields[2],
                                          fields[3], fields[4], fields[5], fields[6], fields[7], fields[8],
                                          fields[9], fields[10], fields[11], fields[12], fields[13], fields[14],
                                          fields[15], fields[16]));
                        }
                    header++;
                }
                return feiras;
                //list.ForEach(size => Console.WriteLine(size));
            }
        }
        #endregion

        #region Métodos auxiliares
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
        }
        private static void EscrevaComEfeito(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
#if !DEBUG
                Thread.Sleep(30);
#endif
            }
            Console.WriteLine("\n");
#if !DEBUG
            Thread.Sleep(500);
#endif
        }
        private static void EscrevaComEfeitoRapido(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
#if !DEBUG
                Thread.Sleep(10);
#endif
            }
            Console.WriteLine("\n");

        }
        #endregion
    }
}
