# Mottu Challenge API - .NET 💡

API RESTful desenvolvida em .NET 8 como parte do desafio técnico da Mottu. A API implementa a fundação de um sistema de gerenciamento de frota, incluindo o controle de Motos, Usuários e Perfis de Acesso.

## Membros do Projeto 👨‍💻
* Nicolas Dobbeck Mendes
* José Bezerra Bastos Neto
* Thiago Henry Dias

## Descrição do Projeto 📃

No dinâmico cenário da mobilidade urbana, a gestão de grandes frotas como a da Mottu enfrenta desafios significativos. A ausência de um sistema centralizado e em tempo real para monitorar veículos resulta em perdas de tempo na localização de motos, ineficiência operacional, elevação de custos e decisões prejudicadas pela falta de dados precisos. Nós identificamos essa lacuna e desenvolvemos uma solução inovadora para revolucionar o mapeamento geográfico e o rastreamento em tempo real da sua frota de motos.<br><br>
Nossa solução oferece uma visão clara e dinâmica da distribuição e do status de cada veículo. Imagine ter um mapa interativo onde cada pátio é uma área delimitada e, dentro dela, marcadores visuais indicam a localização exata de cada moto, esteja ela parada ou em movimento. Essa funcionalidade proporciona um rastreamento em tempo real que permite a qualquer operador identificar instantaneamente a moto e sua posição, além de acessar informações cruciais como seu status operacional: se está disponível, em uso, em manutenção ou aguardando retirada. Isso não só facilita o gerenciamento da frota, mas empodera a equipe a visualizar rapidamente a quantidade de motos em cada local, promovendo uma gestão mais proativa e estratégica.<br><br>
A implementação deste sistema representa um avanço significativo para a Mottu, trazendo benefícios tangíveis que impactam diretamente a eficiência e a economia da operação. A eficiência operacional é aprimorada substancialmente, pois o acesso rápido à localização e ao status das motos elimina a necessidade de buscas manuais, agilizando processos como a retirada de veículos e a organização de manutenções. Isso se traduz em uma redução de custos notável, otimizando recursos e respondendo dinamicamente às demandas do mercado.<br><br>
Este projeto vai muito além de um simples sistema de rastreamento; ele é um passo fundamental na evolução da gestão de frotas da Mottu. Ao oferecer uma visão clara e em tempo real de seus ativos, nossa solução capacita a empresa a operar com uma eficiência sem precedentes. Acreditamos que essa capacidade de monitoramento inteligente não só aprimora as operações diárias, mas também abre portas para inovações futuras, contribuindo significativamente para um cenário de mobilidade urbana mais conectado, seguro e eficiente. Com este projeto, a Mottu está pavimentando o caminho para um futuro onde a logística de frotas é mais inteligente e responsiva.

## Justificativa da Arquitetura

A arquitetura escolhida para este projeto foi a **API Web ASP.NET Core**, utilizando um design que separa as responsabilidades em diferentes camadas, promovendo um código mais limpo e de fácil manutenção.

* **.NET 8 e C#:** Escolhidos por sua performance, ecossistema robusto e ferramentas de desenvolvimento modernas.
* **Entity Framework Core (EF Core):** Utilizado como ORM para abstrair o acesso ao banco de dados Oracle, permitindo um desenvolvimento rápido e seguro da camada de dados através do padrão Code-First e Migrations.
* **Padrão de DTOs (Data Transfer Objects):** Foram criados objetos específicos para as requisições (`Request`) e respostas (`Response`) da API. Isso garante que a API não exponha os modelos internos do banco de dados, aumentando a segurança e permitindo que a API evolua de forma independente.
* **AutoMapper:** Ferramenta utilizada para automatizar a conversão entre as entidades do banco e os DTOs, reduzindo código repetitivo.
* **Boas Práticas REST:** A API foi desenvolvida seguindo princípios REST, incluindo o uso correto de verbos HTTP, status codes (`200`, `201`, `204`, `404`), implementação de **Paginação** para listagens e **HATEOAS** para tornar a API mais "descobrível".
* **Swagger (OpenAPI):** A documentação da API foi gerada a partir de comentários XML no código, garantindo que a documentação esteja sempre sincronizada com os endpoints e inclua exemplos de uso.
* **Testes Unitários (xUnit):** Foi criado um projeto de testes separado para garantir a qualidade e o funcionamento correto da lógica de negócio dos controllers, utilizando um banco de dados em memória para isolamento e velocidade.

## Tecnologias Utilizadas
* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* Oracle Provider para EF Core
* AutoMapper
* Swashbuckle.AspNetCore (Swagger)
* xUnit (Framework de Teste)
* Moq (Biblioteca de Mock)

## Instruções de Execução

1.  **Clonar o Repositório**
    ```bash
    git clone https://github.com/jjosebastos/mottu-challenge.net.git
    cd mottu-challenge.net
    ```

2.  **Configurar a Conexão com o Banco**
    * No arquivo `mottu-challenge/appsettings.json`, altere a `ConnectionString` "DefaultConnection" com as suas credenciais do banco de dados Oracle.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "User Id=SEU_ID;Password=SUA_SENHA;Data Source=//oracle.fiap.com.br:1521/ORCL"
    }
    ```

3.  **Aplicar as Migrations do Banco de Dados**
    * Abra um terminal na pasta do projeto (ex: `.../mottu-challenge.net/mottu-challenge/`).
    * Execute o comando abaixo para criar/atualizar as tabelas:
    ```bash
    dotnet ef database update
    ```

4.  **Executar a Aplicação**
    * Navegue de volta para a pasta da solução (ex: `.../mottu-challenge.net/`).
    * No terminal, execute:
    ```bash
    dotnet run
    ```
    * A API estará rodando. As URLs (incluindo a porta) serão exibidas no terminal.

5.  **Acessar a Documentação Interativa**
    * Abra o navegador e acesse o endereço do Swagger UI, por exemplo: `http://localhost:[PORTA]/swagger`.

## Rotas da API

A documentação completa e interativa com exemplos de payload está disponível na página do Swagger.

#### MotorcycleController
| Método | Rota | Resposta |
|---|---|---|
| GET | /api/motorcycle | 200 OK + lista de `MotorcycleResponse` |
| GET | /api/motorcycle/{id} | 200 OK + `MotorcycleResponse` ou 404 |
| POST | /api/motorcycle | 201 Created + `MotorcycleResponse` |
| PUT | /api/motorcycle/{id} | 204 No Content ou 404 |
| DELETE | /api/motorcycle/{id} | 204 No Content ou 404 |

#### UserController
| Método | Rota | Resposta |
|---|---|---|
| GET | /api/user | 200 OK + lista de `UserResponse` |
| GET | /api/user/{id} | 200 OK + `UserResponse` ou 404 |
| POST | /api/user | 201 Created + `UserResponse` |
| PUT | /api/user/{id} | 204 No Content ou 404 |
| DELETE | /api/user/{id} | 204 No Content ou 404 |

#### RoleController
| Método | Rota | Resposta |
|---|---|---|
| GET | /api/role | 200 OK + lista de `RoleResponse` |
| GET | /api/role/{id} | 200 OK + `RoleResponse` ou 404 |
| POST | /api/role | 201 Created + `RoleResponse` |
| PUT | /api/role/{id} | 204 No Content ou 404 |
| DELETE | /api/role/{id} | 204 No Content ou 404 |

### Exemplo de Uso (cURL)

**Criando uma nova moto:**
```bash
curl -X POST "http://localhost:5225/api/Motorcycle" -H "Content-Type: application/json" -d "{\"year\": 2024, \"model\": \"Yamaha Fazer 250\", \"plate\": \"DEF5G67\"}"
```

## Executando os Testes

O projeto inclui um projeto de testes (`mottu-challenge.Tests`) utilizando xUnit para garantir a qualidade do código.

Para executar todos os testes da solução, navegue até a pasta raiz (`mottu-challenge.net`) e utilize o seguinte comando:

```bash
dotnet test
```
