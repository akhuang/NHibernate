using System;

namespace WKITExample
{

  public class Address
  {

    public virtual Guid Id { get; set; }
    public virtual string Line1 { get; set; }
    public virtual string Line2 { get; set; }
    public virtual string City { get; set; }
    public virtual State State { get; set; }
    public virtual string Zip { get; set; }

  }

}
