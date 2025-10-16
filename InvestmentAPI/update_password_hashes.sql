-- ========================================
-- Investment API - Update Password Hashes
-- Corrigir hashes BCrypt para $2b$ (compatível com C# e Python)
-- ========================================

-- UPDATE HASHES BCrypt
UPDATE RM98047.Users SET PasswordHash = '$2b$10$lzJ6q0..EfW0fJBZuwiqL.vjxrVnEsoZf/KENpc0gblMZJUoATdmm' WHERE Email = 'joao@email.com';
UPDATE RM98047.Users SET PasswordHash = '$2b$10$6mYn8DWvhCZ2rjxjWYicLu98WsF7JqEgoc8K/mSoxo7ukav9t6ODO' WHERE Email = 'maria@email.com';
UPDATE RM98047.Users SET PasswordHash = '$2b$10$jRmA880QlwnKVXcTlhKS.uSX39N1uF12dLKZw/l.vGoUkIzxTpJ.S' WHERE Email = 'pedro@email.com';

COMMIT;

-- VERIFICAÇÃO
SELECT ID, NAME, EMAIL, SUBSTR(PASSWORDHASH, 1, 10) || '...' AS PasswordHash_Preview 
FROM RM98047.Users;
