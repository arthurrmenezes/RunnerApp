<h1 align="center">🏃‍♂️ RunnerApp - Gerencie Treinos de Corrida</h1>

<p align="center">
  Aplicação desenvolvida com <strong>.NET 8</strong>, <strong>C#</strong> e <strong>PostgreSQL</strong> para registrar e gerenciar treinos de corrida. A aplicação permite que usuários criem contas e registrem suas atividades de corrida, salvando informações detalhadas como distância, duração, local e data.<br>
  Monolito com arquitetura em camadas, Clean Architecture, Domain-Driven Design (DDD), autenticação JWT, upload de arquivos e mais.
</p>

## 🚀 Sobre o Projeto

O **RunnerApp** é um SaaS pensado para corredores que desejam registrar, acompanhar e gerenciar seus treinos, com endpoints para manipulação de contas, autenticação, treinos e upload de fotos de perfil. 

---

## 🧱 Tecnologias Utilizadas

- **.NET 8**
- **C#**
- **PostgreSQL**
- **Entity Framework Core**
- **Swagger / OpenAPI**
- **XUnit**

---

## 🧠 Conceitos e Padrões Implementados

### ✔ **Clean Architecture**
A aplicação segue uma estrutura em camadas:

- **Domain**
- **Application**
- **Infrastructure**
- **WebAPI**

Cada camada com sua responsabilidade clara, garantindo baixo acoplamento, alta testabilidade e escalabilidade.

---

### ✔ **Padrões utilizados**
- **Design Patterns** (Repository, Unit of Work)
- **Domain-Driven Design** (Entity, Value Object, Bounded Contexts)

---

### ✔ **Auth**
Sistema de autenticação estruturado com hash seguro de senhas e gerenciamento de usuários.
- Identity
- JWT Bearer Tokens
- Access Tokens
- Refresh Tokens

### ✔ **Middleware de Exceções Personalizado**
Erros são interceptados globalmente e retornados em um padrão consistente:

```json
{
  "statusCode": 400,
  "message": "Validation error",
  "description": "..."
}
```
### ✔ **Rate Limiting**
### ✔ **Armazenamento e Manipulação de Arquivos**
- Upload de foto de perfil (jpg, jpeg e png)
- Armazenamento local em wwwroot/uploads
- Obtenção da imagem em bytes
### ✔ **Testes Unitários com XUnit**
---
## 🛠 Como Rodar o Projeto Localmente
### 1️⃣ Instalar o .NET 8
### 2️⃣ Instalar uma IDE
Você pode usar:
- Visual Studio 2022
- VS Code + C# Dev Kit
### 3️⃣ Configurar o Banco de Dados PostgreSQL
- Instale o PostgreSQL:
- Crie o banco:
```json
CREATE DATABASE runnerapp;
```
### 4️⃣ Configurar o appsettings.json
Acesse o arquivo appsettings.json no camada WebApi e configure. Exemplo:
```json
"JwtSettings": {
  "PrivateKey": "COLOQUE-AQUI-UMA-PRIVATE-KEY-LONGA-E-SEGURA",
  "Issuer": "RunnerApp",
  "Audience": "RunnerAppUsers",
  "ExpiresMinutes": 15,
  "RefreshTokenExpiresDays": 7
},
"ConnectionStrings": {
  "PostgreSQLConnectionString": "Host=localhost;Port=5432;Username=postgres;Password=123456;Database=runnerapp;"
}
```
### 5️⃣ Gerar Migrations
No terminal, dentro da pasta backend:
```json
dotnet ef migrations add InitialCreate -p RunnerApp.Infrastructure -s RunnerApp.WebApi
```
### 6️⃣ Aplicar Migrations ao Banco
No terminal, dentro da pasta backend:
```json
dotnet ef database update -p RunnerApp.Infrastructure -s RunnerApp.WebApi
```
### 7️⃣ Rodar o Projeto
No terminal, dentro da pasta backend:
```json
dotnet ef database update -p RunnerApp.Infrastructure -s RunnerApp.WebApi
```
### A API estará disponível em:
```json
https://localhost:7015
```

👨‍💻 Desenvolvedor: Arthur Menezes<br>
🔗 <a href="https://linkedin.com/in/arthuralbuquerquemenezes/">LinkedIn</a>
