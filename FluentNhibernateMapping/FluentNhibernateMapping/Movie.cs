using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace FluentNhibernateMapping
{
    public class Movie : Product
    {

        public virtual string Director { get; set; }
        public virtual IList<ActorRole> Actors { get; set; }

    }

    public class MovieMapping : SubclassMap<Movie>
    {
        public MovieMapping()
        {
            Map(p => p.Director);
            HasMany(p => p.Actors)
                .KeyColumn("MovieId") //movie's id in ActorRole
                .AsList(l => l.Column("ActorIndex")); //store position of each element in the list
        }
    }
}
