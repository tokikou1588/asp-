using CZBK.BookShop.DAL;
using CZBK.BookShop.IDAL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.DALFactory
{
    /// <summary>
    /// 数据会话层（工厂类）:1.封装了所有的数据操作类实例的创建.业务层通过数据会话层，就可以获取具体的数据操作类实例。
    /// 2:将业务层与数据层解耦.
    /// 3:为EF开放了一个统一的数据处理方法(接口)。
    /// </summary>
  public partial  class DBSession:IDBSession
    {
      //book_shopEntities db = new book_shopEntities();//保证EF上下文线程唯一。
      public DbContext Db { get { return DbContextFactory.CreateCurrentDbContext(); } }
      //private IUserInfoDal _userInfoDal;
      //public IUserInfoDal UserInfoDal
      //{
      //    get {
      //        if (_userInfoDal == null)
      //        {
      //        //    _userInfoDal = new UserInfoDal();//DBSession与数据层耦合在一起，这里可以使用抽象工厂解决.
      //            _userInfoDal = AbstractFactory.CreateUserInfoDal();
      //        }
      //        return _userInfoDal;
      //    }
      //    set { _userInfoDal = value; }
      //}

      /// <summary>
      /// 作用：一个业务中有可能涉及到对多张表进行操作，为了减少与数据库连接次数，提供性能。我们应该将这次业务涉及到的数据添加EF上下文中，并且打上标记，最后一次性提交给数据进行处理。
      /// </summary>
      /// <returns></returns>
      public bool SaveChanges()
      {
          return Db.SaveChanges() > 0;
      }
      public int ExecuteSql(string sql,params System.Data.SqlClient.SqlParameter[] pars)
      {
         return Db.Database.ExecuteSqlCommand(sql,pars);
      }
      public List<T> ExecuteSelectSql<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars)
      {
          return Db.Database.SqlQuery<T>(sql,pars).ToList();
      }

    }
}
