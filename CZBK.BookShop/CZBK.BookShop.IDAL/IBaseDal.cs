using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IDAL
{
   public interface IBaseDal<T>where T:class,new()
    {
       IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
       IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc);
       IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc);

       bool DeleteEntity(T entity);
       bool UpdateEntity(T entity);
       T AddEntity(T entity);
    }
}
