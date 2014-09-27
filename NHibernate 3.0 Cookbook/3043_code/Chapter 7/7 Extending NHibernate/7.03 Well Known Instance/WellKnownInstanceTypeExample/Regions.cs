using System;
using System.Collections.ObjectModel;

namespace WKITExample
{

  public class Regions : ReadOnlyCollection<Region>
  {

    public static Region NorthWest = 
      new Region(1) { Name = "Northwest" };

    public static Region SouthWest =
      new Region(2) { Name = "Southwest" };

    public static Region MidWest =
      new Region(3) { Name = "Midwest" };

    public static Region Eastern =
      new Region(4) { Name = "Eastern" };

    public static Region South =
      new Region(5) { Name = "South" };

    public Regions()
      : base(new Region[] { NorthWest, SouthWest, 
        MidWest, Eastern, South })
    { }

  }

}
