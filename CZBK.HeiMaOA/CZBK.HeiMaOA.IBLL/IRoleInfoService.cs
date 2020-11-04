using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.IBLL
{
   public partial interface IRoleInfoService:IBaseService<RoleInfo>
    {
       bool DeleteEntities(List<int>list);
    }
}
