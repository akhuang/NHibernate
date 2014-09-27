using NHibernate.Search.Attributes;

namespace Eg.Search
{

  [Indexed]
  public class Book : Product
  {

    [Field(Index = Index.UnTokenized)]
    public virtual string ISBN { get; set; }

    [Field]
    public virtual string Author { get; set; }

  }
}
