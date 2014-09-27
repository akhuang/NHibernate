using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseEntityExample;

namespace ComponentExamples
{
  public class Customer : Entity<Customer>
  {

    public virtual string Name { get; set; }
    public virtual Address BillingAddress { get; set; }
    public virtual Address ShippingAddress { get; set; }

  }
}
