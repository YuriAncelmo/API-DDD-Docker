# Teste de Codifica��o - Unico

Este software teste foi realizado com muito carinho e empenho!

## Instala��o

Primeiro de tudo, vamos preparar o ambiente. 

Certifique-se de ter instalado o .net 6.0 e o MySQL Server.

Docker , se você tiver uma instancia do mysql já rodando na sua maquina, certifique-se de pará-la. Caso contrário, poderá receber um erro hnsCallRawResponse.
#### Importa��o dos dados e configura��o do banco 

H� dispon�vel nesta suit, um utilit�rio para te ajudar a importar os dados pr�-existentes de uma das bases de dados que est�o dispon�veis.

Este utilit�rio est�  na raiz deste projeto, na pasta Utilit�rio, com nome Utilitario_FeirasLivresSP.exe, lembre-se de seguir todas as instru��es presentes. 
#### Rodando a API 

Voc� tem algumas op��es para rodar a API, a primeira � abrindo a solu��o a partir do arquivo API_FeirasLivresSP.sln.

Feito isto, clique no play do visual studio, com a configura��o de IIS selecionada.

#### Publica��o do Site no IIS Local

Para realizar a publica��o do site em um IIS, utilizei como base a documenta��o [Publicando um aplicativo .Net Core no IIS - Autor independente](https://alexalvess.medium.com/publicando-aplica��o-net-core-no-iss-f4079c2f312) e para complemento a [Publicando um .Net Core no IIS - MSDN](https://docs.microsoft.com/pt-br/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=visual-studio)

Se tiver problema com as permiss�es de pasta, tenta dar controle total ao usu�rio do IIS e aos usu�rios como o seu pr�prio.

## Uso

Ap�s a importa��o da base de dados para o MySQL e com este j� rodando, com a API rodando, voc� pode come�ar a fazer requests 

As requisi��es para api de feiras ficam no seguinte formato 
https://localhost:44365/feira/distrito/liberdade

Onde 
https://localhost:44365/ � a url e porta do servidor.

feira, � o resource que voc� est� acessando da API.

distrito, o subgrupo onde voc� est� realizando opera��es.

liberdade, � a sua consulta, lembre-se, esta solu��o � CASE SENSITIVE.

### Collection Postman 
Vou disponibilizar tamb�m uma collection do postman (Unico_APIFeirasSP.postman_collection) para voc� fazer a importa��o e testar cada um dos endpoints.
Se voc� tiver problemas de SSL com postman, desabilite, pois nesta solu��o, n�o foi previsto utiliza��o de SSL, por se tratar apenas de um teste.

### Requisito Busca
Iniciando por endpoints de busca, voc� precisa utilizar o m�todo GET e com isso voc� tem as op��es de pesquisar por bairro, distrito, regiao5 e nome_feira.
E os exemplos s�o respectivamente:

https://localhost:44365/feira/bairro/liberdade
https://localhost:44365/feira/distrito/liberdade
https://localhost:44365/feira/regiao5/liberdade
https://localhost:44365/feira/nome/liberdade

Voc� ver� que as APIs com m�todos GET retornam basicamente 2 c�digos no que se refere a c�digos de sucesso.  

##### Retornos 
200 - Ok , sua requisi��o est� correta e possui resultados.
204 - No content, sua requisi��o est� correta, por�m n�o h� resultados dispon�veis.

### Requisito Inser��o

Sobre a inser��o de novos registros, voc� pode acessar o endpoint raiz https://localhost:44365/feira/ passando como parametro no body, a entidade que voc� quer inserir.

A inser��o tamb�m est� dispon�vel na collection, atente-se para os c�digos que podem vir deste endpoint no quesito de tratamento e exce��o.

##### Retornos 

200 - Ok, tudo certo e ainda te retornara uma mensagem no body mostrando	
422 - Houve algum erro de neg�cio, como por exemplo, voc�s est� tentando inserir uma feira que j� existe, junto a este c�digo, vir� a mensagem retornada pelo MySQL https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/422

### Requisito Remo��o

Quanto a remo��o de um registro, voc� precisa passar um c�digo de registro, assim como est� na collection, no pr�prio endpoint https://localhost:44365/feira/77414-A por exemplo. 

##### Retornos 

Ela retornara apenas 200 OK, mesmo que o registro que voc� esteja tentando deletar n�o exista ou um registro de erro gen�rico caso aconte�a.

### Requisito Altera��o

Se voc� quiser alterar algum registro, saiba que pode, e pra isso, acesse o endpoint raiz, e execute um metodo patch passando o id (https://localhost:44365/feira/77414-A), que permite que voc� altere um ou mais campos, com exce��o do campo registro

##### Retornos 

200 - OK 

404 - Quando n�o for encontrado a feira que voc� quer alterar, isto �, n�o h� nenhum registro no banco de dados com o mesmo c�digo de registro que voc� est� passando na requisi��o.

## Testes Unit�rios
Caso voc� queira ver as coberturas de teste desta aplica��o, execute o arquivo CoberturadeTestes.bat na raiz do projeto, ele ir� gerar a cobertura de c�digo e tamb�m o documento do resultado dos testes.

Os arquivos gerados est�o dentro da pasta Teste_FeiraLivre\TestResults

## Logs 
Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documenta��o em https://serilog.net

Os logs de API est�o dentro da pasta do projeto se voc� rodou o projeto direto do Visual Studio.

Se voc� hospedou o site em seu IIS, os logs est�o na pasta que voc� colocou seu site. Ex.: C:\inetpub\wwwroot\APIFeiras\Logs
	
## Swagger 
Est� dispon�vel no endpoint da API swagger/v1/swagger 

#Referencias 
https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
https://docs.microsoft.com/pt-br/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0
https://pt.stackoverflow.com/questions/21278/como-interceptar-exce��es-quando-se-trabalha-com-o-entity-framework
https://dev.to/gbengelebs/how-to-containerize-an-asp-netcore-api-and-mysql-with-docker-compose-1m5c