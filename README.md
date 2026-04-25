# API de Gerenciamento de Estacionamento

API desenvolvida em ASP.NET Web API com C# para realizar o controle de um estacionamento simples, permitindo a gestão de vagas e o registro de entrada e saída de veículos.

## Execução do Projeto

1.  **Clonar o repositório**

    Execute o comando `git clone https://github.com/GabrielDomingues23/Estacionamento-Csharp.git` e acesse a pasta do projeto com `cd Estacionamento-Csharp/estacionamento`.

2.  **Restaurar dependências**

    Utilize o comando `dotnet restore` para baixar os pacotes necessários.

3.  **Configurar Banco de Dados**

    Certifique-se de ter a ferramenta do Entity Framework instalada (`dotnet tool install --global dotnet-ef`). Em seguida, execute `dotnet ef database update` para aplicar as migrations e gerar o arquivo `estacionamento.db`.

4.  **Iniciar a API**

    Execute o comando `dotnet run`. A aplicação ficará disponível no endereço indicado no terminal (`http://localhost:xxxx`).

## Endpoints Disponíveis

### Vagas

**Listar todas as vagas**

`GET /api/estacionamento/vagas`

**Obter vaga por ID**

`GET /api/estacionamento/vagas/{id}`

**Criar nova vaga**

`POST /api/estacionamento/vagas`

JSON

```
{
  "numeroVaga": "A-01"
}
```

**Excluir vaga**

`DELETE /api/estacionamento/vagas/{id}`

### Movimentação

**Entrada de veículo (Ocupar vaga)**

`POST /api/estacionamento/entrada/{id_vaga}`

JSON

```
{
  "Placa": "ABC-1234",
  "Marca": "Marca",
  "Modelo": "Modelo"
}
```

**Saída de veículo (Liberar vaga)**

`PUT /api/estacionamento/saida/{id_vaga}`
