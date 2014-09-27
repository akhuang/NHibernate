using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{
  public interface IBookWithISBN : IQuery<Book>
  {

    string ISBN { get; set; }

  }
}
