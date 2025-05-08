## 📄 README.md


# 📚 Guia para Executar a API ASP.NET Core (.NET 8)

Este documento descreve o passo a passo para configurar, preparar e executar uma API desenvolvida em C# com .NET 8 utilizando Entity Framework Core e SQL Server.

---

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)
- SQL Server (local ou remoto)
- Visual Studio 2022+ ou VS Code
- EF CLI Tool (caso ainda não tenha):

```bash
dotnet tool install --global dotnet-ef
````
Para atualizar:
```bash
dotnet tool update --global dotnet-ef
```

---

## 📁 Clonando o Projeto

Clone o repositório ou faça download dos arquivos.

```bash
git clone https://github.com/mouraeduardo/product.hub
```

---

## ⚙️ Configurando a Connection String

Antes de executar a aplicação, é necessário configurar a **string de conexão** com o banco de dados.

1. Acesse o arquivo:  
   `appsettings.json`

2. Localize a seção `"ConnectionStrings"`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MinhaAPI;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Altere para as informações do seu ambiente:
- `Server`: nome ou IP do seu servidor SQL
- `Database`: nome do banco que será criado
- `User Id` e `Password` caso utilize autenticação SQL Server Authentication.

**Exemplo com autenticação SQL Server:**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MinhaAPI;User Id=sa;Password=SuaSenhaAqui;TrustServerCertificate=True;"
}
```

---

## 📦 Instalando os Pacotes Necessários
Acesse a camada 'Infrastructure'
````bash
cd Infrastructure
````
Caso o projeto não tenha os pacotes instalados, execute:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 📑 Criando e Aplicando Migrations
Ainda na camada 'Infrastructure'

### 📝 Criar a primeira migration

```bash
dotnet ef migrations add InitialCreate
```

### 📤 Aplicar as migrations no banco de dados

```bash
dotnet ef database update
```

Pronto — o banco será criado com as tabelas definidas nas entidades.

---

## ▶️ Executando a API
Retorne para a pasta da solução
```bash
cd ..
```
Agora, acesse a pasta Camada de API
```bash
cd Api
```
Para rodar o projeto:

```bash
dotnet run --project NomeDoSeuProjeto.csproj
```

A aplicação será iniciada e exibirá a URL no terminal, algo como:

```
Now listening on: https://localhost:5001
```

Acesse essa URL no navegador ou via Postman para testar os endpoints.

---

## 📌 Dicas Extras

✅ Verifique se o projeto `Startup.cs` ou `Program.cs` está configurado para usar o EF Core com a connection string correta:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

---

## 📜 Considerações Finais

✔️ Com esses passos, sua API ASP.NET Core estará pronta para rodar localmente, conectada ao banco de dados, com migrations aplicadas e endpoints expostos para testes.

---

📌 Qualquer dúvida ou melhoria, fique à vontade para contribuir ou abrir uma issue.
```

---