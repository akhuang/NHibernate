using System;
using System.Collections.Generic;
using CpBT.Models;
using Eg.Core;

namespace CpBT
{
  class Program
  {
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      var container = ContainerProvider.Container;

      Movie movie = CreateNewMovie();
      Guid movieId;

      var model = container.GetService<IEditMovieModel>();

      model.SaveMovie(movie);
      movieId = movie.Id;
      model.SaveAll();
      movie = null;

      movie = model.GetMovie(movieId);
      movie.Description = "Greatest Movie Ever";
      model.CancelAll();

    }

    static Movie CreateNewMovie()
    {
      return new Movie()
      {
        Name = "Hackers",
        Description = "Bad",
        UnitPrice = 12.59M,
        Director = "Iain Softley",
        Actors = new List<ActorRole>()
        {
          new ActorRole() 
          { 
            Actor = "Jonny Lee Miller", 
            Role="Zero Cool"
          },
          new ActorRole() 
          { 
            Actor = "Angelina Jolie", 
            Role="Acid Burn"
          }
        }
      };

    }

  }
}
