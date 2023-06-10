using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class EncryptHandler
{
    public const string KEY = "Q71C0tyP5ZPDh6TRybNyDD+TD7OvrkPlQ/mhk5TrD00=";
    public const string IV = "bSJkDAF1CAXrLmdGA+/vUg==";

    public static byte[] Encrypt(byte[] plainData)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.ASCII.GetBytes(KEY);
        aesAlg.IV = Encoding.ASCII.GetBytes(IV);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        csEncrypt.Write(plainData, 0, plainData.Length);
        csEncrypt.FlushFinalBlock();
        return msEncrypt.ToArray();
    }

    public static byte[] Decrypt(byte[] encryptedData)
    {
        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.ASCII.GetBytes(KEY);
        aesAlg.IV = Encoding.ASCII.GetBytes(IV);

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(encryptedData);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var msPlain = new MemoryStream();

        csDecrypt.CopyTo(msPlain);
        return msPlain.ToArray();
    }
}
