using Microsoft.VisualBasic.FileIO;
using Modelo_FeiraLivre;
using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using Util_FeirasLivres;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utilitario_FeirasLivresSP
{
    class Program
    {
        static void Main(string[] args)
        {
            string BancoDeDados = "MySQL";
            List<FeiraModel> feiras = new List<FeiraModel>();
            ImprimirCabecalho();

            feiras = RotinaIngestaoArquivo(BancoDeDados);
            RotinaInsercaoBancoDeDados(feiras);
            Console.Read();
        }

        private static void RotinaInsercaoBancoDeDados(List<FeiraModel> feiras)
        {
            EscrevaComEfeito("Eu utilizei como base para configuração, o vídeo desta pessoa https://www.youtube.com/watch?v=OUZIaoCSJas.");

            EscrevaComEfeito("Então fiz da seguinte forma, abri o MySQL Installer, e na opção MySQL Server, cliquei em \"Reconfigure\".");
            EscrevaComEfeito("Eu configurei da forma mais básica possível, dado que nosso intuito aqui não é focar apenas na base de dados.");

            EscrevaComEfeito("Caso você queira se conectar com um banco de dados especifico em seu servidor, altere a string de conexão disponível neste software dentro do arquivo appsettings, presente na mesma pasta que este software");
            EscrevaComEfeito("Eu estou usando o usuario root e senha admin123, as configurações de comunicação com banco de dados, podem ser ajustados no arquivo, na variável com nome MySQL dentro da chave ConnectionStrings");
            EscrevaComEfeito("Estou testando a conexão com o banco agora ...");

            OperacoesBanco(feiras);

            //EscrevaComEfeito("Durante o desenvolvimento recebi um erro dizendo \"Unable to connect to any specified mysql host\" e encontrei a resposta dizendo que a minha string de conexão estava com uma ordem equivocada");
            //EscrevaComEfeito("Se você receber o mesmo erro, veja o que encontrei neste forum do StackOverFlow https://stackoverflow.com/questions/17993657/unable-to-connect-to-any-of-the-specified-mysql-hosts-c-sharp-mysql");


        }

        private static void OperacoesBanco(List<FeiraModel> feiras)
        {

            BancoDeDados db = new BancoDeDados();
            EscrevaComEfeito("Você já tem a tabela criada?");
            EscreverOpcao(1, "Não");
            EscreverOpcao(2, "Sim");
            int opcao = LerOpcao();
            if (opcao == 1)
            {
                EscrevaComEfeito("Você quer que eu crie pra você?");
                EscreverOpcao(1, "Sim");
                EscreverOpcao(2, "Não");
                opcao = LerOpcao();
                if (opcao == 2)
                {
                    EscrevaComEfeito("Já que você não quer que eu crie, então você pode pegar o script que criei na mão, que está na pasta do projeto, chamado \"scriptcriacao.sql\", depois que criar , tecle enter.");
                    Console.Read();
                }
                else
                {
                    try
                    {
                        db = new BancoDeDados();
                        db.CriarTabela();
                        EscrevaComEfeito("Olha só, consegui criar a tabela!");
                    }
                    catch (Exception ex)
                    {
                        EscrevaComEfeito("Eita.. tentei criar a tabela Feira, na base de dados Unico e deu o erro " + ex.Message + " por favor , analise e reinicie o aplicativo para continuar");
                    }

                }
            }
            EscrevaComEfeito("Beleza, então acho que agora dá pra inserir os registros do arquivo :)");
            EscrevaComEfeito("Eu vi alguns foruns sobre inserção multipla de valores no MySQL, e vendo neste aqui https://www.mysqltutorial.org/mysql-insert-multiple-rows/, tomei uma decisão.");

            EscrevaComEfeito("Eu havia criado um modelo para fazer a ingestão dos dados, e posteriormente construiria a logica para comunicação com banco de dados, porém, para longevidade e facilidade de manutenção dese aplicativo, vou usar o Entity Framwork Core.");
            EscrevaComEfeito("No caso do nosso banco de dados, o entity é uma implementação do time do mysql, não necessariamente da Microsoft, e neste link eu consegui toda a documentação https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html")
            EscrevaComEfeito("Chega de conversa, me da um segundo que vou inserir os registros na tabela.");
            db.InserirRegistros(feiras);
            Console.Clear();
            EscrevaComEfeito("Certinho então. Quer saber quantos registros existem no banco de dados?");
            EscreverOpcao(1, "Não precisa, obrigado!");
            EscreverOpcao(2, "Sim, por favor!");
            if (LerOpcao() == 2)
            {
                db.ContarRegistros();
            }
        }

        private static List<FeiraModel> RotinaIngestaoArquivo(string BancoDeDados)
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

                EscrevaComEfeito("Enquanto você lê essa mensagem, eu já estou importando os dados a memória.");
                feiras = LerCSV(localExecucaoAssembly + estruturaPastas + nomeArquivo);
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
                feiras = LerCSV(arquivos[opcao]);
                //Enquanto não for uma opção valida , não deixa seguir;
            }
            return feiras;
        }

        private static void ImprimirCabecalhoArquivo()
        {
            EscrevaComEfeito("Podemos começar fazendo a importação dos dados das feiras de São Paulo.");

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
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
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
