using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Класс для шифрования текста симметричным алгоритмом блочного шифрования AES.
/// </summary>
public static class Cryptography
{
    /// <summary>
    /// Ключ шифрования.
    /// </summary>
    private const string ENCRYPTION_KEY = "VDL1HAfhkQ";

    /// <summary>
    /// В криптографии соль (модификатор) — это строка случайных данных, которая подается на вход хеш-функции вместе с исходными данными.
    /// Используется для удлинения строки пароля, что осложняет восстановление группы исходных паролей за один проход полного перебора
    /// или с помощью предварительно построенных радужных таблиц.
    /// </summary>
    private static readonly byte[] SALT = { 0x29, 0x19, 0x63, 0x5d, 0x2c, 0x4d, 0x17, 0x64, 0x76, 0x64, 0x65 };

    /// <summary>
    /// Зашифровать строку.
    /// </summary>
    /// <param name="originalText">Исходный текст до шифрования.</param>
    /// <returns>Зашифрованная строка.</returns>
    public static byte[] Encrypt(byte[] originalText)
    {
        var encryptedText = ConvertBytes(originalText, true);

        return encryptedText;
    }

    /// <summary>
    /// Расшифровать строку.
    /// </summary>
    /// <param name="encryptedText">Зашифрованная строка.</param>
    /// <returns>Расшифрованная строка.</returns>
    public static byte[] Decrypt(byte[] encryptedText)
    {
        var decryptedText = ConvertBytes(encryptedText, false);

        return decryptedText;
    }

    /// <summary>
    /// Преобразовать байты.
    /// </summary>
    /// <param name="bytes">Массив байтов.</param>
    /// <param name="isEncryption">True - если шифрование, false - дешифрование.</param>
    /// <returns>Массив преобразованных байтов.</returns>
    private static byte[] ConvertBytes(byte[] bytes, bool isEncryption)
    {
        using var encryptor = Aes.Create();

        var pdb = new Rfc2898DeriveBytes(ENCRYPTION_KEY, SALT);
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);

        using var memoryStream = new MemoryStream();

        using var cryptoStream = new CryptoStream(memoryStream,
            isEncryption ? encryptor.CreateEncryptor() : encryptor.CreateDecryptor(), CryptoStreamMode.Write);

        cryptoStream.Write(bytes, 0, bytes.Length);
        cryptoStream.Close();

        return memoryStream.ToArray();
    }
}
