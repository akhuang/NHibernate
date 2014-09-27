using System;

namespace IoCByteCode
{

public class UserAccount
{

  private readonly IPasswordHasher _passwordHasher;

  public UserAccount(IPasswordHasher passwordHasher)
  {
    _passwordHasher = passwordHasher;
  }

  public virtual Guid Id { get; protected set; }
  public virtual string EMail { get; protected set; }
  public virtual string HashedPassword { get; protected set; }

  public virtual void SetCredentials(
    string email, string plainTextPassword)
  {
    EMail = email;
    SetPassword(plainTextPassword);
  }

  public virtual void SetPassword(string plainTextPassword)
  {
    HashedPassword = _passwordHasher.HashPassword(
      EMail, plainTextPassword);
  }

}

}
