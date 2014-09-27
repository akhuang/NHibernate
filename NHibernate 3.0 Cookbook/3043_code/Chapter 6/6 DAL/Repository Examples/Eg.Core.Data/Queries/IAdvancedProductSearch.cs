using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{

  public enum AdvancedProductSearchSort
  {
    PriceAsc,
    PriceDesc,
    Name
  }

  public interface IAdvancedProductSearch : IQuery<IEnumerable<Product>>
  {

    string Name { get; set; }
    string Description { get; set; }
    decimal? MinimumPrice { get; set; }
    decimal? MaximumPrice { get; set; }
    string ISBN { get; set; }
    string Author { get; set; }
    string Director { get; set; }
    string Actor { get; set; }
    AdvancedProductSearchSort Sort { get; set; }

  }
}
