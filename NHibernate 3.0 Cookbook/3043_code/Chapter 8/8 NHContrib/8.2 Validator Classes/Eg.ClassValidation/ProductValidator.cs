using Eg.Core;
using NHibernate.Validator.Cfg.Loquacious;

namespace Eg.ClassValidation
{

  public class ProductValidator : 
    ValidationDef<Product>
  {
    
    public ProductValidator()
    {
      Define(p => p.Name)
        .NotNullableAndNotEmpty()
        .And.MaxLength(255);

      Define(p => p.Description)
        .NotNullableAndNotEmpty();

      Define(p => p.UnitPrice)
        .GreaterThanOrEqualTo(0M)
        .WithMessage("Unit price can't be negative.");

    }

  }

}
