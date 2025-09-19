# Investment API

Uma API REST completa para gerenciamento de usuários e investimentos, desenvolvida em ASP.NET Core com **arquitetura em camadas** (Repository + Service + Controller), Entity Framework e banco de dados in-memory (simulando H2).

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
│              💾 IN-MEMORY DATABASE                     │
│                (Simulating H2)                         │
└─────────────────────────────────────────────────────────┘
```

### 🎯 **Controllers (API Layer)**
- **AuthController**: Endpoints de autenticação e autorização
- **UsersController**: CRUD completo de usuários
- **InvestmentsController**: CRUD completo de investimentos

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
- **Banco de dados H2** simulado com Entity Framework In-Memory
- **Swagger/OpenAPI** para documentação interativa (sempre disponível)
- **Dados iniciais** (seed) pré-carregados
- **Endpoint de login** simples (sem autenticação real)
- **CORS** habilitado para desenvolvimento
- **Arquitetura em Camadas** com separação de responsabilidades
- **Validações robustas** em múltiplas camadas
- **DTOs** para requests de criação/atualização

## 📋 Pré-requisitos

- .NET 9.0 ou superior
- Qualquer IDE que suporte C# (Visual Studio, VS Code, Rider)

## 🛠️ Instalação e Execução

1. **Clone ou baixe o projeto**
   ```bash
   cd InvestmentAPI
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute a aplicação**
   ```bash
   dotnet run
   ```

4. **Acesse a documentação Swagger**
   - Abra o navegador em: `http://localhost:5090`
   - A documentação interativa estará disponível na página inicial

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
├── Controllers/
│   ├── AuthController.cs          # 🎯 Endpoints de autenticação
│   ├── UsersController.cs         # 🎯 CRUD de usuários
│   └── InvestmentsController.cs   # 🎯 CRUD de investimentos
├── Services/
│   ├── IAuthService.cs           # 🔧 Interface do serviço de auth
│   ├── AuthService.cs            # 🔧 Lógica de autenticação
│   ├── IUserService.cs           # 🔧 Interface do serviço de usuários
│   ├── UserService.cs            # 🔧 Regras de negócio de usuários
│   ├── IInvestmentService.cs     # 🔧 Interface do serviço de investimentos
│   └── InvestmentService.cs      # 🔧 Regras de negócio de investimentos
├── Repositories/
│   ├── IUserRepository.cs        # 📂 Interface do repositório de usuários
│   ├── UserRepository.cs         # 📂 Acesso a dados de usuários
│   ├── IInvestmentRepository.cs  # 📂 Interface do repositório de investimentos
│   └── InvestmentRepository.cs   # 📂 Acesso a dados de investimentos
├── Data/
│   └── InvestmentDbContext.cs    # 🗄️ Contexto do banco de dados
├── Models/
│   ├── User.cs                   # 📋 Modelo de usuário
│   ├── Investment.cs             # 📋 Modelo de investimento
│   ├── LoginModels.cs            # 📋 Modelos de login
│   └── InvestmentRequests.cs     # 📋 DTOs para requests
├── Program.cs                    # ⚙️ Configuração da aplicação e DI
└── test_api.py                   # 🧪 Script de teste automatizado
```

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

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** - ORM
- **In-Memory Database** - Simulando H2
- **Swagger/Swashbuckle** - Documentação da API
- **System.Text.Json** - Serialização JSON
- **Repository Pattern** - Padrão de acesso a dados
- **Service Layer Pattern** - Camada de lógica de negócio
- **Dependency Injection** - Inversão de controle

## 📈 Funcionalidades Especiais

- **Relacionamentos**: Um usuário pode ter vários investimentos
- **Validações em Múltiplas Camadas**: 
  - Data Annotations nos modelos
  - Validações de negócio nos Services
  - Tratamento de exceções nos Controllers
- **Cascading Delete**: Deletar usuário remove seus investimentos
- **Resumo de Investimentos**: Totais por tipo e estatísticas
- **CORS**: Habilitado para desenvolvimento frontend
- **Swagger UI**: Interface sempre disponível para testar a API
- **DTOs**: Request objects para evitar problemas de validação
- **Arquitetura SOLID**: Separação clara de responsabilidades

## 🎯 Benefícios da Arquitetura Implementada

### � **Service Layer**
- ✅ Centraliza a lógica de negócio
- ✅ Facilita testes unitários
- ✅ Reutilização de código entre controllers
- ✅ Validações consistentes

### 📂 **Repository Pattern**
- ✅ Abstrai o acesso a dados
- ✅ Facilita mudança de provider de banco
- ✅ Melhora testabilidade
- ✅ Separação de responsabilidades

### 💉 **Dependency Injection**
- ✅ Baixo acoplamento entre camadas
- ✅ Facilita mocking para testes
- ✅ Facilita manutenção e extensibilidade
- ✅ Inversão de controle

## �🐛 Troubleshooting

### Swagger não acessível
O Swagger agora está sempre disponível em `http://localhost:5090`

### Porta já em uso
Se a porta 5090 estiver em uso, a aplicação tentará outras portas automaticamente.

### Erro "User field is required"
Foi corrigido com a implementação de DTOs específicos para requests.

### Banco de dados
O banco in-memory é recriado a cada execução, mantendo dados consistentes durante a execução.

---

**Desenvolvido com ❤️ usando arquitetura em camadas para gerenciamento de investimentos**

## 🏆 Resultado Final

✅ **API REST completa** com CRUD de usuários e investimentos  
✅ **Arquitetura em camadas** (Repository + Service + Controller)  
✅ **Banco H2 simulado** com Entity Framework In-Memory  
✅ **Swagger UI** sempre disponível  
✅ **Dados de seed** para facilitar testes  
✅ **Endpoint de login** funcional  
✅ **Script de teste Python** abrangente  
✅ **Validações robustas** em múltiplas camadas  
✅ **Documentação completa** no README
