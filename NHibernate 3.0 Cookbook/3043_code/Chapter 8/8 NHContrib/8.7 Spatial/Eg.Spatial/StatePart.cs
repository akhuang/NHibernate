using GeoAPI.Geometries;

namespace Eg.Spatial
{

  public class StatePart
  {

    public virtual int Id { get; protected set; }
    public virtual string Name { get; protected set; }
    public virtual float Area { get; protected set; }
    public virtual float Perimeter { get; protected set; }
    public virtual IGeometry Geometry { get; protected set; }

  }

}
