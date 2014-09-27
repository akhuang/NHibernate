using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComponentExamples
{
  public class Address
  {

    public virtual string Lines { get; set; }
    public virtual string City { get; set; }
    public virtual string State { get; set; }
    public virtual string ZipCode { get; set; }

  }
}
