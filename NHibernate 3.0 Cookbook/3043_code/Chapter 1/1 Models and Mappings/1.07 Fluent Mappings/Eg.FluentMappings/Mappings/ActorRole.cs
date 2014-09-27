using FluentNHibernate.Mapping;

namespace Eg.FluentMappings.Mappings
{
  public class ActorRoleMapping : ClassMap<ActorRole>
  {

    public ActorRoleMapping()
    {
      Id(ar => ar.Id)
        .GeneratedBy.GuidComb();
      Version(ar => ar.Version);
      Map(ar => ar.Actor)
        .Not.Nullable();
      Map(ar => ar.Role)
        .Not.Nullable();
    }

  }
}
