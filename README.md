# Teste de Codificação - Unico

Este software teste foi realizado com muito carinho e empenho!

## Instalação

Primeiro de tudo, vamos preparar o ambiente. 

Certifique-se de ter instalado o .net 6.0 e o MySQL Server.

#### Importação dos dados e configuração do banco 

Há disponível nesta suit, um utilitário para te ajudar a importar os dados pré-existentes de uma das bases de dados que estão disponíveis.

Este utilitário está  na raiz deste projeto, na pasta Utilitário, com nome Utilitario_FeirasLivresSP.exe, lembre-se de seguir todas as instruções presentes. 
#### Rodando a API 

Você tem algumas opções para rodar a API, a primeira é abrindo a solução a partir do arquivo API_FeirasLivresSP.sln.

Feito isto, clique no play do visual studio, com a configuração de IIS selecionada.

#### Publicação do Site no IIS Local

Para realizar a publicação do site em um IIS, utilizei como base a documentação [Publicando um aplicativo .Net Core no IIS - Autor independente](https://alexalvess.medium.com/publicando-aplicação-net-core-no-iss-f4079c2f312) e para complemento a [Publicando um .Net Core no IIS - MSDN](https://docs.microsoft.com/pt-br/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=visual-studio)

Se tiver problema com as permissões de pasta, tenta dar controle total ao usuário do IIS e aos usuários como o seu próprio.

## Uso

Após a importação da base de dados para o MySQL e com este já rodando, com a API rodando, você pode começar a fazer requests 

As requisições para api de feiras ficam no seguinte formato 
https://localhost:44365/feira/distrito/liberdade

Onde 
https://localhost:44365/ é a url e porta do servidor.

feira, é o resource que você está acessando da API.

distrito, o subgrupo onde você está realizando operações.

liberdade, é a sua consulta, lembre-se, esta solução é CASE SENSITIVE.

### Collection Postman 
Vou disponibilizar também uma collection do postman (Unico_APIFeirasSP.postman_collection) para você fazer a importação e testar cada um dos endpoints.
Se você tiver problemas de SSL com postman, desabilite, pois nesta solução, não foi previsto utilização de SSL, por se tratar apenas de um teste.

### Requisito Busca
Iniciando por endpoints de busca, você precisa utilizar o método GET e com isso você tem as opções de pesquisar por bairro, distrito, regiao5 e nome_feira.
E os exemplos são respectivamente:

https://localhost:44365/feira/bairro/liberdade
https://localhost:44365/feira/distrito/liberdade
https://localhost:44365/feira/regiao5/liberdade
https://localhost:44365/feira/nome/liberdade

Você verá que as APIs com métodos GET retornam basicamente 2 códigos no que se refere a códigos de sucesso.  

##### Retornos 
200 - Ok , sua requisição está correta e possui resultados.
204 - No content, sua requisição está correta, porém não há resultados disponíveis.

### Requisito Inserção

Sobre a inserção de novos registros, você pode acessar o endpoint raiz https://localhost:44365/feira/ passando como parametro no body, a entidade que você quer inserir.

A inserção também está disponível na collection, atente-se para os códigos que podem vir deste endpoint no quesito de tratamento e exceção.

##### Retornos 

200 - Ok, tudo certo e ainda te retornara uma mensagem no body mostrando	
422 - Houve algum erro de negócio, como por exemplo, vocês está tentando inserir uma feira que já existe, junto a este código, virá a mensagem retornada pelo MySQL https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/422

### Requisito Remoção

Quanto a remoção de um registro, você precisa passar um código de registro, assim como está na collection, no próprio endpoint https://localhost:44365/feira/77414-A por exemplo. 

##### Retornos 

Ela retornara apenas 200 OK, mesmo que o registro que você esteja tentando deletar não exista ou um registro de erro genérico caso aconteça.

### Requisito Alteração

Se você quiser alterar algum registro, saiba que pode, e pra isso, acesse o endpoint raiz, e execute um metodo patch passando o id (https://localhost:44365/feira/77414-A), que permite que você altere um ou mais campos, com exceção do campo registro

##### Retornos 

200 - OK 

404 - Quando não for encontrado a feira que você quer alterar, isto é, não há nenhum registro no banco de dados com o mesmo código de registro que você está passando na requisição.

## Testes Unitários
Caso você queira ver as coberturas de teste desta aplicação, execute o arquivo CoberturadeTestes.bat na raiz do projeto, ele irá gerar a cobertura de código e também o documento do resultado dos testes.

Os arquivos gerados estão dentro da pasta Teste_FeiraLivre\TestResults

## Logs 
Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documentação em https://serilog.net

Os logs de API estão dentro da pasta do projeto se você rodou o projeto direto do Visual Studio.

Se você hospedou o site em seu IIS, os logs estão na pasta que você colocou seu site. Ex.: C:\inetpub\wwwroot\APIFeiras\Logs
	
