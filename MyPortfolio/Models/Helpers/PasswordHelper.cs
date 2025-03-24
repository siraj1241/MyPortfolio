using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MyPortfolio.Models.Helpers
{
    public static class PasswordHelper
    {
        // Generate a hashed password
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        // Verify the entered password with the stored hashed password
        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string[] parts = storedHashedPassword.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);

            byte[] enteredHash = KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return CryptographicOperations.FixedTimeEquals(storedHash, enteredHash);
        }
    }
}