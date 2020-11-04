using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.DAL
{
    /// <summary>
    /// EF上下文对象唯一。
    /// </summary>
   public class DbContextFactory
    {
       public static DbContext CreateCurrentDbContext()
       {
           DbContext db = (DbContext)CallContext.GetData("db");
           if (db == null)
           {
               db = new book_shopEntities();
               CallContext.SetData("db", db);
           }
           return db;
       }
    }
}
