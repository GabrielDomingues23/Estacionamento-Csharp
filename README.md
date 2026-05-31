# Estacionamento C# + React

API desenvolvida em **ASP.NET Web API com C#** para gerenciar um estacionamento simples com controle de vagas, entrada e saída de veículos.

---

## Integrantes da equipe

* Gabriel Domingues
* Thiago Palestino
* Jhonatan Mendes

---

## Descrição do sistema

Este projeto combina uma Web API ASP.NET com Entity Framework Core e um frontend React criado com Vite. O backend expõe endpoints para listar vagas, criar vagas, registrar entrada e saída de carros, e gerencia o estado de cada vaga.

---

## Tecnologias utilizadas

* .NET 10
* ASP.NET Web API
* Entity Framework Core
* SQLite
* JWT Authentication
* React
* Vite
* Fetch API

---

## Autenticação JWT

A API agora possui autenticação JWT.

* Endpoint de login: `POST /api/estacionamento/login`
* Credenciais de exemplo: `admin` / `password`
* O frontend envia o token no cabeçalho `Authorization: Bearer {token}`
* Endpoint protegido: `/api/estacionamento/vagas` e demais endpoints do controlador

A autenticação gera um token JWT assinado com chave simétrica e valida o token em cada requisição protegida.

---

## Endpoints principais

* `POST /api/estacionamento/login`
* `GET /api/estacionamento/vagas`
* `POST /api/estacionamento/vagas`
* `GET /api/estacionamento/vagas/{id}`
* `POST /api/estacionamento/entrada/{vagaId}`
* `PUT /api/estacionamento/saida/{vagaId}`
* `DELETE /api/estacionamento/vagas/{id}`

---

## Como rodar o backend

### 1. Clonar o repositório

```bash
git clone <LINK_DO_REPOSITORIO>
cd Estacionamento-Csharp/estacionamento
```

### 2. Restaurar dependências

```bash
dotnet restore
```

### 3. Instalar o Entity Framework CLI (se necessário)

```bash
dotnet tool install --global dotnet-ef
```

### 4. Aplicar migrations

```bash
dotnet ef database update
```

### 5. Rodar a API

```bash
dotnet run
```

A API será exposta em `http://localhost:5132` e `https://localhost:7152`.

---

## Como rodar o frontend

```bash
cd ../frontend
npm install
npm run dev
```

O frontend React será servido pelo Vite em `http://localhost:5173`.

---

## Comunicação HTTP utilizada no frontend

O frontend consome a API usando `fetch` e faz requisições HTTP:

* `GET` para listar vagas
* `POST` para criar vagas e registrar entrada
* `PUT` para registrar saída
* `DELETE` para remover vagas

---

## Observações

* O frontend React demonstra uso de componentes, `useState`, `useEffect`, renderização dinâmica, eventos e consumo de API.
* O backend mantém entidades, regras de negócio, persistência e formatos de endpoint existentes.
* A comunicação entre frontend e API é feita por HTTP com suporte a CORS.

