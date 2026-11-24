using System.Security.Cryptography;
using System.Text;
using SEYNT.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace SEYNT.Infrastructure.Security;

// AES-256-GCM (authenticated encryption).
public sealed class AesEncryptionService : IEncryptionService
{
    private const int NonceSize = 12; // AES-GCM standard nonce
    private const int TagSize = 16;   // AES-GCM standard tag

    private readonly byte[] _key;

    public AesEncryptionService(IOptions<EncryptionSettings> options)
    {
        var configured = options.Value.Key;

        if (string.IsNullOrWhiteSpace(configured))
        {
            // Fallback for development if not yet set in environment/user-secrets
            configured = "default-dev-secret-key-32chars-min!!";
        }

        _key = SHA256.HashData(Encoding.UTF8.GetBytes(configured));
    }

    public string Encrypt(string plaintext)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plaintext);
        var nonce = RandomNumberGenerator.GetBytes(NonceSize);
        var cipherBytes = new byte[plainBytes.Length];
        var tag = new byte[TagSize];

        using var aes = new AesGcm(_key, TagSize);
        aes.Encrypt(nonce, plainBytes, cipherBytes, tag);

        var output = new byte[NonceSize + TagSize + cipherBytes.Length];
        Buffer.BlockCopy(nonce, 0, output, 0, NonceSize);
        Buffer.BlockCopy(tag, 0, output, NonceSize, TagSize);
        Buffer.BlockCopy(cipherBytes, 0, output, NonceSize + TagSize, cipherBytes.Length);

        return Convert.ToBase64String(output);
    }

    public string Decrypt(string cipherText)
    {
        var input = Convert.FromBase64String(cipherText);

        var nonce = new byte[NonceSize];
        var tag = new byte[TagSize];
        var cipherBytes = new byte[input.Length - NonceSize - TagSize];

        Buffer.BlockCopy(input, 0, nonce, 0, NonceSize);
        Buffer.BlockCopy(input, NonceSize, tag, 0, TagSize);
        Buffer.BlockCopy(input, NonceSize + TagSize, cipherBytes, 0, cipherBytes.Length);

        var plainBytes = new byte[cipherBytes.Length];

        using var aes = new AesGcm(_key, TagSize);
        aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

        return Encoding.UTF8.GetString(plainBytes);
    }
}
