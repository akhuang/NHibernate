using Eg.Core;

namespace AuditEventListener
{

  public interface IAuditLogger
  {

    void Insert(Entity entity);
    void Update(Entity entity);
    void Delete(Entity entity);

  }

}
