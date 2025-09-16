# RunnerApp 🏃

**API de gerenciamento de treinos de corrida.**

## Visão Geral do Projeto

Esta é uma API RESTful desenvolvida como um projeto pessoal para gerenciar e registrar treinos de corrida. A aplicação permite que usuários criem contas e registrem suas atividades de corrida, salvando informações detalhadas como distância, duração, local e data.

O projeto foi construído com **.NET 8** e **C#**, seguindo os princípios da **Clean Architecture** para garantir um código desacoplado, testável, escalável e de fácil manutenção.

## 💻 Tecnologias
- C# / .NET 8
- Entity Framework Core
- PostgreSQL
- Clean Architecture
- Domain-Driven Design (DDD)
- Arquitetura em camadas (Application, Domain, Infrastructure, WebAPI)
- Identity (Autenticação)
- Dependency Injection (DI)
- Swagger (documentação da API)

---

## 🚀 Como executar o projeto

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [PostgreSQL](https://www.postgresql.org/download/)

### Passos

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/arthurrmenezes/RunnerApp.git
    cd RunnerApp
    ```

2.  **Configure a Connection String:**
    Abra o arquivo `appsettings.Development.json` e atualize a `PostgreSQLConnectionString` com as suas credenciais do PostgreSQL.
    ```json
    "PostgreSQLConnectionString": {
      "DefaultConnection": "Server=localhost;Port=5432;Database=RunnerAppDb;User Id=seu-usuario;Password=sua-senha;"
    }
    ```

3.  **Aplique as Migrations do Entity Framework:**
    Este comando irá criar o banco de dados e todas as tabelas necessárias.
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```
A API estará rodando e pronta para receber requisições!

---

<b>Desenvolvido por Arthur Menezes.</b>
linkedin.com/in/arthuralbuquerquemenezes/
