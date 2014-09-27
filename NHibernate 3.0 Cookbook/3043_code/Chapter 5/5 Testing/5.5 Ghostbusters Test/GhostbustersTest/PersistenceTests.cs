using System;
using System.Collections.Generic;
using Eg.Core;
using NUnit.Framework;

namespace GhostbustersTest
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

    [Test]
    public void GhostbustersTest()
    {

      using (var tx = Session.BeginTransaction())
      {

        Session.Save(new Movie()
        {
          Name = "Ghostbusters",
          Description = "Science Fiction Comedy",
          Director = "Ivan Reitman",
          UnitPrice = 7.97M,
          Actors = new List<ActorRole>()
          {
            new ActorRole() 
            { 
              Actor = "Bill Murray",
              Role = "Dr. Peter Venkman"
            }
          }
        });

        Session.Save(new Book()
        {
          Name = "Who You Gonna Call?",
          Description = "The Real Ghostbusters comic series",
          UnitPrice = 30.00M,
          Author = "Dan Abnett",
          ISBN = "1-84576-141-3"
        });

        tx.Commit();
      }

      new Ghostbusters(
        NHConfigurator.Configuration,
        NHConfigurator.SessionFactory,
        new Action<string>(msg => Assert.Fail(msg)),
        new Action<string>(msg => Assert.Inconclusive(msg))
      ).Test();


    }

  }

}
