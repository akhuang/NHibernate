using Eg.Core;
using log4net;

namespace AuditEventListener
{

  public class AuditLogger : IAuditLogger 
  {

    private readonly ILog log = LogManager.GetLogger(typeof(AuditLogger));

    public void Insert(Entity entity)
    {
      log.DebugFormat("{0} #{1} inserted.", entity.GetType(), entity.Id);
    }

    public void Update(Entity entity)
    {
      log.DebugFormat("{0} #{1} updated.", entity.GetType(), entity.Id);
    }

    public void Delete(Entity entity)
    {
      log.DebugFormat("{0} #{1} deleted.", entity.GetType(), entity.Id);
    }

  }

}
