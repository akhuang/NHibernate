using FluentNHibernate.Mapping;
namespace FluentNhibernateMapping
{
    public class ActorRole : Entity
    {

        public virtual string Actor { get; set; }
        public virtual string Role { get; set; }

    }

    public class ActorRoleMapping : ClassMap<ActorRole>
    {
        public ActorRoleMapping()
        {
            Id(a => a.Id).GeneratedBy.GuidComb();
            Version(a => a.Version);

            Map(x => x.Actor)
                .Not.Nullable();

            Map(x => x.Role)
                .Not.Nullable();
        }
    }
}
