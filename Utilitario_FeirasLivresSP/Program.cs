using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading;

namespace Utilitario_FeirasLivresSP
{
    class Program
    {
        static void Main(string[] args)
        {
            string BancoDeDados = "SQL Server";
            EscrevaComEfeito("Olá! Esta é a documentação iterativa para o teste de software da Unico!");

            EscrevaComEfeito("Podemos começar, fazendo a importação dos dados das feiras de São Paulo.");

            EscrevaComEfeito("A Unico especificou que o arquivo utilizado deve ser o DEINFO_AB_FEIRASLIVRES_2014.csv.");

            EscrevaComEfeito("Você pode continuar com o arquivo especificado ou selecionar um novo, o que quer fazer?");

            Console.WriteLine("1) Selecionar o arquivo DEINFO_AB_FEIRASLIVRES_2014.csv.");
            Console.WriteLine("2) Selecionar outro arquivo.");
            string opcao = Console.ReadLine();
            string localExecucaoAssembly = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string estruturaPastas = @"\FEIRAS_LIVRES\CSV\DEINFO_DADOS_AB_FEIRASLIVRES\";
            string nomeArquivo = "DEINFO_AB_FEIRASLIVRES_2014.csv";

            if (opcao == "1")
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

                EscrevaComEfeito(string.Format("Enquanto você lê essa mensagem, eu já estou importando os dados para um banco de dados relacional, chamado {0}.", BancoDeDados));
                LerCSV(localExecucaoAssembly + estruturaPastas + nomeArquivo);

                //Combine that with System.IO.Path.GetDirectoryName if all you want is

            }
            else
            {
                EscrevaComEfeito("Então preciso que você escolha um arquivo, dentre os que estão na estrutura de pastas: " + localExecucaoAssembly + estruturaPastas);
                EscrevaComEfeito("Que são os listados a seguir:");
                string[] arquivos = Directory.GetFiles(localExecucaoAssembly + estruturaPastas);
                while(arquivos.Length == 0 )
                {
                    EscrevaComEfeito(string.Format("Ué, parece que não tem nenhum arquivo na pasta... Você colocou em {0}? Depois de colocar, aperte ENTER",localExecucaoAssembly + estruturaPastas ));
                    Console.Read();
                }
                for (int i = 0; i < arquivos.Length; i++)
                {
                    Console.WriteLine(string.Format("{0}) {1}", i, arquivos[i]));
                }
                opcao = Console.ReadLine();
                //Enquanto não for uma opção valida , não deixa seguir;
            }
            Console.Read();
        }

        private static void LerCSV(string file)
        {
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        //TODO: Process field
                        Console.Write(field);
                    }
                }
            }
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
    }
}
