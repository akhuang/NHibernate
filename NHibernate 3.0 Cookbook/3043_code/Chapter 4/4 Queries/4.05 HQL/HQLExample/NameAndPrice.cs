using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HQLExample
{
  public class NameAndPrice
  {

    public NameAndPrice(string name, decimal unitPrice)
    {
      Name = name;
      UnitPrice = unitPrice;
    }

    public NameAndPrice(string name, double unitPrice)
    {
      Name = name;
      UnitPrice = (decimal) unitPrice;
    }

    public string Name { get; set; }
    public decimal UnitPrice { get; set; }

  }
}
