using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptedStringExample
{

  public abstract class SymmetricEncryptorBase : IEncryptor
  {

    private readonly SymmetricAlgorithm _cryptoProvider;
    private byte[] _myBytes;

    protected SymmetricEncryptorBase(
      SymmetricAlgorithm cryptoProvider)
    {
      _cryptoProvider = cryptoProvider;
    }

    public string EncryptionKey { get; set; }

    public string Encrypt(string plainText)
    {
      var bytes = GetEncryptionKeyBytes();
      using (var memoryStream = new MemoryStream())
      {
        ICryptoTransform encryptor = _cryptoProvider
          .CreateEncryptor(bytes, bytes);

        using (var cryptoStream = new CryptoStream(
          memoryStream, encryptor, CryptoStreamMode.Write))
        {
          using (var writer = new StreamWriter(cryptoStream))
          {
            writer.Write(plainText);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(
              memoryStream.GetBuffer(),
              0,
              (int) memoryStream.Length);
          }
        }
      }
    }

    private byte[] GetEncryptionKeyBytes()
    {
      if (_myBytes == null)
        _myBytes = Encoding.ASCII.GetBytes(EncryptionKey);

      return _myBytes;
    }

    public string Decrypt(string encryptedText)
    {
      var bytes = GetEncryptionKeyBytes();
      using (var memoryStream = new MemoryStream(
        Convert.FromBase64String(encryptedText)))
      {
        ICryptoTransform decryptor = _cryptoProvider
          .CreateDecryptor(bytes, bytes);
        using (var cryptoStream = new CryptoStream(
          memoryStream, decryptor, CryptoStreamMode.Read))
        {
          using (var reader = new StreamReader(cryptoStream))
          {
            return reader.ReadToEnd();
          }
        }
      }
    }

  }

}
