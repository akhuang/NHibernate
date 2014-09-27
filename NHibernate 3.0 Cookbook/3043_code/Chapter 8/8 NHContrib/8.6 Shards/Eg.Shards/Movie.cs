using System.Collections.Generic;

namespace Eg.Shards
{
    public class Movie : Product 
    {

        public virtual string Director { get; set; }
        public virtual IList<ActorRole> Actors { get; set; }

    }
}
