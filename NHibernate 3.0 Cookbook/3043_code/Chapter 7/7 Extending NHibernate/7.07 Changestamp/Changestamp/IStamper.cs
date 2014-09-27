using Eg.Core;
using NHibernate.Persister.Entity;

namespace Changestamp
{

public interface IStamper
{

  void Insert(IStampedEntity entity, object[] state, 
    IEntityPersister persister);
  void Update(IStampedEntity entity, object[] oldState,
    object[] state, IEntityPersister persister);

}

}
