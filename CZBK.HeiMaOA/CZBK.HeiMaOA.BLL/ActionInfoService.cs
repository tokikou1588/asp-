using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.BLL
{
    public partial class ActionInfoService :BaseService<ActionInfo>, IActionInfoService
    {
        /// <summary>
        /// 给权限分配角色
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SetActionRoleInfo(int actionId, List<int> list)
        {
            var actionInfo = this.DbSession.ActionInfoDal.LoadEntities(a=>a.ID==actionId).FirstOrDefault();
            if (actionInfo != null)
            {
                actionInfo.RoleInfo.Clear();
                foreach (int roleId in list)
                {
                   var roleInfo= this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                   actionInfo.RoleInfo.Add(roleInfo);
                }
            }
         return   this.DbSession.SaveChanges();
        }
    }
}
