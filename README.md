
# mottu-challenge.net 💡

## Membros do Projeto 👨‍💻

- Nicolas Dobbeck Mendes  
- José Bezerra Bastos Neto  
- Thiago Henry Dias

## Descrição do Projeto 📃

No dinâmico cenário da mobilidade urbana, a gestão de grandes frotas como a da Mottu enfrenta desafios significativos. A ausência de um sistema centralizado e em tempo real para monitorar veículos resulta em perdas de tempo na localização de motos, ineficiência operacional, elevação de custos e decisões prejudicadas pela falta de dados precisos. Nós identificamos essa lacuna e desenvolvemos uma solução inovadora para revolucionar o mapeamento geográfico e o rastreamento em tempo real da sua frota de motos.<br><br>
Nossa solução oferece uma visão clara e dinâmica da distribuição e do status de cada veículo. Imagine ter um mapa interativo onde cada pátio é uma área delimitada e, dentro dela, marcadores visuais indicam a localização exata de cada moto, esteja ela parada ou em movimento. Essa funcionalidade proporciona um rastreamento em tempo real que permite a qualquer operador identificar instantaneamente a moto e sua posição, além de acessar informações cruciais como seu status operacional: se está disponível, em uso, em manutenção ou aguardando retirada. Isso não só facilita o gerenciamento da frota, mas empodera a equipe a visualizar rapidamente a quantidade de motos em cada local, promovendo uma gestão mais proativa e estratégica.<br><br>
A implementação deste sistema representa um avanço significativo para a Mottu, trazendo benefícios tangíveis que impactam diretamente a eficiência e a economia da operação. A eficiência operacional é aprimorada substancialmente, pois o acesso rápido à localização e ao status das motos elimina a necessidade de buscas manuais, agilizando processos como a retirada de veículos e a organização de manutenções. Isso se traduz em uma redução de custos notável, otimizando recursos e respondendo dinamicamente às demandas do mercado.<br><br>
Este projeto vai muito além de um simples sistema de rastreamento; ele é um passo fundamental na evolução da gestão de frotas da Mottu. Ao oferecer uma visão clara e em tempo real de seus ativos, nossa solução capacita a empresa a operar com uma eficiência sem precedentes. Acreditamos que essa capacidade de monitoramento inteligente não só aprimora as operações diárias, mas também abre portas para inovações futuras, contribuindo significativamente para um cenário de mobilidade urbana mais conectado, seguro e eficiente. Com este projeto, a Mottu está pavimentando o caminho para um futuro onde a logística de frotas é mais inteligente e responsiva.

Nesta disciplina, vamos desenvolver uma API RESTful desenvolvida com **ASP.NET Core**, conectada a um banco de dados **Oracle** via **Entity Framework Core**. Oferece operações CRUD para **Usuários** e **Papéis (Roles)**, com documentação interativa via Swagger.

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
