using System;

namespace WKITExample
{

  [Serializable]
  public class State
  {

    public virtual string PostalCode { get; private set; }
    public virtual string Name { get; private set; }

    internal State(string postalCode, string name)
    {
      PostalCode = postalCode;
      Name = name;
    }

  }

}
