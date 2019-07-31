using DAL.Interfaces;

using System;
using System.Linq;
using System.Linq.Expressions;
using static DAL.CommonEnums.CommonEnum;

namespace DAL.Repositry
{

    public class Repository<T> : IRepository<T> where T : class
    {
        UnitOfWork unitOfWork;


        public Repository(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public T GetById(object id)
        {
            return unitOfWork.context.Set<T>().Find(id);
        }
        public int Delete(object key, bool physiaclDelete = true)
        {
            T row = GetById(key);

            if (row != null)
            {
                if (physiaclDelete == false)
                {
                    // (row as EntityBase).IsDeleted = true;
                    var _Proo_Deletebj = row.GetType().GetProperty("IsDeleted");
                    if (_Proo_Deletebj != null)
                        _Proo_Deletebj.SetValue(row, true);
                    Update(row, key);
                }
                else
                {
                    DeletePhysical(row);
                }
            }
            return unitOfWork.SaveChanges();
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return unitOfWork.context.Set<T>().Where(predicate);
        }


        public IQueryable<T> GetAllData()
        {
            return unitOfWork.context.Set<T>().AsQueryable<T>();
        }


        public int Insert(T entity)
        {

            unitOfWork.context.Set<T>().Add(entity);
            return unitOfWork.SaveChanges();
        }
        private int DeletePhysical(T entity)
        {

            unitOfWork.context.Set<T>().Remove(entity);
            return unitOfWork.SaveChanges();
        }
        public int Update(T entity, object key)
        {
            T existing = unitOfWork.context.Set<T>().Find(key) as T;
            if (existing != null)
            {
                unitOfWork.context.Entry(existing).CurrentValues.SetValues(entity);
                return unitOfWork.SaveChanges();
            }
            return -1;
        }


        public virtual IQueryable<T> GetAll(RowStatus rowstatus = RowStatus.EXISTS)
        {
            var all = unitOfWork.context.Set<T>().AsQueryable<T>();

            {
                switch (rowstatus)
                {
                    case RowStatus.ALL:
                        return all;
                    case RowStatus.DELETED:
                        return ExtensionMethods.Where(all, "IsDeleted", true);
                    case RowStatus.EXISTS:
                        return ExtensionMethods.Where(all, "IsDeleted", false);
                    default:
                        return all;

                }
            }
        }



    }
}
