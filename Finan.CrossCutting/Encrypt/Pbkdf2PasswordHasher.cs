using System.Security.Cryptography;

namespace Finan.CrossCutting.Encrypt
{
    // Implementação PBKDF2 simples (para demonstração). Em produção, considere libs consolidadas.
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100_000;

        public string Hash(string password)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            var result = new byte[1 + SaltSize + KeySize];
            result[0] = 0; // version
            Buffer.BlockCopy(salt, 0, result, 1, SaltSize);
            Buffer.BlockCopy(key, 0, result, 1 + SaltSize, KeySize);
            return Convert.ToBase64String(result);
        }

        public bool Verify(string password, string hashed)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(hashed)) return false;

            byte[] decoded;
            try
            {
                decoded = Convert.FromBase64String(hashed);
            }
            catch
            {
                return false;
            }

            if (decoded.Length != 1 + SaltSize + KeySize) return false;
            var salt = new byte[SaltSize];
            Buffer.BlockCopy(decoded, 1, salt, 0, SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++)
            {
                if (decoded[1 + SaltSize + i] != key[i]) return false;
            }

            return true;
        }
    }
}
