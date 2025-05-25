
# mottu-challenge.net 💡

## Membros do Projeto 👨‍💻

- Nicolas Dobbeck Mendes  
- José Bezerra Bastos Neto  
- Thiago Henry Dias

## Descrição do Projeto 📃

API RESTful desenvolvida com **ASP.NET Core**, conectada a um banco de dados **Oracle** via **Entity Framework Core**. Oferece operações CRUD para **Usuários** e **Papéis (Roles)**, com documentação interativa via Swagger.

### Principais funcionalidades:

* CRUD completo para **Usuários** e **Papéis**
* Consulta de usuários por papel (`GET /api/role/by-role`)
* Documentação interativa com **Swagger**
* Configuração do banco de dados usando **EF Core Migrations**

---

## Tecnologias e Pacotes

* .NET 8.0 LTS
* ASP.NET Core Web API
* Entity Framework Core (`Oracle.EntityFrameworkCore`)
* AutoMapper
* Swashbuckle.AspNetCore (Swagger)

### Pacotes NuGet Instalados

- AutoMapper.Extensions.Microsoft.DependencyInjection  
- Microsoft.EntityFrameworkCore  
- Microsoft.EntityFrameworkCore.Design  
- Microsoft.EntityFrameworkCore.Tools  
- Oracle.EntityFrameworkCore  
- Swashbuckle.AspNetCore  

---

## Configuração do Ambiente

1. **Clone o repositório**:

   ```bash
   git clone https://github.com/seu-usuario/mottu-challenge.net.git
   cd mottu-challenge.net
   ```

2. **Configure o `appsettings.json`**:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "User Id=seu-id;Password=sua-senha;Data Source=//oracle.fiap.com.br:1521/ORCL"
     }
   }
   ```

3. **Instale as dependências NuGet**:

   Abra o Visual Studio e navegue até:  
   `Tools > NuGet Package Manager > Manage NuGet Packages for Solution...`

   Certifique-se de instalar os pacotes listados neste README.

4. **Crie e aplique as migrations**:

   ```powershell
   PM> Add-Migration InitialCreate
   PM> Update-Database
   ```

5. **Execute a aplicação**:

   No Visual Studio: pressione `F5` ou `Ctrl + F5`.

6. **Acesse o Swagger UI**:

   ```
   https://localhost:7088/swagger
   ```

---

## Rotas da API

### Usuários (`UserController`)

| Método     | Rota               | Parâmetros / Body                    | Resposta                              |
|------------|--------------------|--------------------------------------|---------------------------------------|
| **GET**    | `/api/user`        | —                                    | `200 OK` + lista de `UserResponse`    |
| **GET**    | `/api/user/{id}`   | PathParam: `id`                      | `200 OK` + `UserResponse` ou `404`    |
| **POST**   | `/api/user`        | Body JSON: `UserRequest`             | `201 Created` + `UserResponse`        |
| **PUT**    | `/api/user/{id}`   | PathParam: `id`, Body JSON: `UserRequest` | `200 OK` + `UserResponse`        |
| **DELETE** | `/api/user/{id}`   | PathParam: `id`                      | `204 No Content`                      |

---

### Papéis (`RoleController`)

| Método     | Rota                   | Parâmetros / Body                    | Resposta                                     |
|------------|------------------------|--------------------------------------|----------------------------------------------|
| **GET**    | `/api/role`            | —                                    | `200 OK` + lista de `RoleResponse`           |
| **GET**    | `/api/role/{id}`       | PathParam: `id`                      | `200 OK` + `RoleResponse` ou `404 Not Found` |
| **POST**   | `/api/role`            | Body JSON: `RoleRequest`             | `201 Created` + `RoleResponse`               |
| **PUT**    | `/api/role/{id}`       | PathParam: `id`, Body JSON: `RoleRequest` | `200 OK` + `RoleResponse`              |
| **DELETE** | `/api/role/{id}`       | PathParam: `id`                      | `204 No Content`                             |
| **GET**    | `/api/role/by-role`    | QueryParam: `roleId` (ex: `?roleId=1`) | `200 OK` + lista de `UserResponse`          |


## Documentação (Swagger)

A documentação interativa está disponível em:

```
https://localhost:7088/swagger
```

Inclui exemplos de requisição e resposta para todos os endpoints.

---

## Observações

* 🔐 **Autenticação**: JWT e autenticação ainda não implementados. Recomenda-se sua inclusão em futuras versões.
* 🔒 **Segurança de configuração**: Use variáveis de ambiente ou User Secrets para armazenar a `ConnectionString` em produção.
* 🗃️ **Banco de Dados**: Tabelas `Users` e `Roles` geradas via migrations (EF Core).

---
