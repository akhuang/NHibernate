using System;
using NHibernate.Validator.Constraints;

namespace Eg.AttributeValidation
{

  [AttributeUsage(AttributeTargets.Field | 
    AttributeTargets.Property)]
  [Serializable]
  public class NotNegativeDecimalAttribute 
    : DecimalMinAttribute
  {

    public NotNegativeDecimalAttribute()
      : base(0M)
    {
    }

  }

}
