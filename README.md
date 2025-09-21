# Investment API

Uma API REST completa para gerenciamento de usuários e investimentos, desenvolvida em ASP.NET Core com **arquitetura em camadas** (Repository + Service + Controller), Entity Framework e integração com **Oracle Database**.

## 👥 Integrantes

• **Davi Passanha de Sousa Guerra** - RM551605  
• **Cauã Gonçalves de Jesus** - RM97648  
• **Luan Silveira Macea** - RM98290  
• **Rui Amorim Siqueira** - RM98436  
• **Luigi Ferrara Sinno** - RM98047

## 🏗️ Arquitetura Implementada

```
┌─────────────────────────────────────────────────────────┐
│                    🎯 CONTROLLERS                       │
│  AuthController | UsersController | InvestmentsController│
│                    (API Endpoints)                      │
└─────────────────────┬───────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────┐
│                   🔧 SERVICES                          │
│   AuthService | UserService | InvestmentService        │
│              (Business Logic)                          │
└─────────────────────┬───────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────┐
│                 📂 REPOSITORIES                        │
│      UserRepository | InvestmentRepository             │
│              (Data Access Layer)                       │
└─────────────────────┬───────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────┐
│               🗄️ ENTITY FRAMEWORK                      │
│             InvestmentDbContext                         │
│            (Database Abstraction)                      │
└─────────────────────┬───────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────┐
│              � ORACLE DATABASE                        │
│            oracle.fiap.com.br:1521/ORCL               │
│                 Schema: RM98047                        │
└─────────────────────────────────────────────────────────┘
```

### 🎯 **Controllers (API Layer)**
- **AuthController**: Endpoints de autenticação e autorização
- **UsersController**: CRUD completo de usuários
- **InvestmentsController**: CRUD completo de investimentos

### 📊 Diagramas de Arquitetura

#### 🏗️ Diagrama de Arquitetura em Camadas

<img width="1208" height="724" alt="image" src="https://github.com/user-attachments/assets/0cacc62e-70f8-486c-81b9-aa2e3553ee61" />


#### 🔄 Diagrama de Fluxo de Dados

<img width="1146" height="601" alt="image" src="https://github.com/user-attachments/assets/de77cc6f-f6de-4c22-9a83-89c6aa71b73e" />


## 🚀 Como Executar

### Pré-requisitos
- .NET 9.0 SDK
- Oracle Database (configurado em oracle.fiap.com.br)
- Visual Studio 2022 ou VS Code

### Passos
1. Clone o repositório
2. Navegue para a pasta `InvestmentAPI`
3. Execute o comando:
```bash
dotnet run
```
4. A API estará disponível em: `http://localhost:5090`
5. Swagger UI: `http://localhost:5090`

## 📊 Banco de Dados Oracle

### Tabelas Criadas
- **RM98047.USERS**: Usuários do sistema
- **RM98047.INVESTMENTS**: Investimentos dos usuários

### Dados de Teste Disponíveis
```sql
-- Usuários
1. João Silva (joao@email.com)
2. Maria Santos (maria@email.com)  
3. Pedro Oliveira (pedro@email.com)

-- Investimentos
1. Tesouro Selic (R$ 5.000) - João
2. PETR4 (R$ 2.500) - João
3. CDB Banco Inter (R$ 10.000) - Maria
4. VALE3 (R$ 3.000) - Maria
5. LCI Santander (R$ 7.500) - Pedro
```

### 🔧 **Services (Business Logic Layer)**  
- **AuthService**: Lógica de login e validação de tokens
- **UserService**: Regras de negócio para usuários (validações, email único, etc.)
- **InvestmentService**: Regras de negócio para investimentos (validações, relacionamentos)

### 📂 **Repositories (Data Access Layer)**
- **UserRepository**: Operações de banco para usuários
- **InvestmentRepository**: Operações de banco para investimentos
- **Pattern Repository**: Abstração do acesso a dados

### 💉 **Dependency Injection**
- Todas as dependências configuradas no `Program.cs`
- Inversão de controle entre camadas
- Facilita testes unitários e manutenção

## 🚀 Características

- **CRUD completo** para Usuários e Investimentos
- **Relacionamento 1:N** entre Usuários e Investimentos
- **Banco de dados Oracle** da FIAP
- **Swagger/OpenAPI** para documentação interativa (sempre disponível)
- **Dados iniciais** via script SQL
- **Endpoint de login** simples (sem autenticação real)
- **CORS** habilitado para desenvolvimento
- **Arquitetura em Camadas** com separação de responsabilidades
- **Validações robustas** em múltiplas camadas
- **DTOs** para requests de criação/atualização

## 📋 Pré-requisitos

- .NET 9.0 ou superior
- Qualquer IDE que suporte C# (Visual Studio, VS Code, Rider)

## 🛠️ Como Rodar o Projeto

### 1. **Clone o repositório**
```bash
git clone https://github.com/luigiferrarasinno/sprint-_csharp.git
cd sprint-_csharp
```


### 2. **Navegue até o diretório do projeto**
```bash
cd InvestmentAPI
```

### 3. **Restaure as dependências**
```bash
dotnet restore
```

### 4. **Execute a aplicação**
```bash
dotnet run
```

### 5. **Acesse a documentação Swagger**
- Abra o navegador em: `http://localhost:5090`
- A documentação interativa estará disponível na página inicial

### 6. **Execute os testes (opcional)**
```bash
python test_api.py
```

## 📚 Documentação dos Endpoints

### 🔐 **Autenticação**

#### `GET /api/Auth/test-users`
Lista usuários disponíveis para teste de login.
```json
{
  "message": "Usuários disponíveis para teste de login",
  "users": [
    {"id": 1, "name": "João Silva", "email": "joao@email.com"},
    {"id": 2, "name": "Maria Santos", "email": "maria@email.com"},
    {"id": 3, "name": "Pedro Oliveira", "email": "pedro@email.com"}
  ]
}
```

#### `POST /api/Auth/login`
Realiza login no sistema.
```json
// Request
{
  "email": "joao@email.com",
  "password": "qualquer_senha"
}

// Response (200)
{
  "success": true,
  "message": "Login realizado com sucesso",
  "user": { /* dados do usuário */ },
  "token": "MTpqb2FvQGVtYWlsLmNvbTo..."
}
```

#### `POST /api/Auth/validate-token`
Valida um token de autenticação.

---

### 👥 **Usuários**

#### `GET /api/Users`
Lista todos os usuários com seus investimentos.
```json
[
  {
    "id": 1,
    "name": "João Silva",
    "email": "joao@email.com",
    "phone": "(11) 99999-1234",
    "createdAt": "2025-09-19T11:11:22",
    "investments": [
      {
        "id": 1,
        "name": "Tesouro Selic",
        "type": "Tesouro Direto",
        "amount": 5000,
        "expectedReturn": 12.5,
        "description": "Investimento em Tesouro Selic"
      }
    ]
  }
]
```

#### `GET /api/Users/{id}`
Busca usuário específico por ID.

#### `GET /api/Users/{id}/investments`
Lista investimentos de um usuário específico.

#### `POST /api/Users`
Cria novo usuário.
```json
// Request
{
  "name": "Novo Usuario",
  "email": "novo@email.com",
  "phone": "(11) 88888-8888"
}

// Response (201)
{
  "id": 4,
  "name": "Novo Usuario",
  "email": "novo@email.com",
  "phone": "(11) 88888-8888",
  "createdAt": "2025-09-19T14:37:31Z",
  "investments": []
}
```

#### `PUT /api/Users/{id}`
Atualiza usuário existente.

#### `DELETE /api/Users/{id}`
Remove usuário do sistema.

---

### 💰 **Investimentos**

#### `GET /api/Investments`
Lista todos os investimentos com dados dos usuários.

#### `GET /api/Investments/{id}`
Busca investimento específico por ID.

#### `GET /api/Investments/by-type/{type}`
Lista investimentos por tipo (Ação, CDB, LCI, etc.).

#### `GET /api/Investments/by-user/{userId}`
Lista investimentos de um usuário específico.

#### `GET /api/Investments/summary`
Retorna resumo estatístico dos investimentos.
```json
{
  "totalInvestments": 5,
  "totalAmount": 28000,
  "byType": [
    {
      "type": "Ação",
      "count": 2,
      "totalAmount": 5500,
      "averageReturn": 16.5
    },
    {
      "type": "CDB",
      "count": 1,
      "totalAmount": 10000,
      "averageReturn": 13.2
    }
  ]
}
```

#### `POST /api/Investments`
Cria novo investimento.
```json
// Request
{
  "name": "Bitcoin ETF",
  "type": "Criptomoeda",
  "amount": 2500.00,
  "expectedReturn": 25.5,
  "description": "Investimento em ETF de Bitcoin",
  "userId": 1
}

// Response (201)
{
  "id": 6,
  "name": "Bitcoin ETF",
  "type": "Criptomoeda",
  "amount": 2500.0,
  "expectedReturn": 25.5,
  "investmentDate": "2025-09-19T14:37:32Z",
  "description": "Investimento em ETF de Bitcoin",
  "userId": 1,
  "user": { /* dados do usuário */ }
}
```

#### `PUT /api/Investments/{id}`
Atualiza investimento existente.

#### `DELETE /api/Investments/{id}`
Remove investimento do sistema.

## 🧪 Como Testar os Endpoints

### Opção 1: Swagger UI (Recomendado)
1. Execute a API: `dotnet run`
2. Acesse: `http://localhost:5090`
3. Use a interface interativa para testar todos os endpoints

### Opção 2: Teste Automatizado
```bash
python test_api.py
```

### Opção 3: cURL
```bash
# Listar usuários
curl http://localhost:5090/api/users

# Criar usuário
curl -X POST http://localhost:5090/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"Teste","email":"teste@email.com","phone":"(11)99999-9999"}'

# Login
curl -X POST http://localhost:5090/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"joao@email.com","password":"123"}'

# Listar investimentos
curl http://localhost:5090/api/investments

# Criar investimento
curl -X POST http://localhost:5090/api/investments \
  -H "Content-Type: application/json" \
  -d '{"name":"Novo Investimento","type":"Ação","amount":1000,"userId":1}'
```

### Opção 4: Postman
Importe a collection usando a URL do Swagger: `http://localhost:5090/swagger/v1/swagger.json`

## 🧪 Teste Automatizado

Execute o script Python `test_api.py` para testar todos os endpoints automaticamente:

```bash
python test_api.py
```

O script testará:
- ✅ Login e autenticação
- ✅ CRUD de usuários (com validações)
- ✅ CRUD de investimentos (com validações)
- ✅ Endpoints especiais (resumo, busca por tipo, etc.)
- ✅ Validações de negócio (email único, usuário existente, etc.)
- ✅ Tratamento de erros e casos extremos

## 🗂️ Estrutura do Projeto

```
InvestmentAPI/
├── 📁 Controllers/
│   ├── AuthController.cs          # 🎯 Endpoints de autenticação
│   ├── InvestmentsController.cs   # 🎯 CRUD de investimentos
│   └── UsersController.cs         # 🎯 CRUD de usuários
├── 📁 Services/
│   ├── IAuthService.cs           # 🔧 Interface do serviço de auth
│   ├── AuthService.cs            # 🔧 Lógica de autenticação
│   ├── IUserService.cs           # 🔧 Interface do serviço de usuários
│   ├── UserService.cs            # 🔧 Regras de negócio de usuários
│   ├── IInvestmentService.cs     # 🔧 Interface do serviço de investimentos
│   └── InvestmentService.cs      # 🔧 Regras de negócio de investimentos
├── 📁 Repositories/
│   ├── IUserRepository.cs        # 📂 Interface do repositório de usuários
│   ├── UserRepository.cs         # 📂 Acesso a dados de usuários
│   ├── IInvestmentRepository.cs  # 📂 Interface do repositório de investimentos
│   └── InvestmentRepository.cs   # 📂 Acesso a dados de investimentos
├── 📁 Data/
│   └── InvestmentDbContext.cs    # 🗄️ Contexto do banco de dados
├── 📁 Models/
│   ├── User.cs                   # 📊 Modelo de dados do usuário
│   ├── Investment.cs             # 📊 Modelo de dados do investimento
│   ├── InvestmentRequests.cs     # 📝 DTOs para requests de investimentos
│   └── LoginModels.cs            # 📝 DTOs para autenticação
├── 📁 Properties/
│   └── launchSettings.json       # ⚙️ Configurações de execução
├── 📁 bin/                       # 🔨 Arquivos compilados
├── 📁 obj/                       # 🔨 Arquivos temporários de build
├── appsettings.json              # ⚙️ Configurações da aplicação
├── appsettings.Development.json  # ⚙️ Configurações de desenvolvimento
├── InvestmentAPI.csproj          # 📦 Arquivo de projeto .NET
├── InvestmentAPI.http            # 🧪 Requisições HTTP para teste
├── oracle_setup.sql              # 🗄️ Script de criação das tabelas Oracle
├── test_api.py                   # 🧪 Testes automatizados em Python
└── Program.cs                    # ⚙️ Ponto de entrada da aplicação
```

---



## 📊 Dados Iniciais (Seed Data)

A API vem com dados pré-carregados para facilitar os testes:

### 👥 Usuários
- **João Silva** (joao@email.com) - ID: 1
- **Maria Santos** (maria@email.com) - ID: 2  
- **Pedro Oliveira** (pedro@email.com) - ID: 3

### 💰 Investimentos
- Tesouro Selic, PETR4 (João)
- CDB Banco Inter, VALE3 (Maria)
- LCI Santander (Pedro)

## 🔗 Endpoints da API

### 🔐 Autenticação
- `POST /api/Auth/login` - Login do usuário
- `POST /api/Auth/validate-token` - Validar token
- `GET /api/Auth/test-users` - Listar usuários disponíveis para teste

### 👤 Usuários
- `GET /api/Users` - Listar todos os usuários
- `GET /api/Users/{id}` - Buscar usuário por ID
- `GET /api/Users/{id}/investments` - Listar investimentos de um usuário
- `POST /api/Users` - Criar novo usuário
- `PUT /api/Users/{id}` - Atualizar usuário
- `DELETE /api/Users/{id}` - Deletar usuário

### 💰 Investimentos
- `GET /api/Investments` - Listar todos os investimentos
- `GET /api/Investments/{id}` - Buscar investimento por ID
- `GET /api/Investments/by-type/{type}` - Buscar por tipo
- `GET /api/Investments/by-user/{userId}` - Buscar por usuário
- `GET /api/Investments/summary` - Resumo dos investimentos
- `POST /api/Investments` - Criar novo investimento
- `PUT /api/Investments/{id}` - Atualizar investimento
- `DELETE /api/Investments/{id}` - Deletar investimento

## 📝 Exemplos de Uso

### Login
```json
POST /api/Auth/login
{
  "email": "joao@email.com",
  "password": "qualquer_senha"
}
```

### Criar Usuário
```json
POST /api/Users
{
  "name": "Novo Usuario",
  "email": "novo@email.com",
  "phone": "(11) 99999-0000"
}
```

### Criar Investimento
```json
POST /api/Investments
{
  "name": "ITUB4",
  "type": "Ação",
  "amount": 1500.00,
  "expectedReturn": 16.5,
  "description": "Ações do Itaú",
  "userId": 1
}
```

## 🔧 Tecnologias Utilizadas

- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core** - ORM
- **Oracle Database** - Banco de dados da FIAP
- **Swagger/Swashbuckle** - Documentação da API
- **System.Text.Json** - Serialização JSON
- **Repository Pattern** - Padrão de acesso a dados
- **Service Layer Pattern** - Camada de lógica de negócio
- **Dependency Injection** - Inversão de controle




