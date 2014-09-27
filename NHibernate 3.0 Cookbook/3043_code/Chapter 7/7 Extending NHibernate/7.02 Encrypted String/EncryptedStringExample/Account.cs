using System;

namespace EncryptedStringExample
{

  public class Account
  {

    public virtual Guid Id { get; set; }
    public virtual string EMail { get; set; }
    public virtual string Name { get; set; }
    public virtual string CardNumber { get; set; }
    public virtual int ExpirationMonth { get; set; }
    public virtual int ExpirationYear { get; set; }
    public virtual string ZipCode { get; set; }

  }

}
