using System;
using SessionPerPresenter.Data;
using Eg.Core;

namespace SessionPerPresenter
{

  public class PresenterA : IPresenter 
  {

    private readonly IDao<Movie> _movieDao;
    private readonly IDao<Book> _bookDao;
    private readonly ITransactionProvider _transactionProvider;

    public PresenterA(IDao<Movie> movieDao, 
      IDao<Book> bookDao,
      ITransactionProvider transactionProvider)
    {
      _movieDao = movieDao;
      _bookDao = bookDao;
      _transactionProvider = transactionProvider;
    }

    public void Dispose()
    {
      _movieDao.Dispose();
      _bookDao.Dispose();
    }

  }

}
