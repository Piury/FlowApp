
using System.Security.Cryptography;
using System.Text;

namespace FlowDb.Services.Token;
public class Hasher
{
    public string HashPassword(string password)
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);

        const int iterations = 10000;
        var hashAlgorithm = HashAlgorithmName.SHA512;

        var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm);
        byte[] keyBytes = pbkdf2.GetBytes(32);

        // Combine salt and keyBytes into a single string (allocate 48 bytes)
        byte[] finalHashBytes = new byte[salt.Length + keyBytes.Length];

        Buffer.BlockCopy(salt, 0, finalHashBytes, 0, salt.Length);
        Buffer.BlockCopy(keyBytes, 0, finalHashBytes, salt.Length, keyBytes.Length);



        string savedPasswordHash = Convert.ToBase64String(finalHashBytes);
        return savedPasswordHash;
    }
    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Extract salt and key bytes from storedHash
        byte[] storedHashBytes = Convert.FromBase64String(storedHash);
        if (storedHashBytes.Length < 32)
        {
            // Invalid hash format
            return false;
        }

        // Extract salt from the beginning of the storedHashBytes
        byte[] salt = new byte[16];
        Array.Copy(storedHashBytes, 0, salt, 0, 16);

        // Extract key bytes from the rest of the storedHashBytes
        byte[] keyBytes = new byte[storedHashBytes.Length - 16];
        Array.Copy(storedHashBytes, 16, keyBytes, 0, keyBytes.Length);

        // Verify salt length
        if (salt.Length != 16)
        {
            // Invalid salt length
            return false;
        }

        // Verify key bytes length
        if (keyBytes.Length != 32)
        {
            // Invalid key bytes length
            return false;
        }

        const int iterations = 10000;
        var hashAlgorithm = HashAlgorithmName.SHA512;

        var pbkdf2 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(enteredPassword), salt, iterations, hashAlgorithm);
        byte[] enteredHash = pbkdf2.GetBytes(32);

        // Verify generated hash length
        if (enteredHash.Length != 32)
        {
            // Invalid generated hash length
            return false;
        }

        // Compare generated hash with extracted key bytes
        return ConstantTimeEquals(enteredHash, keyBytes, keyBytes.Length);
    }
    private static bool ConstantTimeEquals(byte[] a, byte[] b, int length)
    {
        if (a.Length != b.Length || length != a.Length)
        {
            return false;
        }

        var result = 0;
        for (int i = 0; i < length; i++)
        {
            result |= a[i] ^ b[i];
        }
        return result == 0;
    }
}

