
namespace Eg.FluentMappings
{
    public class Book : Product 
    {

        public virtual string ISBN { get; set; }
        public virtual string Author { get; set; }

    }
}
