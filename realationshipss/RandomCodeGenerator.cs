using System;
using System.Security.Cryptography;

class RandomCodeGenerator
{
    static void Main()
    {
        // Generate a 512-bit (64-byte) key
        var key = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);

        // Convert the key to a Base64 string
        var base64Key = Convert.ToBase64String(key);

        // Output the key
        Console.WriteLine("Generated Key: " + base64Key);
    }
}