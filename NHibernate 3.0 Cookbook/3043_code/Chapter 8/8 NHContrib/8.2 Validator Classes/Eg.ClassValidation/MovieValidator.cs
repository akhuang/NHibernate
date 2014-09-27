using Eg.Core;
using NHibernate.Validator.Cfg.Loquacious;

namespace Eg.ClassValidation
{

  public class MovieValidator : ValidationDef<Movie>
  {

    public MovieValidator()
    {
      Define(m => m.Director)
        .NotNullableAndNotEmpty()
        .And.MaxLength(255);
      
      Define(m => m.Actors)
        .HasValidElements();
    }

  }

}
