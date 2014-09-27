using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eg.Core.Data.Queries;
using NHibernate;

namespace Eg.Core.Data.Impl.Queries
{
  public class BookWithISBN : NamedQueryBase<Book>, IBookWithISBN
  {

    public BookWithISBN(ISessionFactory sessionFactory)
      : base(sessionFactory) { }

    protected override IDictionary<string, object> Parameters
    {
      get
      {
        return new Dictionary<string, object>()
        {
          { "isbn", ISBN }
        };
      }
    }

    public string ISBN { get; set; }

    protected override Book Execute(NHibernate.IQuery query)
    {
      return query.UniqueResult<Book>();
    }

  }
}
