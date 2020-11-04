using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface IActionGroupService:IBaseService<ActionGroup>
    {
       bool SetActionGroupRoleInfo(int groupId,List<int>roleIdList);
       bool SetActionInfo(int groupId, List<int> actionIdList);
    }
}
