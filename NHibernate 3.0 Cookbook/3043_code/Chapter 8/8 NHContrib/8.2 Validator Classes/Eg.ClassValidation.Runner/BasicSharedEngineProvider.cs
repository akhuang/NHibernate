using NHibernate.Validator.Engine;
using Environment = NHibernate.Validator.Cfg.Environment;

namespace Eg.ClassValidation.Runner
{
  public class BasicSharedEngineProvider : ISharedEngineProvider
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
