using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Impl.Queries
{

  public interface INamedQuery
  {
    string QueryName { get; }
    IDictionary<string, object> Parameters { get; }
  }

}
