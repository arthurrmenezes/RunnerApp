# RunnerApp 🏃

**API de gerenciamento de treinos de corrida.**

## 💻 Tecnologias
- C# / .NET 8
- Entity Framework Core
- PostgreSQL
- Clean Architecture
- Arquitetura em camadas (Application, Domain, Infrastructure, WebAPI)

---

## 🚀 Como rodar o projeto

```bash
# Clonar o repositório
git clone https://github.com/seuusuario/RunnerApp.git

# Entrar na pasta backend
cd RunnerApp/backend

# Restaurar dependências
dotnet restore

# Rodar a aplicação
dotnet run
A API rodará por padrão em https://localhost:7077.
```

<h2 id="technologies">🚀 Endpoints</h2>
<b>Training</b>
- POST api/v1/training/create → Cria um treino
- GET api/v1/training/{id} → Busca um treino pelo ID

<b>Desenvolvido por Arthur Menezes.</b>
https://www.linkedin.com/in/arthuralbuquerquemenezes/
