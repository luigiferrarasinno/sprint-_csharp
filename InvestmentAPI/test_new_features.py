#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script de teste para os novos endpoints da Investment API
Testa a criação de usuário com geração automática de hash BCrypt
Testa o endpoint de stock quotes com GET apenas
"""

import requests
import json
import sys
import io

# Configurar encoding para suportar emojis
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

BASE_URL = "http://localhost:5090/api"
HEADERS = {"Content-Type": "application/json"}

def print_separator(title):
    """Imprime um separador visual"""
    print("\n" + "="*60)
    print(f" {title}")
    print("="*60)

def print_test(test_name):
    """Imprime o nome do teste"""
    print(f"\n🧪 {test_name}")
    print("-" * 40)

def print_response(response):
    """Imprime a resposta da API"""
    print(f"Status: {response.status_code}")
    try:
        data = response.json()
        print(f"Response: {json.dumps(data, indent=2, ensure_ascii=False)}")
    except:
        print(f"Response: {response.text}")

def test_create_user_with_password():
    """Testa criação de usuário com senha (hash gerado automaticamente)"""
    print_separator("TESTE 1: CRIAR USUÁRIO COM SENHA")
    
    print_test("POST - Criar novo usuário com senha")
    user_data = {
        "name": "João Novo",
        "email": "joao.novo@email.com",
        "phone": "(11) 98765-4321",
        "password": "minhaSenha123"  # Senha será hasheada automaticamente
    }
    response = requests.post(f"{BASE_URL}/Users", json=user_data, headers=HEADERS)
    print_response(response)
    
    if response.status_code == 201:
        user = response.json()
        print(f"\n✅ Usuário criado com sucesso!")
        print(f"   ID: {user.get('id')}")
        print(f"   Nome: {user.get('name')}")
        print(f"   Email: {user.get('email')}")
        print(f"   PasswordHash (gerado): {str(user.get('passwordHash'))[:20]}...")
        return user
    else:
        print(f"\n❌ Erro ao criar usuário")
        return None

def test_login_with_new_user(email):
    """Testa login com o novo usuário"""
    print_separator("TESTE 2: LOGIN COM NOVO USUÁRIO")
    
    print_test("POST - Login com novo usuário")
    login_data = {
        "email": email,
        "password": "minhaSenha123"  # Mesma senha usada na criação
    }
    response = requests.post(f"{BASE_URL}/Auth/login", json=login_data, headers=HEADERS)
    print_response(response)
    
    if response.status_code == 200:
        result = response.json()
        print(f"\n✅ Login bem-sucedido!")
        print(f"   Mensagem: {result.get('message')}")
        print(f"   Token: {str(result.get('token'))[:30]}..." if result.get('token') else "   Sem token")
    else:
        print(f"\n❌ Erro no login")

def test_stock_quotes_get_only():
    """Testa endpoint de cotações (apenas GET)"""
    print_separator("TESTE 3: STOCK QUOTES (APENAS GET)")
    
    symbols = ["PETR4.SA", "VALE3.SA", "ITUB4.SA"]
    
    for symbol in symbols:
        print_test(f"GET - Cotação de {symbol}")
        response = requests.get(f"{BASE_URL}/StockQuotes/quote?symbol={symbol}", headers=HEADERS)
        print_response(response)
        
        if response.status_code == 200:
            data = response.json()
            global_quote = data.get("Global Quote", {})
            print(f"\n   Símbolo: {global_quote.get('01. symbol', 'N/A')}")
            print(f"   Preço: {global_quote.get('05. price', 'N/A')}")
            print(f"   Volume: {global_quote.get('06. volume', 'N/A')}")

def test_no_post_stock_quotes():
    """Verifica que POST não está disponível"""
    print_separator("TESTE 4: VERIFICAR QUE POST NÃO EXISTE")
    
    print_test("POST - Tentar POST em stock quotes (deve falhar)")
    quote_data = {"symbol": "PETR4.SA"}
    response = requests.post(f"{BASE_URL}/StockQuotes/quote", json=quote_data, headers=HEADERS)
    print(f"Status: {response.status_code}")
    
    if response.status_code == 405 or response.status_code == 404:
        print(f"✅ Correto! POST não está disponível (Status {response.status_code})")
    else:
        print(f"Response: {response.text}")

if __name__ == "__main__":
    print("\n🚀 INICIANDO TESTES DOS NOVOS ENDPOINTS")
    print(f"📍 Base URL: {BASE_URL}")
    
    # Teste 1: Criar usuário com senha
    new_user = test_create_user_with_password()
    
    # Teste 2: Login com novo usuário
    if new_user:
        test_login_with_new_user(new_user.get('email'))
    
    # Teste 3: Stock quotes (GET apenas)
    test_stock_quotes_get_only()
    
    # Teste 4: Verificar que POST não existe
    test_no_post_stock_quotes()
    
    print("\n" + "="*60)
    print("✅ TODOS OS TESTES CONCLUÍDOS")
    print("="*60)
