using System;

namespace CpBT.DataAccess
{

  public interface IDao<TEntity>
  {

    TEntity Get(Guid Id);
    void Save(TEntity entity);

  }

}
