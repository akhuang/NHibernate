using Eg.Core;
using NHibernate.Validator.Cfg.Loquacious;

namespace Eg.ClassValidation
{

  public class BookValidator : ValidationDef<Book>
  {
    
    public BookValidator()
    {
      Define(b => b.ISBN)
        .NotNullable()
        .And.MatchWith(@"^\{9}[\dxX]$");

      Define(b => b.Author)
        .NotNullableAndNotEmpty()
        .And.MaxLength(255);
    }

  }

}
