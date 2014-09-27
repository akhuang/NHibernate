using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;

namespace Eg.AttributeValidation.Runner
{
  public class BasicSharedEngineProvider : 
    ISharedEngineProvider
  {
       

    private readonly ValidatorEngine ve;

    public BasicSharedEngineProvider(ValidatorEngine ve)
    {
      this.ve = ve;
    }

    public ValidatorEngine GetEngine()
    {
      return ve;
    }

    public void UseMe()
    {
      Environment.SharedEngineProvider = this;
    }

  }
}
