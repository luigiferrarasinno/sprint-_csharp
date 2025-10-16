#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script de teste para Investment API
Testa todos os endpoints da API REST

Para executar:
pip install requests
python test_api.py
"""

import requests
import json
import time
from datetime import datetime
import sys
import io

# Configurar encoding para suportar emojis
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

# Configurações
BASE_URL = "http://localhost:5090/api"
SWAGGER_URL = "http://localhost:5090"  # URL do Swagger UI
HEADERS = {"Content-Type": "application/json"}

def print_separator(title):
    """Imprime um separador visual para organizar os testes"""
    print("\n" + "="*60)
    print(f" {title}")
    print("="*60)

def print_test(test_name):
    """Imprime o nome do teste sendo executado"""
    print(f"\n🧪 {test_name}")
    print("-" * 40)

def print_response(response):
    """Imprime a resposta da API de forma formatada"""
    print(f"Status: {response.status_code}")
    try:
        data = response.json()
        print(f"Response: {json.dumps(data, indent=2, ensure_ascii=False)}")
    except:
        print(f"Response: {response.text}")
    print()

def test_api_health():
    """Verifica se a API está rodando"""
    print_separator("VERIFICAÇÃO DE SAÚDE DA API")
    
    try:
        response = requests.get(f"{BASE_URL.replace('/api', '')}/")
        if response.status_code == 200:
            print("✅ API está rodando!")
            print("📖 Swagger UI disponível em: http://localhost:5000")
        else:
            print("⚠️  API respondeu, mas com status não esperado")
    except requests.exceptions.ConnectionError:
        print("❌ Erro: API não está rodando!")
        print("   Execute: dotnet run")
        return False
    except Exception as e:
        print(f"❌ Erro inesperado: {e}")
        return False
    
    return True

def test_auth_endpoints():
    """Testa os endpoints de autenticação"""
    print_separator("TESTES DE AUTENTICAÇÃO")
    
    # Listar usuários de teste
    print_test("Listar usuários disponíveis para teste")
    response = requests.get(f"{BASE_URL}/Auth/test-users")
    print_response(response)
    
    # Login válido
    print_test("Login com credenciais válidas")
    login_data = {
        "email": "joao@email.com",
        "password": "senha123"
    }
    response = requests.post(f"{BASE_URL}/Auth/login", json=login_data, headers=HEADERS)
    print_response(response)
    
    token = None
    if response.status_code == 200:
        token = response.json().get("token")
        print(f"🔑 Token obtido: {token[:50]}...")
    
    # Login inválido
    print_test("Login com credenciais inválidas")
    invalid_login = {
        "email": "usuario@inexistente.com",
        "password": "senhaerrada"
    }
    response = requests.post(f"{BASE_URL}/Auth/login", json=invalid_login, headers=HEADERS)
    print_response(response)
    
    # Validar token
    if token:
        print_test("Validar token")
        response = requests.post(f"{BASE_URL}/Auth/validate-token", json=token, headers=HEADERS)
        print_response(response)
    
    return token

def test_users_crud():
    """Testa o CRUD completo de usuários"""
    print_separator("TESTES CRUD - USUÁRIOS")
    
    # Listar todos os usuários
    print_test("GET - Listar todos os usuários")
    response = requests.get(f"{BASE_URL}/Users")
    print_response(response)
    
    # Buscar usuário específico
    print_test("GET - Buscar usuário por ID (1)")
    response = requests.get(f"{BASE_URL}/Users/1")
    print_response(response)
    
    # Buscar investimentos de um usuário
    print_test("GET - Investimentos do usuário 1")
    response = requests.get(f"{BASE_URL}/Users/1/investments")
    print_response(response)
    
    # Criar novo usuário
    print_test("POST - Criar novo usuário")
    new_user = {
        "name": "Teste Usuario",
        "email": f"teste_{int(time.time())}@email.com",
        "phone": "(11) 99999-0000"
    }
    response = requests.post(f"{BASE_URL}/Users", json=new_user, headers=HEADERS)
    print_response(response)
    
    created_user_id = None
    if response.status_code == 201:
        created_user_id = response.json().get("id")
        print(f"👤 Usuário criado com ID: {created_user_id}")
    
    # Atualizar usuário
    if created_user_id:
        print_test(f"PUT - Atualizar usuário {created_user_id}")
        updated_user = {
            "id": created_user_id,
            "name": "Teste Usuario Atualizado",
            "email": new_user["email"],
            "phone": "(11) 88888-0000"
        }
        response = requests.put(f"{BASE_URL}/Users/{created_user_id}", json=updated_user, headers=HEADERS)
        print_response(response)
    
    # Tentar criar usuário com email duplicado
    print_test("POST - Tentar criar usuário com email duplicado")
    duplicate_user = {
        "name": "Usuario Duplicado",
        "email": "joao@email.com",  # Email já existe
        "phone": "(11) 77777-0000"
    }
    response = requests.post(f"{BASE_URL}/Users", json=duplicate_user, headers=HEADERS)
    print_response(response)
    
    # Buscar usuário inexistente
    print_test("GET - Buscar usuário inexistente (999)")
    response = requests.get(f"{BASE_URL}/Users/999")
    print_response(response)
    
    return created_user_id

def test_investments_crud(user_id=None):
    """Testa o CRUD completo de investimentos"""
    print_separator("TESTES CRUD - INVESTIMENTOS")
    
    # Listar todos os investimentos
    print_test("GET - Listar todos os investimentos")
    response = requests.get(f"{BASE_URL}/Investments")
    print_response(response)
    
    # Buscar investimento específico
    print_test("GET - Buscar investimento por ID (1)")
    response = requests.get(f"{BASE_URL}/Investments/1")
    print_response(response)
    
    # Buscar por tipo
    print_test("GET - Buscar investimentos por tipo (Ação)")
    response = requests.get(f"{BASE_URL}/Investments/by-type/Ação")
    print_response(response)
    
    # Buscar por usuário
    print_test("GET - Buscar investimentos por usuário (1)")
    response = requests.get(f"{BASE_URL}/Investments/by-user/1")
    print_response(response)
    
    # Resumo de investimentos
    print_test("GET - Resumo de investimentos")
    response = requests.get(f"{BASE_URL}/Investments/summary")
    print_response(response)
    
    # Criar novo investimento
    target_user_id = user_id if user_id else 1
    print_test(f"POST - Criar novo investimento para usuário {target_user_id}")
    new_investment = {
        "name": "Bitcoin ETF",
        "type": "Criptomoeda",
        "amount": 2500.00,
        "expectedReturn": 25.5,
        "description": "Investimento em ETF de Bitcoin",
        "userId": target_user_id
    }
    response = requests.post(f"{BASE_URL}/Investments", json=new_investment, headers=HEADERS)
    print_response(response)
    
    created_investment_id = None
    if response.status_code == 201:
        created_investment_id = response.json().get("id")
        print(f"💰 Investimento criado com ID: {created_investment_id}")
    
    # Atualizar investimento
    if created_investment_id:
        print_test(f"PUT - Atualizar investimento {created_investment_id}")
        updated_investment = {
            "id": created_investment_id,
            "name": "Ethereum ETF",
            "type": "Criptomoeda",
            "amount": 3000.00,
            "expectedReturn": 30.0,
            "description": "Investimento atualizado para ETF de Ethereum",
            "userId": target_user_id
        }
        response = requests.put(f"{BASE_URL}/Investments/{created_investment_id}", json=updated_investment, headers=HEADERS)
        print_response(response)
    
    # Tentar criar investimento para usuário inexistente
    print_test("POST - Tentar criar investimento para usuário inexistente")
    invalid_investment = {
        "name": "Investimento Inválido",
        "type": "Teste",
        "amount": 1000.00,
        "userId": 999  # Usuário não existe
    }
    response = requests.post(f"{BASE_URL}/Investments", json=invalid_investment, headers=HEADERS)
    print_response(response)
    
    # Buscar investimento inexistente
    print_test("GET - Buscar investimento inexistente (999)")
    response = requests.get(f"{BASE_URL}/Investments/999")
    print_response(response)
    
    return created_investment_id

def test_delete_operations(user_id=None, investment_id=None):
    """Testa as operações de delete"""
    print_separator("TESTES DE EXCLUSÃO")
    
    # Deletar investimento
    if investment_id:
        print_test(f"DELETE - Deletar investimento {investment_id}")
        response = requests.delete(f"{BASE_URL}/Investments/{investment_id}")
        print_response(response)
        
        # Verificar se foi deletado
        print_test(f"GET - Verificar se investimento {investment_id} foi deletado")
        response = requests.get(f"{BASE_URL}/Investments/{investment_id}")
        print_response(response)
    
    # Deletar usuário
    if user_id:
        print_test(f"DELETE - Deletar usuário {user_id}")
        response = requests.delete(f"{BASE_URL}/Users/{user_id}")
        print_response(response)
        
        # Verificar se foi deletado
        print_test(f"GET - Verificar se usuário {user_id} foi deletado")
        response = requests.get(f"{BASE_URL}/Users/{user_id}")
        print_response(response)
    
    # Tentar deletar recurso inexistente
    print_test("DELETE - Tentar deletar usuário inexistente (999)")
    response = requests.delete(f"{BASE_URL}/Users/999")
    print_response(response)

def test_data_validation():
    """Testa validações de dados"""
    print_separator("TESTES DE VALIDAÇÃO")
    
    # Criar usuário com dados inválidos
    print_test("POST - Criar usuário com email inválido")
    invalid_user = {
        "name": "",  # Nome vazio
        "email": "email-invalido",  # Email sem formato correto
        "phone": "123"
    }
    response = requests.post(f"{BASE_URL}/Users", json=invalid_user, headers=HEADERS)
    print_response(response)
    
    # Criar investimento com dados inválidos
    print_test("POST - Criar investimento com dados inválidos")
    invalid_investment = {
        "name": "",  # Nome vazio
        "type": "",  # Tipo vazio
        "amount": -100,  # Valor negativo
        "userId": 1
    }
    response = requests.post(f"{BASE_URL}/Investments", json=invalid_investment, headers=HEADERS)
    print_response(response)

def test_stock_quotes():
    """Testa os endpoints de cotações de ações"""
    print_separator("TESTES DE COTAÇÕES DE AÇÕES (ALPHA VANTAGE)")
    
    # Teste com símbolo válido (GET)
    print_test("GET - Consultar cotação via query parameter")
    response = requests.get(f"{BASE_URL}/StockQuotes/quote?symbol=PETR4.SA")
    print_response(response)
    
    # Teste com outro símbolo (POST)
    print_test("POST - Consultar cotação via body JSON")
    stock_data = {"symbol": "VALE3.SA"}
    response = requests.post(f"{BASE_URL}/StockQuotes/quote", json=stock_data, headers=HEADERS)
    print_response(response)
    
    # Teste com símbolo vazio
    print_test("GET - Tentar consultar com símbolo vazio")
    response = requests.get(f"{BASE_URL}/StockQuotes/quote?symbol=")
    print_response(response)
    
    # Teste com símbolo inválido
    print_test("POST - Consultar com símbolo inválido")
    invalid_stock = {"symbol": "XXXXX"}
    response = requests.post(f"{BASE_URL}/StockQuotes/quote", json=invalid_stock, headers=HEADERS)
    print_response(response)
    
    # Teste com mais símbolos
    print_test("GET - Consultar cotação ITUB4.SA")
    response = requests.get(f"{BASE_URL}/StockQuotes/quote?symbol=ITUB4.SA")
    print_response(response)

def main():
    """Função principal que executa todos os testes"""
    print("🚀 INICIANDO TESTES DA INVESTMENT API")
    print(f"⏰ Timestamp: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}")
    print(f"🌐 URL Base: {BASE_URL}")
    
    # Verificar se a API está rodando
    if not test_api_health():
        print("\n❌ API não está disponível. Abortando testes.")
        return
    
    try:
        # Executar todos os testes
        token = test_auth_endpoints()
        created_user_id = test_users_crud()
        created_investment_id = test_investments_crud(created_user_id)
        test_stock_quotes()  # Novo teste de cotações
        test_delete_operations(created_user_id, created_investment_id)
        test_data_validation()
        
        # Resumo final
        print_separator("RESUMO DOS TESTES")
        print("✅ Todos os testes foram executados!")
        print("📊 Verifique os resultados acima para validar o comportamento da API")
        print("📖 Documentação Swagger: http://localhost:5090")
        
    except KeyboardInterrupt:
        print("\n\n⚠️  Testes interrompidos pelo usuário")
    except Exception as e:
        print(f"\n\n❌ Erro durante os testes: {e}")
        print("🔍 Verifique se a API está rodando e acessível")

if __name__ == "__main__":
    main()
