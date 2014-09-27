using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{

  public interface IPagedQuery<T>
    : IQuery<PagedResult<T>>
  {

    int PageNumber { get; set; }
    int ItemsPerPage { get; set; }

  }

}
