# WizCo

Sistema de gerenciamento de pedidos desenvolvido em .NET 8 seguindo princípios de Clean Architecture e Domain-Driven Design (DDD).

## Sobre o Projeto

WizCo é uma API REST para gestão de pedidos (orders) que permite criar, consultar, listar e cancelar pedidos de forma eficiente e segura. O projeto foi desenvolvido com foco em boas práticas de desenvolvimento, separação de responsabilidades e testabilidade.

## Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em 4 camadas principais:

```
WizCo/
├── src/
│   ├── WizCo.Api/              # Camada de Apresentação (API)
│   ├── WizCo.Application/      # Camada de Aplicação (Use Cases)
│   ├── WizCo.Domain/           # Camada de Domínio (Regras de Negócio)
│   └── WizCo.Infrastructure/   # Camada de Infraestrutura (Dados)
└── tests/
    └── WizCo.Infrastructure.Tests/
```

### Camadas

- **WizCo.Api**: Controllers, configurações da API e injeção de dependências
- **WizCo.Application**: Serviços de aplicação, DTOs, validações e mapeamentos
- **WizCo.Domain**: Entidades, interfaces de repositórios, filtros e regras de negócio
- **WizCo.Infrastructure**: Implementação de repositórios, contexto do banco de dados e serviços

## Tecnologias Utilizadas

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0.23**
- **SQLite** (banco de dados)
- **FluentValidation** (validação de DTOs)
- **Swashbuckle (Swagger)** (documentação da API)
- **X.PagedList** (paginação)

## Funcionalidades

- ✅ Criar pedidos com múltiplos itens
- ✅ Listar pedidos com filtros e paginação
- ✅ Consultar pedido por ID
- ✅ Cancelar pedidos (com validação de status)
- ✅ Cálculo automático do valor total do pedido
- ✅ Validação de dados com FluentValidation

## Domínio

### Entidades

**Order (Pedido)**
- `Id`: Identificador único
- `ClientName`: Nome do cliente (máx. 200 caracteres)
- `CreatedAt`: Data de criação
- `Status`: Status do pedido (New, Paid, Canceled)
- `TotalValue`: Valor total calculado automaticamente
- `Items`: Coleção de itens do pedido
- `Visible`: Flag para soft delete

**ItemOrder (Item do Pedido)**
- `Id`: Identificador único
- `ProductName`: Nome do produto (máx. 200 caracteres)
- `Amount`: Quantidade
- `UnitPrice`: Preço unitário
- `Subtotal`: Subtotal calculado (Amount × UnitPrice)
- `OrderId`: Referência ao pedido

## Configuração e Instalação

### Pré-requisitos

- .NET 8.0 SDK

### Passos para Execução

1. **Clone o repositório**
```bash
git clone https://github.com/FlpAugusto/WizCo.git
cd WizCo
```

2. **Restaure os pacotes**
```bash
dotnet restore
```

3. **Execute as migrations (se necessário)**
```bash
dotnet ef database update --project src/WizCo.Infrastructure --startup-project src/WizCo.Api
```

4. **Execute o projeto**
```bash
dotnet run --project src/WizCo.Api
```

5. **Acesse a documentação Swagger**
```
https://localhost:{porta}/swagger
```

## Testes

O projeto inclui testes automatizados

```bash
dotnet test
```

## Padrões e Práticas

- **Repository Pattern**: Abstração do acesso a dados
- **Service Layer**: Lógica de aplicação isolada
- **DTO Pattern**: Separação entre modelos de domínio e transferência de dados
- **Mapper Pattern**: Conversão entre entidades e DTOs
- **Notification Pattern**: Gerenciamento de erros e validações
- **Dependency Injection**: Injeção de dependências nativa do .NET
- **Query Object Pattern**: Filtros reutilizáveis
- **Paginação**: Resultados paginados para melhor performance
