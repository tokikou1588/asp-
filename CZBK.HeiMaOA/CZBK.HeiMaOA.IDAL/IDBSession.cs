using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.IDAL
{
   public partial interface IDBSession
    {
       //IUserInfoDal UserInfoDal { get; set; }
       bool SaveChanges();
       int ExecuteSql(string sql,params System.Data.SqlClient.SqlParameter[] pars);
       List<T> ExecuteQuery<T>(string sql, params System.Data.SqlClient.SqlParameter[] pars);
    }
}
