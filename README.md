# Finan

Sistema de gestão financeira desenvolvido utilizando .NET 8 com arquitetura baseada em DDD, testes automatizados e deploy em containers.

---

## Visão Geral

O Finan foi criado para permitir o gerenciamento completo de finanças pessoais através do controle de receitas, despesas, contas correntes e geração de indicadores financeiros.

A aplicação possui separação entre Front-End e Back-End, permitindo evolução independente dos componentes.

---

## Principais Funcionalidades

- Cadastro de Receitas
- Cadastro de Despesas
- Agrupamento Financeiro
- Classificações Financeiras
- Contas Correntes
- Extrato Bancário
- Dashboard Financeiro
- Controle de Usuários
- APIs REST

---

## Arquitetura da Solução

flowchart TD

    WEB["Finan.Web<br/>Razor Pages"]
    API["Finan.Api<br/>ASP.NET Core Web API"]

    CONTRACTS["Contracts<br/>Requests, Responses, Filters"]
    APP["Application<br/>Services, Validators, Use Cases"]
    DOMAIN["Domain<br/>Entities, Business Rules"]
    INFRA["Infrastructure<br/>EF Core, Repositories"]
    DB["PostgreSQL"]

    WEB --> API
    API --> CONTRACTS
    CONTRACTS --> APP
    APP --> DOMAIN
    APP --> INFRA
    INFRA --> DB

---

## Estrutura do Projeto

### App

Responsável pelos pontos de entrada da aplicação.

- Finan.Web
- Finan.Api

### Application

Camada responsável pelos casos de uso.

- Serviços
- Validações
- Regras de aplicação

### Domain

Núcleo do negócio.

- Entidades
- Value Objects
- Regras de domínio

### Infra

Implementações técnicas.

- Entity Framework
- PostgreSQL
- Criptografia
- Extensões
- Persistência

### Contracts

Comunicação entre camadas.

- Requests
- Responses
- Filtros
- Enums
- Mensagens

### Tests

Validação automatizada.

- Unit Tests
- Integration Tests

---

## Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- Razor Pages
- Entity Framework Core
- PostgreSQL
- Docker
- GitHub Actions
- xUnit

---

## Infraestrutura

A aplicação encontra-se publicada em ambiente cloud utilizando Render.

Containers:

- Front-End
- Back-End
- PostgreSQL

---

## Testes

O projeto possui testes unitários e testes de integração para validação das regras de negócio e fluxos principais.

---

## Roadmap

Próximas evoluções planejadas:

- RabbitMQ
- Outbox Pattern
- OpenTelemetry
- Observabilidade
- Cache Distribuído
- Background Services
- Deploy AWS
