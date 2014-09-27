using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Eg.Core.Data.Queries;

namespace Eg.Core.Data.Impl.Tests
{

  [TestFixture]
  public class PagedProductSearchTests : DALFixture 
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
    public void PagingTest()
    {
      CreateTestData();
      var repository = Locator.GetInstance<IRepository<Product>>();

      var allItems = new List<Product>();

      var query = repository.CreateQuery<IPagedProductSearch>();
      query.Sort = PagedProductSearchSort.PriceAsc;
      query.ItemsPerPage = 2;
      query.PageNumber = 1;
      var actual = query.Execute();
      Assert.AreEqual(5, actual.TotalItems);
      Assert.AreEqual(2, actual.PageOfResults.Count());
      allItems.AddRange(actual.PageOfResults);

      query = repository.CreateQuery<IPagedProductSearch>();
      query.Sort = PagedProductSearchSort.PriceAsc;
      query.ItemsPerPage = 2;
      query.PageNumber = 2;
      actual = query.Execute();
      Assert.AreEqual(5, actual.TotalItems);
      Assert.AreEqual(2, actual.PageOfResults.Count());
      allItems.AddRange(actual.PageOfResults);

      query = repository.CreateQuery<IPagedProductSearch>();
      query.Sort = PagedProductSearchSort.PriceAsc;
      query.ItemsPerPage = 2;
      query.PageNumber = 3;
      actual = query.Execute();
      Assert.AreEqual(5, actual.TotalItems);
      Assert.AreEqual(1, actual.PageOfResults.Count());
      allItems.AddRange(actual.PageOfResults);

      Assert.True(allItems.SequenceEqual(
        allItems.OrderBy(p => p.UnitPrice)));

    }


  }


}
