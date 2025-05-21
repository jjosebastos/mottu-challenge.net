# CodeCrafters 🛠

# Projeto API - Usuários (ASP.NET Core + Oracle)

Este projeto é uma API RESTful desenvolvida em **ASP.NET Core** com banco de dados **Oracle**, que permite realizar operações CRUD (Create, Read, Update, Delete) de usuários.

---

## ✅ Tecnologias Utilizadas

- **ASP.NET Core 6 ou superior**
- **Oracle Database** (utilizando Oracle.ManagedDataAccess)
- **Entity Framework Core** (ORM)
- **Migrations** para criação de tabelas e seed de dados
- **Visual Studio / Visual Studio Code**
- **Postman** (para testes de requisições)

---

## 📁 Estrutura da API

- `GET /api/users` – Lista todos os usuários
- `POST /api/users` – Cria um novo usuário
- `PUT /api/users/{id}` – Atualiza os dados de um usuário existente
- `DELETE /api/users/{id}` – Remove um usuário pelo ID

---

## 📦 Dependências via NuGet

Certifique-se de instalar os seguintes pacotes:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Oracle.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
