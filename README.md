# API Livros

Uma API RESTful desenvolvida com ASP.NET Core 8 para gerenciar um catálogo de livros e seus respectivos autores.

## Descrição

Este projeto implementa funcionalidades CRUD (Criar, Ler, Atualizar, Deletar) para as entidades `Livro` e `Autor`. Ele utiliza Entity Framework Core para persistência de dados em um banco de dados SQL Server e Swagger para documentação e teste interativo da API.

## Tecnologias Utilizadas

* **ASP.NET Core 8:** Framework para construção de aplicações web e APIs.
* **Entity Framework Core 8:** ORM para interação com o banco de dados.
* **SQL Server:** Sistema de gerenciamento de banco de dados.
* **Swagger (OpenAPI):** Para documentação e teste da API.
* **C#:** Linguagem de programação principal.

## Recursos (Endpoints da API)

A API expõe os seguintes endpoints principais:

### Autores (`/api/Autor`)

* **`GET /api/Autor/ListarAutores`**: Lista todos os autores cadastrados.
* **`GET /api/Autor/BuscarAutorPorId/{idAutor}`**: Busca um autor específico pelo seu ID.
* **`GET /api/Autor/BuscarAutorPorIdLivro/{idLivro}`**: Busca o autor de um livro específico (passando o ID do livro como parâmetro de rota, embora a API espere `idLivro` como query string, conforme Swagger).
* **`POST /api/Autor/CriarAutor`**: Cria um novo autor.
    * Corpo da requisição (JSON):
        ```json
        {
          "nome": "string",
          "sobreNome": "string"
        }
        ```
* **`PUT /api/Autor/EditarAutor`**: Atualiza os dados de um autor existente.
    * Corpo da requisição (JSON):
        ```json
        {
          "nome": "string",
          "sobreNome": "string",
          "id": 0
        }
        ```
* **`DELETE /api/Autor/ExcluirAutor?idAutor={idAutor}`**: Exclui um autor pelo seu ID (passado como query string).

### Livros (`/api/Livro`)

* **`GET /api/Livro/ListarLivros`**: Lista todos os livros cadastrados, incluindo informações do autor.
* **`GET /api/Livro/BuscarLivroPorId/{idLivro}`**: Busca um livro específico pelo seu ID.
    * *Observação: Internamente, este endpoint pode estar chamando `BuscarLivroPorIdAutor` no serviço, o que pode precisar de revisão.*
* **`GET /api/Livro/BuscarLivroPorIdAutor/{idAutor}`**: Busca todos os livros de um autor específico pelo ID do autor.
* **`POST /api/Livro/CriarLivro`**: Cria um novo livro.
    * Corpo da requisição (JSON):
        ```json
        {
          "titulo": "string",
          "autor": { // AutorVinculoDto
            "id": 0,
            "name": "string", // Opcional aqui, o ID é o principal para vincular
            "lastName": "string" // Opcional aqui
          }
        }
        ```
* **`PUT /api/Livro/EditarLivro`**: Atualiza os dados de um livro existente.
    * Corpo da requisição (JSON):
        ```json
        {
          "id": 0,
          "titulo": "string",
          "autor": { // AutorVinculoDto
            "id": 0,
            "name": "string", // Opcional aqui
            "lastName": "string" // Opcional aqui
          }
        }
        ```
* **`DELETE /api/Livro/ExcluirLivro?idLivro={idLivro}`**: Exclui um livro pelo seu ID (passado como query string).

## Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior.
* SQL Server (Express, Developer, ou outra edição).
* Um editor de código como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

## Configuração do Ambiente

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/seu-usuario/seu-repositorio-api-livros.git](https://github.com/seu-usuario/seu-repositorio-api-livros.git)
    cd seu-repositorio-api-livros/API Livros/Web API
    ```

2.  **Configure a String de Conexão:**
    * Abra o arquivo `appsettings.json` (ou `appsettings.Development.json` para ambiente de desenvolvimento).
    * Modifique a `DefaultConnection` na seção `ConnectionStrings` para apontar para sua instância do SQL Server.
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "server=SEU_SERVIDOR_SQL;database=WebApiLivro;trusted_connection=true;trustservercertificate=true"
        }
        ```
        * Substitua `SEU_SERVIDOR_SQL` pelo nome do seu servidor SQL (ex: `localhost\SQLEXPRESS` ou `.`).
        * O banco de dados `WebApiLivro` será criado pelas migrações se não existir.

3.  **Aplique as Migrações do Entity Framework Core:**
    * Abra um terminal ou prompt de comando na pasta do projeto `Web API`.
    * Execute o seguinte comando para aplicar as migrações e criar/atualizar o banco de dados:
        ```bash
        dotnet ef database update
        ```
    * Isso aplicará a migração `20250530135644_CriandoBrancoDeDados` que cria as tabelas `Autores` e `Livros`.

## Como Executar

1.  **Pelo Terminal:**
    * Navegue até a pasta do projeto `Web API`.
    * Execute o comando:
        ```bash
        dotnet run
        ```

2.  **Pelo Visual Studio:**
    * Abra a solução ou o projeto no Visual Studio.
    * Pressione F5 ou clique no botão "Play" (geralmente com o perfil "http" ou "https").

3.  **Acessando a API e o Swagger:**
    * Após iniciar, a aplicação estará disponível (por padrão) em:
        * HTTP: `http://localhost:5042`
        * HTTPS: `https://localhost:7004`
    * A interface do Swagger para testar a API estará disponível em `/swagger` (ex: `http://localhost:5042/swagger` ou `https://localhost:7004/swagger`).

## Estrutura do Projeto

* **`/Controllers`**: Contém os controladores da API que lidam com as requisições HTTP.
* **`/Data`**: Contém o `AppDbContext` para a configuração do Entity Framework Core.
* **`/DTO`**: (Data Transfer Objects) Classes usadas para transferir dados entre as camadas da aplicação e para as requisições/respostas da API.
* **`/Migrations`**: Contém os arquivos de migração do Entity Framework Core para o esquema do banco de dados.
* **`/Models`**: Contém as classes de entidade (`AutorModel`, `LivroModel`) que representam as tabelas do banco de dados, e o `ResponseModel` para padronizar as respostas.
* **`/Services`**: Contém as interfaces (`AutorInterface`, `LivroInterface`) e suas implementações (`AutorService`, `LivroServicecs`) que encapsulam a lógica de negócios.
* **`Program.cs`**: Arquivo de inicialização da aplicação, onde os serviços e o pipeline de requisições são configurados.


* **`appsettings.json`**: Arquivo de configuração principal da aplicação.

---
