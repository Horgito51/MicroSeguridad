using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Seguridad.Business.Common
{
    public static class PasswordHasher
    {
        private const int SaltSizeBytes = 16;
        private const int HashSizeBytes = 32;
        private const int Iterations = 100_000;

        public static (string hashBase64, string saltBase64) HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSizeBytes);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSizeBytes);
            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }

        public static bool Verify(string password, string storedHashBase64, string storedSaltBase64)
        {
            if (string.IsNullOrWhiteSpace(storedHashBase64))
                return false;

            if (string.Equals(password, storedHashBase64, StringComparison.Ordinal))
                return true;

            if (string.IsNullOrWhiteSpace(storedSaltBase64))
                return TryShaLegacyVerify(password, storedHashBase64, string.Empty);

            if (TryDecodeBytes(storedSaltBase64, out var saltBytes) && TryDecodeBytes(storedHashBase64, out var storedHashBytes))
            {
                var computed = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, HashAlgorithmName.SHA256, storedHashBytes.Length);
                if (CryptographicOperations.FixedTimeEquals(computed, storedHashBytes))
                    return true;
            }

            return TryShaLegacyVerify(password, storedHashBase64, storedSaltBase64);
        }

        private static bool TryShaLegacyVerify(string password, string storedHash, string storedSalt)
        {
            var looksHex = IsHex(storedHash);
            var isSha512Hex = looksHex && storedHash.Length == 128;
            var salt = storedSalt ?? string.Empty;
            var candidates = new[] { password, password + salt, salt + password };

            foreach (var candidate in candidates)
            {
                var bytes = isSha512Hex
                    ? SHA512.HashData(Encoding.UTF8.GetBytes(candidate))
                    : SHA256.HashData(Encoding.UTF8.GetBytes(candidate));

                if (HashMatches(bytes, storedHash))
                    return true;
            }

            return false;
        }

        private static bool HashMatches(byte[] hashBytes, string storedHash)
        {
            if (IsHex(storedHash))
                return string.Equals(Convert.ToHexString(hashBytes), storedHash, StringComparison.OrdinalIgnoreCase);

            var b64 = Convert.ToBase64String(hashBytes);
            return string.Equals(b64, storedHash, StringComparison.Ordinal)
                || string.Equals(b64.TrimEnd('='), storedHash.TrimEnd('='), StringComparison.Ordinal);
        }

        private static bool TryDecodeBytes(string input, out byte[] bytes)
        {
            bytes = Array.Empty<byte>();
            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (TryBase64Decode(input, out bytes))
                return true;

            if (!IsHex(input))
                return false;

            bytes = Convert.FromHexString(input);
            return true;
        }

        private static bool TryBase64Decode(string input, out byte[] bytes)
        {
            bytes = Array.Empty<byte>();
            var trimmed = input.Trim();
            var mod = trimmed.Length % 4;
            if (mod != 0)
                trimmed += new string('=', 4 - mod);

            try
            {
                bytes = Convert.FromBase64String(trimmed);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsHex(string input)
            => !string.IsNullOrWhiteSpace(input) && input.Length % 2 == 0 && input.All(Uri.IsHexDigit);
    }
}
