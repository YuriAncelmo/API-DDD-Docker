# Codificação - Unico

Este projeto tem o intuito de demonstrar como é feito uma API com boas práticas, para consumir uma base de dados.

##Técnologias e técnicas utilizadas 

 Entity Framework, Domain Driven Design, Docker, MySQL e testes unitários de controller
 
## Preparação 

Você deve ter o Docker Desktop e o .Net Framework 6 para executar a aplicação

### Docker

Execute o Docker.

### MySQL
Certifique-se de parar serviços do MySQL. 

### .Net Framework
É necessário a versão 6 para rodar o projeto.

## Como rodar o projeto
Abra a solução, no arquivo API_FeirasLivresSP.sln. Abra o gerenciador de solução, clique com botão direito em docker-compose, selecione "Definir como Projeto de Inicialização".

Um pouco acima, no botão de play, pressione o botão "Docker compose"

Ainda não encontrei uma forma de configurar os arquivos do Docker, para que a aplicação seja executada pelo comando docker-compose up

### Uso
Após subir a aplicação para o Docker, a documentação está disponível no Swagger em https://localhost:5000/index.html, com exemplos de requisição.

## Testes Unitários
Caso você queira ver as coberturas de teste desta aplicação, execute o arquivo CoberturadeTestes.bat na raiz do projeto, ele irá gerar a cobertura de código e também o documento do resultado dos testes.

Os arquivos gerados estão dentro da pasta DDDWebAPI.Tests\TestResults

## Logs 

Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documentação em https://serilog.net

Os logs de API estão dentro do container, na pasta C:/app/Logs.

Mas também você pode ver os dentro dos logs do próprio container.

### Erro comum
https://stackoverflow.com/questions/48066994/docker-no-matching-manifest-for-windows-amd64-in-the-manifest-list-entries

# Referencias 
https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
https://docs.microsoft.com/pt-br/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0
https://pt.stackoverflow.com/questions/21278/como-interceptar-exceções-quando-se-trabalha-com-o-entity-framework
https://dev.to/gbengelebs/how-to-containerize-an-asp-netcore-api-and-mysql-with-docker-compose-1m5c
https://medium.com/beelabacademy/implementando-na-prática-rest-api-com-conceitos-de-ddd-net-core-sql-no-docker-ioc-2cb3a2e7c649
https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0
https://dotnetcoretutorials.com/2018/05/12/the-testing-context/
https://code-maze.com/swagger-ui-asp-net-core-web-api/
