# FinSight

## Criar solução 

    dotnet new sln -n FinSight

## Adicionar projetos na solução

    find . -name "*.csproj" -exec dotnet sln FinSight.sln add {} \;

## Listar projetos da solução

    dotnet sln FinSight.sln list