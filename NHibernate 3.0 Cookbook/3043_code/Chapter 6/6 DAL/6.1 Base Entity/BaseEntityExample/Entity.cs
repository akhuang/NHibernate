using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseEntityExample
{

  public abstract class Entity<TEntity, TId>
    where TEntity : Entity<TEntity, TId>
  {

    public virtual TId Id { get; protected set; }

    public override bool Equals(object obj)
    {
      TEntity other = obj as TEntity;

      if (other == null)
        return false;

      if (ReferenceEquals(this, other))
        return true;

      if (!Equals(default(TId), Id)
        && !Equals(default(TId), other.Id))
        return Equals(Id, other.Id);

      return Equals(other);

    }

    public virtual bool Equals(TEntity other)
    {
      return false;
    }

    public override int GetHashCode()
    {
      if (Id == null)
        return 0;
      return Id.GetHashCode();
    }

  }

  public abstract class Entity<TEntity> : Entity<TEntity, Guid>
     where TEntity : Entity<TEntity>
  {
  }

}
