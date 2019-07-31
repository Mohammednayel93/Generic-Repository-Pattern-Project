using System;
using System.Linq;
using System.Linq.Expressions;
using static DAL.CommonEnums.CommonEnum;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        int Update(T entity, object key);
        int Delete(object key, bool physiaclDelete);
        IQueryable<T> GetAll(RowStatus rowstatus);

        T GetById(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllData();
    }
}
