echo ' Verificando a cobertura dos testes ' 
dotnet test --logger trx
dotnet test --collect "Code Coverage"
