using System;
using System.Security.Principal;
using NHibernate.Persister.Entity;

namespace Changestamp
{

public class Stamper : IStamper 
{

  private const string CREATED_BY = "CreatedBy";
  private const string CREATED_TS = "CreatedTS";
  private const string CHANGED_BY = "ChangedBy";
  private const string CHANGED_TS = "ChangedTS";

  public void Insert(IStampedEntity entity, object[] state, 
    IEntityPersister persister)
  {
    if (entity == null)
      return;
    SetCreate(entity, state, persister);
    SetChange(entity, state, persister);
  }

  public void Update(IStampedEntity entity, object[] oldState, 
    object[] state, IEntityPersister persister)
  {
    if (entity == null)
      return;
    SetChange(entity, state, persister);
  }

  private void SetCreate(IStampedEntity entity, object[] state,
    IEntityPersister persister)
  {
    entity.CreatedBy = GetUserName();
    SetState(persister, state, CREATED_BY, entity.CreatedBy);
    entity.CreatedTS = DateTime.Now;
    SetState(persister, state, CREATED_TS, entity.CreatedTS);
  }

  private void SetChange(IStampedEntity entity, 
    object[] state, IEntityPersister persister)
  {
    entity.ChangedBy = GetUserName();
    SetState(persister, state, CHANGED_BY, 
      entity.ChangedBy);
    entity.ChangedTS = DateTime.Now;
    SetState(persister, state, CHANGED_TS, 
      entity.ChangedTS);
  }
  
  private void SetState(IEntityPersister persister, 
    object[] state, string propertyName, object value)
  {
    var index = GetIndex(persister, propertyName);
    if (index == -1)
      return;
    state[index] = value;
  }

  private int GetIndex(IEntityPersister persister, 
    string propertyName)
  {
    return Array.IndexOf(persister.PropertyNames, 
      propertyName);
  }

  private string GetUserName()
  {
    return WindowsIdentity.GetCurrent().Name; 
  }



}

}
