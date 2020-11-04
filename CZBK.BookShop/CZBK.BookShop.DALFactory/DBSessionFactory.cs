using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CZBK.BookShop.IDAL;
using System.Runtime.Remoting.Messaging;
namespace CZBK.BookShop.DALFactory
{
   public class DBSessionFactory
    {
       public static IDBSession CreateCurrentDbSession()
       {
           IDBSession dbSession = (IDBSession)CallContext.GetData("dbSession");
           if (dbSession == null)
           {
               dbSession = new DBSession();
               CallContext.SetData("dbSession",dbSession);
           }
           return dbSession;
       }
    }
}
