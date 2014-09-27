using System;
using System.Collections.Generic;
using Eg.Core;
using NUnit.Framework;

namespace SQLitePreloading
{

  [TestFixture]
  public class PersistenceTests : NHibernateFixture
  {

    [Test]
    public void Movie_cascades_save_to_ActorRole()
    {

      Guid movieId;
      Movie movie = new Movie()
      {
        Name = "Mars Attacks",
        Description = "Sci-Fi Parody",
        Director = "Tim Burton",
        UnitPrice = 12M,
        Actors = new List<ActorRole>()
          {
            new ActorRole() {
              Actor = "Jack Nicholson",
              Role = "President James Dale"
            }
          }
      };

      using (var tx = Session.BeginTransaction())
      {
        movieId = (Guid)Session.Save(movie);
        tx.Commit();
      }

      Session.Clear();

      using (var tx = Session.BeginTransaction())
      {
        movie = Session.Get<Movie>(movieId);
        tx.Commit();
      }

      Assert.That(movie.Actors.Count == 1);

    }

  }

}
