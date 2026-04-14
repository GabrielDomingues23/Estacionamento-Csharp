API desenvolvida em **ASP.NET Web API com C#**, com o objetivo de gerenciar um estacionamento simples, incluindo controle de vagas, entrada e saída de veículos.

---

## Tecnologias utilizadas

* .NET (ASP.NET Web API)
* Entity Framework Core
* SQLite
* C#

---

## Pré-requisitos

Antes de rodar o projeto, você precisa ter instalado:

* [.NET SDK](https://dotnet.microsoft.com/download)
* Git

---

## Como rodar o projeto

### 1. Clonar o repositório

```bash
git clone <LINK_DO_REPOSITORIO>
cd Estacionamento-Csharp/estacionamento
```

---

### 2. Restaurar dependências

```bash
dotnet restore
```

---

### 3. Instalar ferramenta do Entity Framework (caso necessário)

```bash
dotnet tool install --global dotnet-ef
```

Se já tiver instalado, pode pular.

---

### 4. Aplicar migrations (criar banco de dados)

```bash
dotnet ef database update
```

Isso vai gerar o arquivo:

```bash
estacionamento.db
```

---

### 5. Rodar a API

```bash
dotnet run
```

---

## Acessando a API

Após rodar, a API estará disponível em:

```bash
https://localhost:xxxx/api/estadia
```

---

## Estrutura do projeto

```
Controllers/    → Endpoints da API
Models/         → Entidades do sistema
Repositories/   → Acesso ao banco de dados
Data/           → DbContext (Entity Framework)
```

---

## Funcionalidades atuais

* Listar estadias (GET /api/estadia)

---

## Próximas funcionalidades

* Entrada de veículo
* Saída de veículo
* Controle automático de vagas
* Cálculo de valor

---

## 📝 Observações

* O banco utilizado é SQLite (arquivo local)
* Não é necessário instalar servidor de banco de dados

---