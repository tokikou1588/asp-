using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.IDAL
{
   public partial interface IDBSession
    {
       DbContext Db { get; }
      // IUserInfoDal UserInfoDal { get; set; }
       bool SaveChanges();
    }
}
