using System.Collections.Generic;
using Eg.Core.Data.Queries;
using LinqSpecs;
using NUnit.Framework;

namespace Eg.Core.Data.Impl.Tests
{

  [TestFixture]
  public class MovieSpecificationTests : DALFixture
  {

    private Movie SaveMovie()
    {
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
        Session.Save(movie);
        tx.Commit();
      }
      return movie;
    }

    [Test]
    public void DirectedBy_Test()
    {
      var repo = Locator.GetInstance<IRepository<Movie>>();
      var expected = SaveMovie();
      var spec = new MoviesDirectedBy(expected.Director);

      var actual = repo.FindOne(spec);

      Assert.AreSame(expected, actual);

    }

    [Test]
    public void Starring_Test()
    {
      var repo = Locator.GetInstance<IRepository<Movie>>();
      var expected = SaveMovie();
      var spec = new MoviesStarring(expected.Actors[0].Actor);

      var actual = repo.FindOne(spec);

      Assert.AreSame(expected, actual);
    }

    [Test]
    public void Composite_Test()
    {
      var repo = Locator.GetInstance<IRepository<Movie>>();
      var expected = SaveMovie();

      var spec1 = new MoviesDirectedBy(expected.Director);
      var spec2 = new MoviesStarring(expected.Actors[0].Actor);

      var spec = spec1 & spec2;

      var actual = repo.FindOne(spec);

      Assert.AreSame(expected, actual);
    }

  }

}
