using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
    public partial class ActionGroupService : BaseService<ActionGroup>,IActionGroupService
    {
        /// <summary>
        /// 为权限分配角色
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        public bool SetActionGroupRoleInfo(int groupId, List<int> roleIdList)
        {
            var groupInfo = this.DbSession.ActionGroupDal.LoadEntities(g => g.ID == groupId).FirstOrDefault();
            if (groupInfo != null)
            {
                groupInfo.Role.Clear();
                foreach (var roleId in roleIdList)
                {
                    var roleInfo = this.DbSession.RoleDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    groupInfo.Role.Add(roleInfo);
                }
            }
            return this.DbSession.SaveChanges();

        }

        /// <summary>
        /// 为权限分组
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="actionIdList"></param>
        /// <returns></returns>
        public bool SetActionInfo(int groupId, List<int> actionIdList)
        {
            var groupInfo = this.DbSession.ActionGroupDal.LoadEntities(g => g.ID == groupId).FirstOrDefault();
            if (groupInfo != null)
            {
                groupInfo.ActionInfo.Clear();
                foreach (var actionId in actionIdList)
                {
                    var actionInfo = this.DbSession.ActionInfoDal.LoadEntities(r => r.ID == actionId).FirstOrDefault();
                    groupInfo.ActionInfo.Add(actionInfo);
                }
            }
            return this.DbSession.SaveChanges();
        }
    }
}
