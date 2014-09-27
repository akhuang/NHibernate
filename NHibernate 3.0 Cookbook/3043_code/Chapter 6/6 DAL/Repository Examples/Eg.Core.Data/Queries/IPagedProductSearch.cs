using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{

  public enum PagedProductSearchSort
  {
    PriceAsc,
    PriceDesc,
    Name
  }

  public interface IPagedProductSearch
    : IPagedQuery<Product>
  {

    string Name { get; set; }
    string Description { get; set; }
    decimal? MinimumPrice { get; set; }
    decimal? MaximumPrice { get; set; }
    PagedProductSearchSort Sort { get; set; }

  }

}
