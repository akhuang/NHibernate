using FluentNHibernate.Mapping;
using System;

namespace FluentNhibernateMapping
{
    public class Product : Entity
    {

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Decimal UnitPrice { get; set; }

    }

    public class ProductMapping : ClassMap<Product>
    {
        public ProductMapping()
        {
            Id(p => p.Id).GeneratedBy.GuidComb();
            DiscriminateSubClassesOnColumn("ProductType");
            Version(p => p.Version);

            NaturalId().Not.ReadOnly().Property(p => p.Name);

            Map(p => p.Description);
            Map(p => p.UnitPrice).Not.Nullable();



        }
    }
}
