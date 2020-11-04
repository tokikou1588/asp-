﻿using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.BLL
{
   public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
       /// <summary>
       /// 删除角色信息
       /// </summary>
       /// <param name="list"></param>
       /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var roleInfos = this.DbSession.RoleInfoDal.LoadEntities(c=>list.Contains(c.ID));
            foreach (var roleInfo in roleInfos)
            {
                this.DbSession.RoleInfoDal.DeleteEntity(roleInfo);
            }
            return this.DbSession.SaveChanges();
        }
    }
}
