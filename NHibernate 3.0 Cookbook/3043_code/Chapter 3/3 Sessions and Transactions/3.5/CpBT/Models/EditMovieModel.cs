using System;
using CpBT.DataAccess;
using Eg.Core;
using uNhAddIns.Adapters;

namespace CpBT.Models
{

  [PersistenceConversational(
    MethodsIncludeMode=MethodsIncludeMode.Implicit)]
  public class EditMovieModel : IEditMovieModel 
  {

    private readonly IDao<Movie> _movieDao;

    public EditMovieModel(IDao<Movie> movieDao)
    {
      _movieDao = movieDao;
    }

    public virtual Movie GetMovie(Guid movieId)
    {
      return _movieDao.Get(movieId);
    }

    public virtual void SaveMovie(Movie movie)
    {
      _movieDao.Save(movie);
    }

    [PersistenceConversation(
      ConversationEndMode=EndMode.End)]
    public virtual void SaveAll()
    {
    }

    [PersistenceConversation(
      ConversationEndMode=EndMode.Abort)]
    public virtual void CancelAll()
    {
    }

  }

}
