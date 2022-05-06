using Microsoft.VisualBasic.FileIO;
using Modelo_FeiraLivre;
using System;
using System.IO;
using System.Threading;
namespace Utilitario_FeirasLivresSP
{
    class Program
    {
        static void Main(string[] args)
        {
            string BancoDeDados = "MySQL";


            ImprimirCabecalho();

            RotinaIngestaoArquivo(BancoDeDados);

            Console.Read();
        }

        private static void RotinaIngestaoArquivo(string BancoDeDados)
        {
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

                EscrevaComEfeito("Enquanto você lê essa mensagem, eu já estou importando os dados a memória.");
                LerCSV(localExecucaoAssembly + estruturaPastas + nomeArquivo);
                EscrevaComEfeito("Para fazer a configuração deste banco de dados, realizei o download em https://dev.mysql.com/downloads/file/?id=511553");
                EscrevaComEfeito("Baixe a versão para desenvolvedor, padrão.");
                EscrevaComEfeito("Vai demorar um pouquinho pra instalar... vá tomar um café enquanto espera, depois volte aqui (: (Não esqueça de dar um enter quando estiver tudo OK.)");
                Console.Read();
                EscrevaComEfeito("Estou utilizando para fazer acesso ao banco de dados , a biblioteca MySql.Data.MySqlClient .Net Core Class Library, versão 8.0.29");

                Console.WriteLine("Deixa eu tentar conectar com o banco de dados, me dá um minuto.");


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
                LerCSV(arquivos[opcao]);
                //Enquanto não for uma opção valida , não deixa seguir;
            }
        }

        private static void ImprimirCabecalhoArquivo()
        {
            EscrevaComEfeito("Podemos começar, fazendo a importação dos dados das feiras de São Paulo.");

            EscrevaComEfeito("A Unico especificou que o arquivo utilizado deve ser o DEINFO_AB_FEIRASLIVRES_2014.csv.");

            EscrevaComEfeito("Você pode continuar com o arquivo especificado ou selecionar um novo, o que quer fazer?");

            EscreverOpcao(1, "Selecionar o arquivo DEINFO_AB_FEIRASLIVRES_2014.csv.");

            EscreverOpcao(2, "Selecionar outro arquivo.");
        }

        public static void ImprimirCabecalho()
        {
            EscrevaComEfeito("Olá! Esta é a documentação iterativa para o teste de software da Unico!");
        }
        private static int LerOpcao()
        {
            int opcao = -1;
            while (opcao != -1)
                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Digite uma opção válida");
                }
            return opcao;
        }

        private static void EscreverOpcao(int numero, string descricao)
        {
            Console.WriteLine(string.Format("{0}) {1}", numero, descricao));
        }

        private static void LerCSV(string file)
        {
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)//O(n)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    FeiraModel model = new FeiraModel(fields[0], fields[1], fields[2],
                        fields[3], fields[4], fields[5], fields[6], fields[7], fields[8],
                        fields[9], fields[10], fields[11], fields[12], fields[13], fields[14],
                        fields[15], fields[16]);
                }
            }
        }

        private static void EscrevaComEfeito(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]);
#if DEBUG
                Thread.Sleep(30);
#endif
            }
            Console.WriteLine("\n");
#if DEBUG
            Thread.Sleep(500);
#endif
        }
    }
}
