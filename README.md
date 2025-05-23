# mottu-challenge.net 💡

## Descrição do Projeto 📃

API RESTful desenvolvida com **ASP.NET Core**, conectada a um banco de dados **Oracle** via **Entity Framework Core**. Oferece operações CRUD para **Usuários** e **Papéis (Roles)**, além de um endpoint de recuperação de senha usando **SendGrid**.

Principais funcionalidades:

* CRUD completo para **Usuários** e **Papéis**
* Endpoint de recuperação de senha (`recuperar-senha`) que envia e-mails via SendGrid
* Documentação interativa com **Swagger**
* Configuração do banco de dados usando **EF Core Migrations**

---

## Tecnologias e Pacotes

* .NET 8.0 LTS
* ASP.NET Core Web API
* Entity Framework Core (`Oracle.EntityFrameworkCore`)
* AutoMapper
* SendGrid (serviço de e-mail)
* Swashbuckle.AspNetCore (Swagger)

---

## Configuração do Ambiente

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/seu-usuario/mottu-challenge.net.git
   cd mottu-challenge.net
   ```

2. **Configure o *appsettings.json***:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "User Id=seu-id;Password=sua-senha;Data Source=//oracle.fiap.com.br:1521/ORCL"
     }
   }
   ```

3. **Instale as dependências**:

   ```bash
   dotnet restore
   ```

4. **Crie e aplique as migrations**:

   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Execute a aplicação**:

   ```bash
   dotnet run
   ```

6. **Acesse o Swagger UI**:

   Abra no navegador: `https://localhost:7008/swagger`

---

## Rotas da API

### Usuários (`UserController`)

Esta seção detalha cada endpoint disponível para operações com **Usuários**, incluindo método HTTP, rota, parâmetros de entrada e possíveis respostas.

| Método     | Rota                        | Parâmetros / Body                    | Resposta                              |
| ---------- | --------------------------- | ------------------------------------ | ------------------------------------- |
| **GET**    | `/api/user`                 | —                                    | `200 OK` + lista de Usuários          |
| **GET**    | `/api/user/{id}`            | PathParam: `id`                      | `200 OK` + Usuário ou `404 Not Found` |
| **POST**   | `/api/user`                 | JSON: `UserRequest`                  | `201 Created` + `UserResponse`        |
| **PUT**    | `/api/user/{id}`            | PathParam: `id`, JSON: `UserRequest` | `200 OK` + `UserResponse`             |
| **DELETE** | `/api/user/{id}`            | PathParam: `id`                      | `204 No Content`                      |
| **POST**   | `/api/user/recuperar-senha` | QueryParam: `paraEmail`              | `200 OK` / `400 Bad Request`          |

**Exemplo de requisição**:

```http
POST /api/user/recuperar-senha?paraEmail=usuario@exemplo.com HTTP/1.1
Host: localhost:7008
```

### Papéis (`RoleController`)

Endpoints para gerenciamento de **Papéis (Roles)**.

| Método     | Rota             | Parâmetros / Body                    | Resposta                                     |
| ---------- | ---------------- | ------------------------------------ | -------------------------------------------- |
| **GET**    | `/api/role`      | —                                    | `200 OK` + lista de Papéis                   |
| **GET**    | `/api/role/{id}` | PathParam: `id`                      | `200 OK` + `RoleResponse` ou `404 Not Found` |
| **POST**   | `/api/role`      | JSON: `RoleRequest`                  | `201 Created` + `RoleResponse`               |
| **PUT**    | `/api/role/{id}` | PathParam: `id`, JSON: `RoleRequest` | `200 OK` + `RoleResponse`                    |
| **DELETE** | `/api/role/{id}` | PathParam: `id`                      | `204 No Content`                             |

---

## Documentação (Swagger)

A documentação interativa está disponível em:

```
https://localhost:7008/swagger
```

Inclui exemplos de requisição e resposta para todos os endpoints.

---

## Observações

* **Autenticação/Tokens**: JWT não está implementado neste sprint; considere proteger endpoints críticos em sprints futuros.
* **Variáveis de ambiente**: em produção, use `User Secrets` ou variáveis de ambiente para armazenar `ConnectionStrings` e `SendGrid:ApiKey`.
* **Esquema do banco de dados**: gerado via migrations (tabelas `Users`, `Roles`).

