using System;

namespace MappingEnums
{

public enum AccountTypes
{
  Consumer,
  Business,
  Corporate,
  NonProfit
}

  public class Account
  {

    public virtual Guid Id { get; set; }
    public virtual AccountTypes AcctType { get; set; }
    public virtual string Number { get; set; }
    public virtual string Name { get; set; }

  }


}