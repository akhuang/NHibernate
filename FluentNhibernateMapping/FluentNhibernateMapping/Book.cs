
using FluentNHibernate.Mapping;
namespace FluentNhibernateMapping
{
    public class Book : Product
    {

        public virtual string ISBN { get; set; }
        public virtual string Author { get; set; }

    }

    public class BookMapping : SubclassMap<Book>
    {
        public BookMapping()
        {
            Map(p => p.Author);
            Map(p => p.ISBN);
        }
    }
}
