using System;
using System.Collections.Generic;
using System.Linq;
using ConfOrm;
using ConfOrm.NH;
using ConfOrm.Patterns;
using NHibernate.Cfg.MappingSchema;

namespace Eg.ConfORMMapping.Mappings
{

  public class MappingFactory
  {

    private readonly Mapper _mapper;

    public MappingFactory()
    {
      _mapper = GetMapper();
    }

    public HbmMapping CreateMapping()
    {
      return _mapper
        .CompileMappingFor(GetEntityTypes());
    }

    private static 
      ObjectRelationalMapper GetORM()
    {
      var orm = new ObjectRelationalMapper();
      orm.TablePerClassHierarchy<Product>();
      orm.TablePerClass<ActorRole>();

      orm.NaturalId<Product>(p => p.Name);

      orm.Cascade<Movie, ActorRole>(
        Cascade.All | Cascade.DeleteOrphans);

      orm.Patterns.PoidStrategies
        .Add(new GuidOptimizedPoidPattern());

      orm.Patterns.Versions
        .Add(new MyVersionPattern());

      return orm;
    }

    private static Mapper GetMapper()
    {
      var orm = GetORM();
      var mapper = new Mapper(orm);
      
      mapper.AddPropertyPattern(
        m => orm.IsRootEntity(m.DeclaringType) &&
          !m.Name.Equals("Description"), 
        a => a.NotNullable(true));
      return mapper;
    }

    private static IEnumerable<Type> GetEntityTypes()
    {
      var entityType = typeof (Entity);
      return entityType.Assembly.GetTypes()
        .Where(t => entityType.IsAssignableFrom(t));
    }

  }

}
