using System;

namespace Changestamp
{

public interface IStampedEntity
{

  string CreatedBy { get; set; }
  DateTime CreatedTS { get; set; }
  string ChangedBy { get; set; }
  DateTime ChangedTS { get; set; }

}

}
