using System;

namespace Changestamp
{

public abstract class Entity : IStampedEntity 
{

  public virtual Guid Id { get; protected set; }

  public virtual string CreatedBy { get; set; }
  public virtual DateTime CreatedTS { get; set; }
  public virtual string ChangedBy { get; set; }
  public virtual DateTime ChangedTS { get; set; }

}

}
