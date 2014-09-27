using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Eg.Core.Data.Queries;

namespace Eg.Core.Data.Impl.Tests
{
  [TestFixture]
  public class AdvancedProductSearchTests : DALFixture 
  {

    private void CreateTestData()
    {
      var products = new List<Product>() {
        new Product()
        {
          Name = "Ice",
          Description = "Cool stuff",
          UnitPrice = 1M
        },
        new Product()
        {
          Name = "Hot Cocoa",
          Description = "Hot stuff",
          UnitPrice = 2M
        },
        new Book() 
        {
          Name = "NHibernate 3.0 Cookbook",
          Description = "Description goes here",
          UnitPrice = 45.95M,
          ISBN = "12345",
          Author = "Jason Dentler"
        },
        new Book() 
        {
          Name = "Some Other Book",
          Description = "Description goes here",
          UnitPrice = 45.95M,
          ISBN = "23456",
          Author = "Joe Author"
        },
        new Movie()
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
        }
      };

      using (var tx = Session.BeginTransaction())
      {
        foreach (var p in products)
          Session.Save(p);
        tx.Commit();
      }

    }

    [Test]
    public void SearchByProductName()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Name = "cocoa";
      var actual = query.Execute();
      Assert.AreEqual(1, actual.Count());
      Assert.That(
        actual.All(p => p.Name.ToLower().Contains("cocoa")));
    }

    [Test]
    public void SearchByProductDescription()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Description = "Stuff";
      var actual = query.Execute();

      Assert.AreEqual(2, actual.Count());
      Assert.True(actual.All(p => p.Description
        .ToLower().Contains("stuff")));
    }

    [Test]
    public void SearchByMinPrice()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.MinimumPrice = 2M;
      var actual = query.Execute();

      Assert.AreEqual(4, actual.Count());
      Assert.True(actual.All(p => p.UnitPrice >= 2M));
    }

    [Test]
    public void SearchByMaxPrice()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.MaximumPrice = 2M;
      var actual = query.Execute();

      Assert.AreEqual(2, actual.Count());
      Assert.True(actual.All(p => p.UnitPrice <= 2M));
    }

    [Test]
    public void SearchByAuthor()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Author = "Dentler";
      var actual = query.Execute();

      Assert.AreEqual(1, actual.Count());
      Assert.True(actual.All(p => p is Book && 
        ((Book)p).Author.ToLower().Contains("dentler")));
    }

    [Test]
    public void SearchByISBN()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.ISBN = "12345";
      var actual = query.Execute();

      Assert.AreEqual(1, actual.Count());
      Assert.True(actual.All(p => p is Book &&
        ((Book)p).ISBN == "12345"));
    }

    [Test]
    public void SearchByDirectorTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Director = "burt";
      var actual = query.Execute();

      Assert.AreEqual(1, actual.Count());
      Assert.True(actual.All(p => p is Movie &&
        ((Movie)p).Director.ToLower().Contains("burt")));
    }

    [Test]
    public void SearchByActorTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Actor = "jack";
      var actual = query.Execute();

      Assert.AreEqual(1, actual.Count());
      Assert.True(actual.All(p => p is Movie &&
        ((Movie)p).Actors.Any(ar => 
          ar.Actor.ToLower().Contains("jack"))
        ));
    }

    [Test]
    public void CompositeMovieTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.MinimumPrice = 50M;
      query.Actor = "jack";
      var actual = query.Execute();
      Assert.AreEqual(0, actual.Count());
    }

    [Test]
    public void CompositeBookTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.MinimumPrice = 50M;
      query.Author = "Dentler";
      var actual = query.Execute();
      Assert.AreEqual(0, actual.Count());
    }

    [Test]
    public void CompositeProductTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.MinimumPrice = 1M;
      query.MaximumPrice = 1.5M;
      query.Description = "stuff";
      var actual = query.Execute();
      Assert.AreEqual(1, actual.Count());
      Assert.True(actual.All(p => 
        p.UnitPrice >= 1 && 
        p.UnitPrice <= 1.5M &&
        p.Description.ToLower().Contains("stuff")));
    }

    [Test]
    public void SortByPriceAscTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Sort = AdvancedProductSearchSort.PriceAsc;
      var actual = query.Execute();

      Assert.True(
        actual.SequenceEqual(
        actual.OrderBy(p => p.UnitPrice)));
    }

    [Test]
    public void SortByPriceDescTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Sort = AdvancedProductSearchSort.PriceDesc;
      var actual = query.Execute();

      Assert.True(
        actual.SequenceEqual(
        actual.OrderByDescending(p => p.UnitPrice)));
    }

    [Test]
    public void SortByNameTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var query = repository.CreateQuery<IAdvancedProductSearch>();
      query.Sort = AdvancedProductSearchSort.Name;
      var actual = query.Execute();

      Assert.True(
        actual.SequenceEqual(
        actual.OrderBy(p => p.Name)));
    }





  }


}
