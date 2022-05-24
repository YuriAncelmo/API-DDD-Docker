# Teste de Codificação - Unico

Este projeto tem o intuito de demonstrar como é feito uma API com boas práticas, para consumir uma base de dados das feiras livres e São Paulo.

## Instalação

Primeiro de tudo, vamos preparar o ambiente. 

### Docker

Foi utilizado o docker para preparação do ambiente. Certifique-se de parar serviços do MySQL antes de executar o comando abaixo.

O comando ```docker-compose up

### Uso

Após subir a aplicação para o Docker, a documentação está disponível no Swagger em https://localhost:5000/index.html, com exemplos de requisição.

## Testes Unitários
Caso voc� queira ver as coberturas de teste desta aplica��o, execute o arquivo CoberturadeTestes.bat na raiz do projeto, ele ir� gerar a cobertura de c�digo e tamb�m o documento do resultado dos testes.

Os arquivos gerados est�o dentro da pasta Teste_FeiraLivre\TestResults

## Logs 

Sobre os rastreios , usei uma biblioteca nova que encontrei em um forum , chamada sirilog , que possui documentação em https://serilog.net

Os logs de API estão dentro do container, na pasta C:/app/Logs.

## Erro comuns 
https://stackoverflow.com/questions/48066994/docker-no-matching-manifest-for-windows-amd64-in-the-manifest-list-entries

#Referencias 
https://docs.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
https://docs.microsoft.com/pt-br/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0
https://pt.stackoverflow.com/questions/21278/como-interceptar-exce��es-quando-se-trabalha-com-o-entity-framework
https://dev.to/gbengelebs/how-to-containerize-an-asp-netcore-api-and-mysql-with-docker-compose-1m5c
https://medium.com/beelabacademy/implementando-na-prática-rest-api-com-conceitos-de-ddd-net-core-sql-no-docker-ioc-2cb3a2e7c649
https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/testing?view=aspnetcore-6.0
https://dotnetcoretutorials.com/2018/05/12/the-testing-context/