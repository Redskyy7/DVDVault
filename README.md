---

## Configuração do Ambiente Local

Para iniciar, é necessário configurar e preparar o ambiente local para o projeto. Siga os passos abaixo para configurar adequadamente o Redis, o PostgreSQL e o ambiente de desenvolvimento .NET 8.

### 1. Instalação do Redis

O Redis é usado para armazenar em cache informações temporárias e é uma parte essencial para leitura no sistema. Recomendo o uso do Docker para configurar uma instância local do Redis de forma rápida e fácil. Execute o seguinte comando em seu terminal:

```bash
docker run -d -p 6379:6379 --name redis redis
```

Este comando iniciará uma instância do Redis localmente, ouvindo na porta 6379.

### 2. Instalação do PostgreSQL

O PostgreSQL é o banco de dados principal utilizado em nosso projeto. Certifique-se de ter o PostgreSQL instalado em sua máquina. Você pode baixá-lo e instalá-lo a partir do site oficial: [https://www.postgresql.org/download/](https://www.postgresql.org/download/).

### 3. Instalação do .NET 8

O projeto é desenvolvido em C# utilizando o framework .NET 8. Certifique-se de ter o SDK do .NET 8 instalado em sua máquina. Você pode baixá-lo e instalá-lo a partir do site oficial da Microsoft: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).

## Configuração do Banco de Dados

Com o Redis e o PostgreSQL configurados, você precisa atualizar o banco de dados para garantir que esteja sincronizado com as últimas alterações no modelo de dados. Para isso, siga as instruções abaixo:

1. Abra um terminal e navegue até a pasta `DVDVault.Infra`.
2. Execute o comando `dotnet ef database update`.

Este comando aplicará todas as migrações pendentes e garantirá que seu banco de dados esteja atualizado com o esquema mais recente.

Após seguir essas etapas, seu ambiente local estará configurado e pronto para executar o projeto sem problemas.

---
