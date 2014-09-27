using FluentNHibernate.Mapping;

namespace Eg.FluentMappings.Mappings
{
  public class ProductMapping : ClassMap<Product>
  {

    public ProductMapping()
    {
      Id(p => p.Id)
        .GeneratedBy.GuidComb();
      DiscriminateSubClassesOnColumn("ProductType");
      Version(p => p.Version);
      NaturalId()
        .Not.ReadOnly()
        .Property(p => p.Name);
      Map(p => p.Description);
      Map(p => p.UnitPrice)
        .Not.Nullable();
    }

  }
}
