using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.SpringNet
{
   public class UserInfoService:IUserInfoService
    {
       public string UserName { get; set; }
       public Person Person { get; set; }
        public string ShowMsg(string msg)
        {
            return "Hello"+UserName+":"+msg+"年龄是:"+Person.Age;
        }
    }
}
