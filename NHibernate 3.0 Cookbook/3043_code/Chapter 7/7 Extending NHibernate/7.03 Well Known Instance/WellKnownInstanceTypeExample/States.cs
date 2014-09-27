using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WKITExample
{

  public class States : ReadOnlyCollection<State>
  {

    public static State Arizona = new State("AZ", "Arizona");
    public static State California = new State("CA", "California");
    public static State Colorado = new State("CO", "Colorado");
    public static State Oklahoma = new State("OK", "Oklahoma");
    public static State NewMexico = new State("NM", "New Mexico");
    public static State Nevada = new State("NV", "Nevada");
    public static State Texas = new State("TX", "Texas");
    public static State Utah = new State("UT", "Utah");

    public States()
      : base(new State[] { Arizona, California, Colorado, 
        Oklahoma, NewMexico, Nevada, Texas, Utah })
    { }

  }

}
