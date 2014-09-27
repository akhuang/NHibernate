using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eg.Core;

namespace CpBT.Models
{
  public interface IEditMovieModel
  {

    Movie GetMovie(Guid movieId);
    void SaveMovie(Movie movie);
    void SaveAll();
    void CancelAll();

  }
}
