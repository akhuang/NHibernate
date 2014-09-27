using System;
using System.Collections.Generic;
using Eg.Core;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace PersistenceTester
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
    public void Product_persistence_test()
    {
      new PersistenceSpecification<Product>(Session)
        .CheckProperty(p => p.Name, "Product Name")
        .CheckProperty(p => p.Description, "Product Description")
        .CheckProperty(p => p.UnitPrice, 300.85M)
        .VerifyTheMappings();
    }

    [Test]
    public void ActorRole_persistence_test()
    {
      new PersistenceSpecification<ActorRole>(Session)
      .CheckProperty(p => p.Actor, "Actor Name")
      .CheckProperty(p => p.Role, "Role")
      .VerifyTheMappings();
    }

    [Test]
    public void Movie_persistence_test()
    {
      new PersistenceSpecification<Movie>(Session)
      .CheckProperty(p => p.Name, "Movie Name")
      .CheckProperty(p => p.Description, "Movie Description")
      .CheckProperty(p => p.UnitPrice, 25M)
      .CheckProperty(p => p.Director, "Director Name")
      .CheckList(p => p.Actors, new List<ActorRole>()
      {
        new ActorRole() { Actor = "Actor Name", Role = "Role" }
      })
      .VerifyTheMappings();
    }


  }

}
