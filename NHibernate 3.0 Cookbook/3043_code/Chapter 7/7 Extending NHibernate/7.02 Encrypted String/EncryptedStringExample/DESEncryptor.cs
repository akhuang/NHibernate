using System.Configuration;
using System.Security.Cryptography;

namespace EncryptedStringExample
{

  public class DESEncryptor : SymmetricEncryptorBase
  {

    public DESEncryptor()
      : base(new DESCryptoServiceProvider())
    { }

  }

}
