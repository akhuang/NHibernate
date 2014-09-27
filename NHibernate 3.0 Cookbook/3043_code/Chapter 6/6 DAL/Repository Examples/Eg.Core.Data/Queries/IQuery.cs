using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eg.Core.Data.Queries
{

  public interface IQuery
  {
  }

  public interface IQuery<TResult> : IQuery 
  {

    TResult Execute();

  }

}
