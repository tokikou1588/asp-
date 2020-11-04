using CZBK.BookShop.DALFactory;
using CZBK.BookShop.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
    /// <summary>
    /// 封装了业务层具体的CURD方法的具体的实现
    /// </summary>
    public abstract class BaseService<T> where T : class,new()
    {
        public IDBSession DbSession
        {
            //get { return new DBSession(); }
            get { return DBSessionFactory.CreateCurrentDbSession(); }
        }
        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();//子类必须要实现该抽象方法。
        }
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDal.LoadEntities(whereLambda);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc)
        {
            return this.CurrentDal.LoadPageEntities<s>(pageIndex,pageSize,out totalCount,orderbyLambda,whereLambda,isAsc);
        }
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize,  System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc)
        {
            return this.CurrentDal.LoadPageEntities<s>(pageIndex, pageSize,orderbyLambda, whereLambda, isAsc);
        }

        public bool DeleteEntity(T entity)
        {
            this.CurrentDal.DeleteEntity(entity);
            return this.DbSession.SaveChanges();
        }
        public bool UpdateEntity(T entity)
        {
            this.CurrentDal.UpdateEntity(entity);
            return this.DbSession.SaveChanges();
        }
        public T AddEntity(T entity)
        {
            this.CurrentDal.AddEntity(entity);
            this.DbSession.SaveChanges();
            return entity;
        }
    }
}
