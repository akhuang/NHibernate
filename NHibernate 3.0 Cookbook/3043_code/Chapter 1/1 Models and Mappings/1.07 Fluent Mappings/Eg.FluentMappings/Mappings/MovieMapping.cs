using FluentNHibernate.Mapping;

namespace Eg.FluentMappings.Mappings
{
  public class MovieMapping : SubclassMap<Movie>
  {

    public MovieMapping()
    {
      Map(m => m.Director);
      HasMany(m => m.Actors)
        .KeyColumn("MovieId")
        .AsList(l => l.Column("ActorIndex"));
    }

  }
}
