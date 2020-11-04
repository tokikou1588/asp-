using CZBK.ItcastOA.DALFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
   public abstract class BaseService<T>where T:class,new()
    {
       /// <summary>
       /// 创建DBSession.,
       /// </summary>
       public IDAL.IDBSession DbSession
       {
           //get { return new DALFactory.DBSession(); }//暂时
           get { return DBSessionFactory.CreateDbSession(); }
       }
       //定义抽象方法
       public abstract void SetCurrentDal();
      public IDAL.IBaseDal<T> CurrentDal { get; set; }//该属性存放的子类确定的具体数据操作类的实例。
       public BaseService()
       {
           SetCurrentDal();
       }
       /// <summary>
       /// 查询方法。
       /// </summary>
       /// <param name="whereLambda"></param>
       /// <returns></returns>
       public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
       {
           //return DbSession.UserInfoDal.LoadEntities(whereLambda);
          return CurrentDal.LoadEntities(whereLambda);
       }
       /// <summary>
       /// 分页
       /// </summary>
       /// <typeparam name="s"></typeparam>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <param name="totalCount"></param>
       /// <param name="whereLambda"></param>
       /// <param name="orderbyLambda"></param>
       /// <param name="isAsc"></param>
       /// <returns></returns>
       public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
       {
           return CurrentDal.LoadPageEntities<s>(pageIndex,pageSize,out totalCount,whereLambda,orderbyLambda,isAsc);
       }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
       public bool DeleteEntity(T entity)
       {
            CurrentDal.DeleteEntity(entity);
            return DbSession.SaveChanges();

       }
         /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
       public bool EditEntity(T entity)
       {
           CurrentDal.EditEntity(entity);
           return DbSession.SaveChanges();
       }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
       public T AddEntity(T entity)
       {
           CurrentDal.AddEntity(entity);
           DbSession.SaveChanges();
           return entity;
       }
    }
}
