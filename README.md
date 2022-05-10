
Este software teste foi realizado com muito carinho e empenho!

Primeiro de tudo, vamos preparar o ambiente. 

Certifique-se de ter instalado o .net 6.0 e o MySQL 

Há disponível nesta suit, um utilitário para te ajudar a importar os dados pré-existentes de uma das bases de dados que estão disponíveis.

Este utilitário está  na raiz deste projeto, na pasta utilitario, com o nome de Utilitario.exe, lembre-se de seguir todas as instruções presentes. 

Após a importação da base de dados para o MySQL e com este já rodando, você pode rodar a API e começar a fazer requests 

As requisições para api de feiras ficam no seguinte formato 
https://localhost:44365/feira/distrito/liberdade

Onde 
https://localhost:44365/ é a url do servidor
feira, é o radical da API 
distrito, o subgrupo onde você está realizando operacoes
liberdade, é o que você está procurando, lembre-se , esta solução é CASE SENSITIVE.

Iniciando por endpoints de busca, você precisa utilizar o método GET e com isso você tem as opções de pesquisar por bairro, distrito, regiao5 e nome_feira.
E os exemplos são respectivamente:
https://localhost:44365/feira/bairro/liberdade
https://localhost:44365/feira/distrito/liberdade
https://localhost:44365/feira/regiao5/liberdade
https://localhost:44365/feira/nome/liberdade

Vou disponibilizar também uma collection do postman (Unico_APIFeirasSP.postman_collection) para você fazer a importação e testar cada um dos endpoints.
Se você tiver problemas de SSL com postman, desabilite, pois nesta solução, não foi previsto utilização de SSL, por se tratar apenas de um teste.
Busca
Você verá que as APIs com métodos GET retornam basicamente 2 códigos no que se refere a códigos de sucesso.  
200 - Ok , sua requisição está correta e possui resultados .
204 - No content, sua requisição está correta, porém não há resultados disponíveis.

Inserção
Sobre a inserção de novos registros, você pode acessar o endpoint raiz https://localhost:44365/feira/ passando como parametro no body, a entidade que você quer inserir.

A inserção também está disponível na collection, atente-se para os códigos que podem vir deste endpoint no quesito de tratamento e exceção.
200 - Ok, tudo certo e ainda te retornara uma mensagem no body mostrando	
422 - Houve algum erro de negócio, como por exemplo, vocês está tentando inserir uma feira que já existe, junto a este código, virá a mensagem retornada pelo MySQL https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/422

Remoção
Quanto a remoção de um registro, você precisa passar um código de registro, assim como está na collection, no próprio endpoint https://localhost:44365/feira/77414-A por exemplo. 
Ela retornara apenas 200 OK, mesmo que o registro que você esteja tentando deletar não exista ou um registro de erro genérico caso aconteça.

Alteração
Se você quiser alterar algum registro, saiba que pode, e pra isso, acesse o endpoint raiz, e execute um metodo patch passando o id(https://localhost:44365/feira/77414-A), que permite que você altere um ou mais campos, com exceção do campo registro
Os códigos que podem ser retornados sao 
200 - OK 
404 - Quando não for encontrado a feira que você quer alterar, isto é, não há nenhum registro no banco de dados com o mesmo código de registro que você está passando na requisição.

Testes Unitários
Caso você queira ver as coberturas de teste desta aplicação,execute o arquivo CoberturadeTestes.bat na raiz do projeto, ele irá gerar a cobertura de código e também o documento do resujltado dos testes 
Há uma limitação com o Visual Studio Community, que é o que eu utilizo, para abertura de arquivos .coverage, então não consegui validar seu conteúdo, porém o arquivo com extensão trx também mostra os resultados dos testes.

Logs 
Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documentação em https://serilog.net

Cada log está na pasta do projeto ao qual corresponde, por exemplo, os logs relacionados a API , estão no folder do projeto de API , os do Utilitário no utilitário, e assim por diante

Eles estão presentes na pasta Logs de cada folder.
	
	