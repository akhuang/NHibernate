using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{

  public class PagedResult<T>
  {

    public int TotalItems { get; set; }
    public IEnumerable<T> PageOfResults { get; set; }

  }

}
