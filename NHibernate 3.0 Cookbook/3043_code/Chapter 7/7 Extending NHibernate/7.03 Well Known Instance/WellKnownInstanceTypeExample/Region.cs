using System;

namespace WKITExample
{

  [Serializable]
  public class Region
  {

    public virtual int Id { get; protected set; }
    public virtual string Name { get; set; }

    internal Region(int id)
    {
      this.Id = id;
    }

  }

}
