using System;

namespace Eg.Core
{
    public class Product : Entity 
    {

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Decimal UnitPrice { get; set; }

    }
}
