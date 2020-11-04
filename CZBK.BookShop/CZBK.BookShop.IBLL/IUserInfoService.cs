using CZBK.BookShop.Model;
using CZBK.BookShop.Model.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface IUserInfoService:IBaseService<UserInfo>
    {
       bool DeleteEntities(List<int> list);
       IQueryable<UserInfo> LoadSearchUserInfo(UserInfoSearch userInfoSearchParam);
       bool SetUserRole(int userId,List<int>roleIdList);
    }
}
