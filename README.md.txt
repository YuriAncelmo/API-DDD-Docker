https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework60.html

Este software teste foi realizado com muito carinho e empenho!

Primeiro de tudo, vamos preparar o ambiente. 

H� dispon�vel nesta suit, um utilit�rio para te ajudar a importar os dados pr�-existentes de uma das bases de dados que est�o dispon�veis.

Este utilit�rio est� dispon�vel, na raiz deste projeto , na pasta utilitario, com o nome de Utilitario.exe, lembre-se de seguir todas as instru��es presentes 

Apos a importa��o da base de dados para o MySQL e com este j� rodando, voc� pode rodar a API e come�ar a fazer requests 

As requisi��es para api de feiras ficam no seguinte formato 
https://localhost:44365/feira/distrito/liberdade

Onde 
https://localhost:44365/ � a url do servidor
feira, � o radical da API 
distrito, o subgrupo onde voc� est� realizando operacoes
liberdade, � o que voc� est� procurando, lembre-se , esta solu��o � CASE SENSITIVE.

Iniciando por endpoints de busca, voc� precisa utilizar o m�todo GET e com isso voc� tem as op��es de pesquisar por bairro, distrito, regiao5 e nome_feira.
E os exemplos s�o respectivamente:
https://localhost:44365/feira/bairro/liberdade
https://localhost:44365/feira/distrito/liberdade
https://localhost:44365/feira/regiao5/liberdade
https://localhost:44365/feira/nome/liberdade

Vou disponibilizar tamb�m uma collection do postman (Unico_APIFeirasSP.postman_collection) para voc� fazer a importa��o e testar cada um dos endpoints.
Se voc� tiver problemas de SSL com postman, desabilite, pois nesta solu��o, n�o foi previsto utiliza��o de SSL, por se tratar apenas de um teste.
Busca
Voc� ver� que as APIs com m�todos GET retornam basicamente 2 c�digos no que se refere a c�digos de sucesso.  
200 - Ok , sua requisi��o est� correta e possui resultados .
204 - No content, sua requisi��o est� correta, por�m n�o h� resultados dispon�veis.

Inser��o
Sobre a inser��o de novos registros, voc� pode acessar o endpoint raiz https://localhost:44365/feira/ passando como parametro no body, a entidade que voc� quer inserir.

A inser��o tamb�m est� dispon�vel na collection, atente-se para os c�digos que podem vir deste endpoint no quesito de tratamento e exce��o.
200 - Ok, tudo certo e ainda te retornara uma mensagem no body mostrando	
422 - Houve algum erro de neg�cio, como por exemplo, voc�s est� tentando inserir uma feira que j� existe, junto a este c�digo, vir� a mensagem retornada pelo MySQL https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/422

Remo��o
Quanto a remo��o de um registro, voc� precisa passar um c�digo de registro, assim como est� na collection, no pr�prio endpoint https://localhost:44365/feira/77414-A por exemplo. 
Ela retornara apenas 200 OK, mesmo que o registro que voc� esteja tentando deletar n�o exista ou um registro de erro gen�rico caso aconte�a.

Altera��o
Se voc� quiser alterar algum registro, saiba que pode, e pra isso, acesse o endpoint raiz, e execute um metodo patch passando o id(https://localhost:44365/feira/77414-A), que permite que voc� altere um ou mais campos, com exce��o do campo registro
Os c�digos que podem ser retornados sao 
200 - OK 
404 - Quando n�o for encontrado a feira que voc� quer alterar, isto �, n�o h� nenhum registro no banco de dados com o mesmo c�digo de registro que voc� est� passando na requisi��o.

Testes Unit�rios
Caso voc� queira ver os testes unit�rios dispon�veis nesta aplica��o, abra o arquivo API_FeirasLivresSP no visual studio , clique com bot�o direito no projeto Teste_FeiraLivre e selecione Executar todos os testes.

Os resultados destes testes tamb�m estar�o dispon�veis na raiz desta suit, com o nome de 

Logs 
Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documenta��o em https://serilog.net

Cada log est� na pasta do projeto ao qual corresponde, por exemplo, os logs relacionados a API , est�o no folder do projeto de API , os do Utilit�rio no utilit�rio, e assim por diante

Eles est�o presentes na pasta Logs de cada folder.
	
	