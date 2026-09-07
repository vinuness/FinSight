# FinSight

## Criar solução 

    dotnet new sln -n FinSight

## Adicionar projetos na solução

    find . -name "*.csproj" -exec dotnet sln FinSight.sln add {} \;

## Listar projetos da solução

    dotnet sln FinSight.sln list

# Docker

## Criar imagem 

    docker build -t (nome_da_imagem) diretorio

# Docker Compose

## Criar container
    
    docker compose up -d

## Remover container

    docker compose down

## Ver imagens
    docker images

## Ver containers
    docker ps