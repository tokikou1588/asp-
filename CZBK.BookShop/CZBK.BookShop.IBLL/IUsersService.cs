using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
  public partial  interface IUsersService:IBaseService<Users>
    {
      bool AddEntity(Users userInfo,out string msg);
      bool LoadUserLogin(string userName, string userPwd, out string msg, out Users user);
      void FindUserPwd(Users user);
    }
}
