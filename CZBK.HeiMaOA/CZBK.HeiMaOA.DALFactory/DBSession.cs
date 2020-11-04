using CZBK.HeiMaOA.DAL;
using CZBK.HeiMaOA.IDAL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.DALFactory
{
    /// <summary>
    /// DBSession：数据会话层，赋责数据操作类实例的创建。然后业务层调用数据会话层，获取相应的数据操作类实例。所以说，DBSession(数据会话层)其实就是一个工厂类，完成了业务层与数据层的解耦.
    /// </summary>
   public partial class DBSession:IDBSession
    {
       //DbContext Db = new OAEntities();
       public DbContext Db {
           get { return DBContextFactory.CreateDbContext(); }//完成EF上下文创建
       }
       //private IUserInfoDal _UserInfoDal;
       //public IUserInfoDal UserInfoDal
       //{
       //    get {
       //        if (_UserInfoDal == null)
       //        {
       //            //_UserInfoDal = new UserInfoDal();//这里不能直接new,因为DBSession与数据层耦合.
       //            _UserInfoDal = DALAbstractFactory.CreateUserInfoDal();//通过抽象工厂将数据会话层与数据层解耦. 
       //        }
       //        return _UserInfoDal;
       //    }
       //    set
       //    {
       //        _UserInfoDal = value;
       //    }
       //}

       /// <summary>
       /// 保存数据。一个业务中有可能涉及到对多张表的操作，那么我们希望是将要操作的数据先追加到EF上下文件中，然后再统一的保存到数据库中。这样，就完成了链接一次数据库，完成了多次操作。提高数据操作的性能。
       /// </summary>
       /// <returns></returns>
       public bool SaveChanges()
       {
           return Db.SaveChanges() > 0;
       }
       /// <summary>
       /// 执行SQL语句。insert ,delete update
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="pars"></param>
       /// <returns></returns>
       public int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars)
       {
           return Db.Database.ExecuteSqlCommand(sql, pars);
       }
       public List<T> ExecuteQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars)
       {
           return Db.Database.SqlQuery<T>(sql, pars).ToList();
       }
    }
}
