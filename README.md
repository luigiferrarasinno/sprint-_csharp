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
4. A API estará disponível em: `http://localhost:5090/api`
5. Swagger UI: `http://localhost:5090`

## 📊 Banco de Dados Oracle

### Tabelas Criadas
- **RM98047.USERS**: Usuários do sistema (com suporte a senha com hash BCrypt)
- **RM98047.INVESTMENTS**: Investimentos dos usuários

### Dados de Teste Disponíveis
```sql
-- Usuários com Autenticação por Senha
1. João Silva (joao@email.com) - Senha: senha123
2. Maria Santos (maria@email.com) - Senha: senha456
3. Pedro Oliveira (pedro@email.com) - Senha: senha789

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

### 🔐 **Autenticação com Senha**

#### ⚠️ Informações de Segurança
A API utiliza **autenticação com senha** usando **BCrypt.Net-Next** para hash seguro das senhas (10 rounds). As senhas **nunca** são armazenadas em texto plano. Cada senha é armazenada como um hash criptográfico na coluna `PASSWORDHASH` da tabela `USERS`.

#### Usuários de Teste
```
Email: joao@email.com    | Senha: senha123
Email: maria@email.com   | Senha: senha456
Email: pedro@email.com   | Senha: senha789
```

#### Setup do Banco de Dados
Para criar o banco de dados com suporte a autenticação, execute o script SQL fornecido:
```bash
# Execute no SQLDeveloper, SQL*Plus ou ferramenta Oracle:
@./InvestmentAPI/oracle_setup.sql
```

Este script:
- ✅ Cria tabela USERS com coluna `PASSWORDHASH` (VARCHAR2(255), NOT NULL)
- ✅ Cria tabela INVESTMENTS com relacionamento para USERS
- ✅ Popula usuários de teste com senhas hasheadas em BCrypt
- ✅ Popula investimentos de teste

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
Realiza login no sistema com validação de senha criptografada.
```json
// Request
{
  "email": "joao@email.com",
  "password": "senha123"
}

// Response (200 OK)
{
  "success": true,
  "message": "Login realizado com sucesso",
  "user": { 
    "id": 1,
    "name": "João Silva",
    "email": "joao@email.com",
    "phone": "(11) 99999-1234",
    "createdAt": "2025-10-16T10:00:00Z"
  },
  "token": "MTpqb2FvQGVtYWlsLmNvbToyMDI1LTEwLTE2VDAx..."
}

// Response (401 Unauthorized) - Senha inválida
{
  "success": false,
  "message": "Email ou senha inválidos"
}

// Response (400 Bad Request) - Dados inválidos
{
  "success": false,
  "message": "Email é obrigatório"
}
```

#### `POST /api/Auth/validate-token`
Valida um token de autenticação.
```json
// Request
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."

// Response (200 OK)
{
  "success": true,
  "message": "Token válido",
  "user": {
    "id": 1,
    "name": "João Silva",
    "email": "joao@email.com"
  }
}

// Response (401 Unauthorized)
{
  "success": false,
  "message": "Token inválido"
}
```

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

### � **Cotações de Ações (Alpha Vantage)**

#### `GET /api/StockQuotes/quote?symbol=PETR4.SA`
Consulta a cotação de uma ação em tempo real via API pública do Alpha Vantage.

**Parâmetros:**
- `symbol` (query, obrigatório): Símbolo da ação (ex: PETR4.SA, VALE3.SA, ITUB4.SA)

**Exemplos de símbolos válidos:**
```
PETR4.SA  - Petrobras
VALE3.SA  - Vale
ITUB4.SA  - Itaú
BBDC3.SA  - Bradesco
USIM5.SA  - Usiminas
```

**Resposta (200 OK):**
```json
{
  "Global Quote": {
    "01. symbol": "PETR4.SA",
    "02. price": "25.45",
    "03. volume": "1000000",
    "04. timestamp": "2025-10-16 16:30:00",
    "05. price change": "+0.45",
    "06. price change percent": "+1.80%",
    "07. bid price": "25.43",
    "08. ask price": "25.47",
    "09. bid size": "500000",
    "10. ask size": "500000",
    "11. trade date": "2025-10-16"
  }
}
```

**Resposta (400 Bad Request) - Símbolo vazio:**
```json
{
  "message": "Símbolo não pode ser vazio",
  "example": "PETR4.SA"
}
```

**Resposta (503 Service Unavailable) - API indisponível:**
```json
{
  "message": "Erro ao consultar API do Alpha Vantage",
  "error": "API call frequency limit reached",
  "note": "Note: Thank you for using Alpha Vantage!"
}
```

#### `POST /api/StockQuotes/quote`
Alternativa via POST para consultar cotação (corpo JSON).

**Request:**
```json
{
  "symbol": "PETR4.SA"
}
```

**Resposta:** Mesma do endpoint GET acima.

---

### �💰 **Investimentos**

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
# ==== AUTENTICAÇÃO ====
# Listar usuários de teste
curl http://localhost:5090/api/auth/test-users

# Fazer login
curl -X POST http://localhost:5090/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"joao@email.com","password":"senha123"}'

# Validar token
curl -X POST http://localhost:5090/api/auth/validate-token \
  -H "Content-Type: application/json" \
  -d '"MTpqb2FvQGVtYWlsLmNvbTo..."'

# ==== USUÁRIOS ====
# Listar usuários
curl http://localhost:5090/api/users

# Buscar usuário específico
curl http://localhost:5090/api/users/1

# Criar novo usuário
curl -X POST http://localhost:5090/api/users \
  -H "Content-Type: application/json" \
  -d '{"name":"Novo Usuario","email":"novo@email.com","phone":"(11)99999-9999"}'

# ==== COTAÇÕES ====
# Consultar cotação de ação (GET)
curl "http://localhost:5090/api/stockquotes/quote?symbol=PETR4.SA"

# Consultar cotação (POST)
curl -X POST http://localhost:5090/api/stockquotes/quote \
  -H "Content-Type: application/json" \
  -d '{"symbol":"PETR4.SA"}'

# ==== INVESTIMENTOS ====
# Listar todos os investimentos
curl http://localhost:5090/api/investments

# Listar investimentos de um usuário
curl http://localhost:5090/api/investments/by-user/1

# Listar investimentos por tipo
curl http://localhost:5090/api/investments/by-type/Ação

# Criar investimento
curl -X POST http://localhost:5090/api/investments \
  -H "Content-Type: application/json" \
  -d '{"name":"Bitcoin ETF","type":"Criptomoeda","amount":2500,"expectedReturn":25.5,"description":"Investimento em ETF de Bitcoin","userId":1}'
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
- `GET /api/Auth/test-users` - Listar usuários disponíveis para teste
- `POST /api/Auth/login` - Login do usuário (com validação de senha)
- `POST /api/Auth/validate-token` - Validar token

### 👤 Usuários
- `GET /api/Users` - Listar todos os usuários
- `GET /api/Users/{id}` - Buscar usuário por ID
- `GET /api/Users/{id}/investments` - Listar investimentos de um usuário
- `POST /api/Users` - Criar novo usuário
- `PUT /api/Users/{id}` - Atualizar usuário
- `DELETE /api/Users/{id}` - Deletar usuário

### 📈 Cotações de Ações
- `GET /api/StockQuotes/quote?symbol=PETR4.SA` - Consultar cotação de ação (Alpha Vantage)
- `POST /api/StockQuotes/quote` - Consultar cotação via POST (Alpha Vantage)

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

### Login com Senha
```json
POST /api/Auth/login
{
  "email": "joao@email.com",
  "password": "senha123"
}
```

### Consultar Cotação de Ação
```bash
# GET
curl "http://localhost:5090/api/stockquotes/quote?symbol=PETR4.SA"

# Resposta
{
  "Global Quote": {
    "01. symbol": "PETR4.SA",
    "02. price": "25.45",
    "03. volume": "1000000",
    ...
  }
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

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** - ORM
- **Oracle Database** - Banco de dados da FIAP
- **Swagger/Swashbuckle** - Documentação da API
- **BCrypt.Net-Next** - Hash seguro de senhas
- **Alpha Vantage API** - Dados de cotações de ações em tempo real
- **System.Text.Json** - Serialização JSON
- **Repository Pattern** - Padrão de acesso a dados
- **Service Layer Pattern** - Camada de lógica de negócio
- **Dependency Injection** - Inversão de controle





