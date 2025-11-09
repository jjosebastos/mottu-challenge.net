# Mottu Challenge API - ASP.NET 💡

API RESTful de gerenciamento de frota desenvolvida em **.NET 8** como parte do desafio técnico da Mottu. A solução implementa o controle completo de Motos, Usuários e Perfis de Acesso, incorporando segurança via **Autenticação JWT**, integrações avançadas com **Oracle PL/SQL** e recursos de **Machine Learning (ML.NET)**.

---

## Membros do Projeto 👨‍💻
* Nicolas Dobbeck Mendes
* José Bezerra Bastos Neto
* Thiago Henry Dias

---

## Descrição do Projeto 📃

No dinâmico cenário da mobilidade urbana, a gestão de grandes frotas como a da Mottu enfrenta desafios significativos. A ausência de um sistema centralizado e em tempo real para monitorar veículos resulta em perdas de tempo na localização de motos, ineficiência operacional, elevação de custos e decisões prejudicadas pela falta de dados precisos. Nós identificamos essa lacuna e desenvolvemos uma solução inovadora para revolucionar o mapeamento geográfico e o rastreamento em tempo real da sua frota de motos.

Nossa solução oferece uma visão clara e dinâmica da distribuição e do status de cada veículo. Imagine ter um mapa interativo onde cada pátio é uma área delimitada e, dentro dela, marcadores visuais indicam a localização exata de cada moto, esteja ela parada ou em movimento. Essa funcionalidade proporciona um rastreamento em tempo real que permite a qualquer operador identificar instantaneamente a moto e sua posição, além de acessar informações cruciais como seu status operacional: se está disponível, em uso, em manutenção ou aguardando retirada.

Este projeto vai além de um simples sistema de rastreamento; ele é um passo fundamental na evolução da gestão de frotas da Mottu. Ao oferecer uma visão clara e em tempo real de seus ativos, nossa solução capacita a empresa a operar com uma eficiência sem precedentes, além de abrir portas para inovações futuras, como a previsão de falhas mecânicas com **ML.NET** e a utilização de funções de banco de dados para lógica de negócio, como a classificação do status de baterias via **PL/SQL**.

---

## Justificativa da Arquitetura

A arquitetura escolhida é a **API Web ASP.NET Core**, seguindo um design em camadas que separa as responsabilidades (Controladores, Serviços, Repositórios), visando um código limpo e de fácil manutenção.

* **.NET 8 e C#:** Escolhidos por sua performance, ecossistema robusto e ferramentas de desenvolvimento modernas.
* **Segurança (JWT):** Implementação de autenticação via **JSON Web Tokens (JWT)** para proteger as rotas. O *hash* de senhas é realizado com **BCrypt.Net**, e a validação é feita via `Microsoft.AspNetCore.Authentication.JwtBearer`.
* **Acesso a Dados (EF Core + Oracle):** O **Entity Framework Core (EF Core)** é usado como ORM, configurado com o *provider* da Oracle para abstrair o acesso ao banco de dados, utilizando o padrão Code-First e Migrations.
* **Design REST:** A API adota princípios REST, incluindo o uso de **Paginação**, *status codes* semânticos e **HATEOAS** (Hypermedia as an Engine of Application State) para tornar as respostas mais ricas e "descobríveis".
* **Integrações Avançadas:**
    * **Oracle PL/SQL:** Demonstra a capacidade de chamar funções de banco de dados (Stored Procedures/Functions), como no `MottuController`.
    * **Machine Learning (ML.NET):** Integração de um modelo ML.NET para *predictions* em um *endpoint* dedicado.
* **Testes e Documentação:**
    * **Testes Unitários (xUnit & Moq):** Cobertura de testes de unidade para garantir a qualidade da lógica de negócio.
    * **Swagger (OpenAPI):** Documentação interativa gerada via *reflection* e comentários XML.
    * **Versionamento:** Implementação de versionamento via URI (`api/v{version:apiVersion}/[controller]`) com o pacote `Asp.Versioning.Mvc`.

---

## Tecnologias Utilizadas 🚀
| Categoria | Pacote/Tecnologia | Descrição |
| :--- | :--- | :--- |
| **Framework** | .NET 8, ASP.NET Core Web API | Core do projeto |
| **Acesso a Dados** | Entity Framework Core, Oracle.EntityFrameworkCore | ORM e Provider para Banco Oracle |
| **Segurança** | Microsoft.AspNetCore.Authentication.JwtBearer, BCrypt.Net | Autenticação JWT e Hashing de Senhas |
| **Testes** | xUnit, Moq, Microsoft.NET.Test.Sdk | Framework de testes e Mocking |
| **Utilitários** | AutoMapper, Swashbuckle.AspNetCore (Swagger) | Mapeamento de objetos (DTOs) e Documentação |
| **Inovação** | Microsoft.Extensions.ML, Microsoft.ML | Integração com Machine Learning |

---

## Instruções de Execução

### 1. Pré-requisitos
* .NET 8 SDK instalado.
* Acesso a uma instância do Banco de Dados Oracle.

### 2. Configuração Inicial

1.  **Clonar o Repositório**
    ```bash
    git clone [https://github.com/jjosebastos/mottu-challenge.net.git](https://github.com/jjosebastos/mottu-challenge.net.git)
    cd mottu-challenge.net
    ```

2.  **Configurar a Conexão com o Banco**
    * No arquivo `mottu-challenge/appsettings.json`, atualize a `ConnectionString` "DefaultConnection" com suas credenciais do banco de dados Oracle:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "User Id=SEU_ID;Password=SUA_SENHA;Data Source=//oracle.fiap.com.br:1521/ORCL"
    }
    ```

3.  **Aplicar as Migrations do Banco de Dados**
    * Abra um terminal na pasta do projeto (`.../mottu-challenge.net/mottu-challenge/`).
    * Execute o comando abaixo para criar/atualizar as tabelas:
    ```bash
    dotnet ef database update
    ```

### 3. Rodar a Aplicação

* Navegue para a pasta raiz da solução (ex: `.../mottu-challenge.net/`).
* No terminal, execute:
    ```bash
    dotnet run --project mottu-challenge
    ```
* A API estará rodando. A URL será exibida no terminal.

### 4. Acessar a Documentação Interativa
* Abra o navegador e acesse o endereço do **Swagger UI**, por exemplo: `http://localhost:[PORTA]/swagger`.

---

## Rotas da API 🗺️

Todas as rotas (exceto `/Auth/login`, `/Mottu`, `/Prediction` e `/health`) são protegidas e requerem um **Token JWT** válido no cabeçalho `Authorization: Bearer <token>`.

### Autenticação e Previsão (v1.0)

| Controller | Método | Rota | Descrição |
| :--- | :--- | :--- | :--- |
| `AuthController` | **POST** | `/api/v1.0/Auth/login` | Autentica e retorna o **Token JWT**. |
| `PredictionController` | **POST** | `/api/v1.0/Prediction` | Realiza **Previsão** via ML.NET. |

### Gestão de Frota e Usuários (v1.0)

| Controller | Método | Rota Base | Operações |
| :--- | :--- | :--- | :--- |
| `MotorcycleController` | GET, POST, PUT, DELETE | `/api/v1.0/Motorcycle/{id}` | CRUD de Motos (GET paginado com HATEOAS) |
| `UserController` | GET, POST, PUT, DELETE | `/api/User/{id}` | CRUD de Usuários |
| `RoleController` | GET, POST, PUT, DELETE | `/api/Role/{id}` | CRUD de Perfis de Acesso |

### Integrações e Monitoramento (Sem Versionamento)

| Controller | Método | Rota | Descrição |
| :--- | :--- | :--- | :--- |
| *Monitoramento* | **GET** | `http://localhost:[PORTA]/health` | Endpoint de **Health Check** (Status da API e dependências). |
| `MottuController` | **GET** | `/api/Mottu/bateria/status?nivelBateria={nivel}` | Classifica o status da bateria chamando uma **Function Oracle (PL/SQL)**. |

---

## Executando os Testes Unitários com xUnit e Moq ✅

O projeto inclui um projeto de testes separado (`mottu-challenge.Tests`) utilizando **xUnit** como framework de teste e **Moq** para isolar as dependências (como `DbContext` e `Services`) nos testes, garantindo que apenas a lógica de negócio seja validada.

Para executar todos os testes da solução, navegue até a pasta raiz (`mottu-challenge.net/`) e utilize o seguinte comando:

```bash
dotnet test
