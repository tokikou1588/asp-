using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IDAL
{
   public partial interface IDBSession
    {
       DbContext Db { get; }
      // IUserInfoDal UserInfoDal { get; set; }
       bool SaveChanges();
       int ExecuteSql(string sql, params System.Data.SqlClient.SqlParameter[] pars);
       List<T> ExecuteSelectSql<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars);
    }
}
