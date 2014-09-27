
namespace EncryptedStringExample
{
  public interface IEncryptor
  {
    string Encrypt(string plainText);
    string Decrypt(string encryptedText);
    string EncryptionKey { get; set; }
  }
}
