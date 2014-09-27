using FluentNHibernate.Mapping;

namespace Eg.FluentMappings.Mappings
{
  public class BookMapping : SubclassMap<Book>
  {

    public BookMapping()
    {
      Map(b => b.Author);
      Map(b => b.ISBN);
    }

  }
}
