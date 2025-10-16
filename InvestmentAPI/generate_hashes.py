#!/usr/bin/env python3
import bcrypt

# Gerar novos hashes para as senhas de teste
senhas = {
    "joao@email.com": "senha123",
    "maria@email.com": "senha456", 
    "pedro@email.com": "senha789"
}

print("Novos hashes BCrypt ($2b$ - compatível com C# e Python):\n")

for email, senha in senhas.items():
    # Gerar com $2b$ (mais recente e compatível)
    hash_bytes = bcrypt.hashpw(senha.encode('utf-8'), bcrypt.gensalt(10))
    hash_str = hash_bytes.decode('utf-8')
    print(f"Email: {email}")
    print(f"Senha: {senha}")
    print(f"Hash: {hash_str}")
    
    # Verificar se o hash funciona
    is_valid = bcrypt.checkpw(senha.encode('utf-8'), hash_bytes)
    print(f"Verificação: {'✓ Válido' if is_valid else '✗ Inválido'}\n")

# Gerar script SQL
print("\n" + "="*70)
print("SQL UPDATE para atualizar os hashes no banco de dados:")
print("="*70 + "\n")

hashes = {}
for email, senha in senhas.items():
    hash_bytes = bcrypt.hashpw(senha.encode('utf-8'), bcrypt.gensalt(10))
    hash_str = hash_bytes.decode('utf-8')
    hashes[email] = hash_str
    print(f"UPDATE RM98047.Users SET PasswordHash = '{hash_str}' WHERE Email = '{email}';")

print("\nCOMMIT;")
