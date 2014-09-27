using Eg.Core;
using NHibernate.Event;

namespace Changestamp
{
public class EventListener :
  IPreInsertEventListener,
  IPreUpdateEventListener
{

  private readonly IStamper _stamper;

  public EventListener()
    : this(new Stamper())
  { }

  public EventListener(IStamper stamper)
  {
    _stamper = stamper;
  }

  public bool OnPreInsert(PreInsertEvent e)
  {
    _stamper.Insert(e.Entity as IStampedEntity,
      e.State, e.Persister);
    return false;
  }

  public bool OnPreUpdate(PreUpdateEvent e)
  {
    _stamper.Update(e.Entity as IStampedEntity,
      e.OldState, e.State, e.Persister);
    return false;
  }


}
}
