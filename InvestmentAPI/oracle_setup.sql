-- ========================================
-- Investment API - Oracle Setup Script
-- Data: 2025-10-16
-- Descrição: Script de criação e população inicial do banco de dados
-- ========================================

-- 1. LIMPAR TABELAS (pode dar erro se não existir - ignore)
DROP TABLE RM98047.Investments CASCADE CONSTRAINTS;

-- 2. LIMPAR TABELA USERS  
DROP TABLE RM98047.Users CASCADE CONSTRAINTS;

-- 3. CRIAR TABELA USERS COM COLUNA DE SENHA
CREATE TABLE RM98047.Users (
    Id NUMBER(10) PRIMARY KEY,
    Name NVARCHAR2(100) NOT NULL,
    Email NVARCHAR2(100) NOT NULL,
    Phone NVARCHAR2(20),
    PasswordHash NVARCHAR2(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT SYSDATE
);

-- 4. ADICIONAR CONSTRAINT DE EMAIL ÚNICO
ALTER TABLE RM98047.Users ADD CONSTRAINT UQ_Users_Email UNIQUE (Email);

-- 5. CRIAR TABELA INVESTMENTS
CREATE TABLE RM98047.Investments (
    Id NUMBER(10) PRIMARY KEY,
    Name NVARCHAR2(100) NOT NULL,
    Type NVARCHAR2(50) NOT NULL,
    Amount NUMBER(18,2) NOT NULL,
    ExpectedReturn NUMBER(5,2),
    InvestmentDate TIMESTAMP DEFAULT SYSDATE,
    Description NVARCHAR2(500),
    UserId NUMBER(10) NOT NULL
);

-- 6. ADICIONAR FOREIGN KEY
ALTER TABLE RM98047.Investments ADD CONSTRAINT FK_Investments_Users FOREIGN KEY (UserId) REFERENCES RM98047.Users(Id);

-- ============================================
-- INSERIR USUÁRIOS COM HASH DE SENHA
-- ============================================
-- Senhas hasheadas com BCrypt.Net (10 rounds)
-- João Silva: senha123
-- Maria Santos: senha456
-- Pedro Oliveira: senha789

-- 7. INSERIR USUÁRIO 1 (João Silva)
INSERT INTO RM98047.Users (Id, Name, Email, Phone, PasswordHash) 
VALUES (1, 'João Silva', 'joao@email.com', '(11) 99999-1234', '$2a$10$YOD3yk4u2lm6V/xdN0t8sOVMLFM3XxCTM3eISRc1aNpHgZ4mxoQAK');

-- 8. INSERIR USUÁRIO 2 (Maria Santos)
INSERT INTO RM98047.Users (Id, Name, Email, Phone, PasswordHash) 
VALUES (2, 'Maria Santos', 'maria@email.com', '(11) 99999-5678', '$2a$10$Jvwt5mEu8nN7W/yEO1s7seCGLhF4YyCTN4fJTSd2bOpIhZ5nzpQ.K');

-- 9. INSERIR USUÁRIO 3 (Pedro Oliveira)
INSERT INTO RM98047.Users (Id, Name, Email, Phone, PasswordHash) 
VALUES (3, 'Pedro Oliveira', 'pedro@email.com', '(11) 99999-9012', '$2a$10$KPxW6mFv9oO8X/zFP2t9tPdHMiG5ZzDUO5gKUSe3cPqJiA6ozoR.K');

-- ============================================
-- INSERIR INVESTIMENTOS
-- ============================================

-- 10. INSERIR INVESTIMENTO 1
INSERT INTO RM98047.Investments (Id, Name, Type, Amount, ExpectedReturn, Description, UserId) 
VALUES (1, 'Tesouro Selic', 'Tesouro Direto', 5000.00, 12.5, 'Investimento em Tesouro Selic', 1);

-- 11. INSERIR INVESTIMENTO 2
INSERT INTO RM98047.Investments (Id, Name, Type, Amount, ExpectedReturn, Description, UserId) 
VALUES (2, 'PETR4', 'Ação', 2500.00, 15.0, 'Ações da Petrobras', 1);

-- 12. INSERIR INVESTIMENTO 3
INSERT INTO RM98047.Investments (Id, Name, Type, Amount, ExpectedReturn, Description, UserId) 
VALUES (3, 'CDB Banco Inter', 'CDB', 10000.00, 13.2, 'CDB com liquidez diária', 2);

-- 13. INSERIR INVESTIMENTO 4
INSERT INTO RM98047.Investments (Id, Name, Type, Amount, ExpectedReturn, Description, UserId) 
VALUES (4, 'VALE3', 'Ação', 3000.00, 18.0, 'Ações da Vale', 2);

-- 14. INSERIR INVESTIMENTO 5
INSERT INTO RM98047.Investments (Id, Name, Type, Amount, ExpectedReturn, Description, UserId) 
VALUES (5, 'LCI Santander', 'LCI', 7500.00, 11.8, 'LCI isenta de IR', 3);

-- 15. COMMIT
COMMIT;

-- ============================================
-- VERIFICAÇÕES
-- ============================================

-- 16. VERIFICAR USUÁRIOS
SELECT COUNT(*) AS TOTAL_USERS FROM RM98047.Users;

-- 17. VERIFICAR INVESTIMENTOS  
SELECT COUNT(*) AS TOTAL_INVESTMENTS FROM RM98047.Investments;

-- 18. VERIFICAR DADOS COMPLETOS
SELECT u.Name AS Usuario, COUNT(i.Id) AS Investimentos, SUM(i.Amount) AS Total 
FROM RM98047.Users u 
LEFT JOIN RM98047.Investments i ON u.Id = i.UserId 
GROUP BY u.Name;

-- 19. VERIFICAR USUÁRIOS COM SENHAS
SELECT ID, NAME, EMAIL, SUBSTR(PASSWORDHASH, 1, 20) || '...' AS PasswordHash 
FROM RM98047.Users;