using Eg.Core;
using NHibernate.Event;

namespace AuditEventListener
{
  public class EventListener :
    IPreInsertEventListener,
    IPreUpdateEventListener,
    IPreDeleteEventListener 
  {

    private readonly IAuditLogger _logger;

    public EventListener()
      : this(new AuditLogger())
    { }

    public EventListener(IAuditLogger logger)
    {
      _logger = logger;
    }

    public bool OnPreInsert(PreInsertEvent e)
    {
      _logger.Insert(e.Entity as Entity);
      return false;
    }

    public bool OnPreUpdate(PreUpdateEvent e)
    {
      _logger.Update(e.Entity as Entity);
      return false;
    }

    public bool OnPreDelete(PreDeleteEvent e)
    {
      _logger.Delete(e.Entity as Entity);
      return false;
    }

  }
}
