using System;
using System.Text;
using System.Security.Cryptography;

namespace IoCByteCode
{

public class PasswordHasher : IPasswordHasher 
{

  private readonly HashAlgorithm _algorithm;

  public PasswordHasher(HashAlgorithm algorithm)
  {
    _algorithm = algorithm;
  }

  public string HashPassword(string email, string password)
  {
    var plainText = email + password;
    var plainTextData = Encoding.Default.GetBytes(plainText);
    var hash = _algorithm.ComputeHash(plainTextData);
    return Convert.ToBase64String(hash);
  }

}

}
