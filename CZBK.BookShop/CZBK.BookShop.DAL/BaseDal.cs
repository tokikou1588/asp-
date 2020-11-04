using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.DAL
{
   public  class BaseDal<T>where T:class,new()
    {
       // book_shopEntities db = new book_shopEntities();
       DbContext db = DbContextFactory.CreateCurrentDbContext();
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
        
            return db.Set<T>().Where<T>(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize,  System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where<T>(whereLambda);
          
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }


        public bool DeleteEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.EntityState.Deleted;
           // return db.SaveChanges() > 0;
            return true;
        }

        public bool UpdateEntity(T entity)
        {
            db.Entry<T>(entity).State = System.Data.EntityState.Modified;
            //return db.SaveChanges() > 0;
            return true;
        }

        public T AddEntity(T entity)
        {
            db.Set <T>().Add(entity);
            //db.SaveChanges();
            return entity;
        }
    }
}
