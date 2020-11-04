using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.BLL
{
   public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {

        
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
        //public void SetUserRole()
        //{
        //    this.DbSession.UserInfoDal.AddEntity(userInfo);
        //    this.DbSession.UserInfoDal.DeleteEntity(userInfo);
        //    this.DbSession.UserInfoDal.EditEntity(userInfo);
        //    this.DbSession.SaveChanges();
        //}
       /// <summary>
       /// 批量删除用户数据
       /// </summary>
       /// <param name="list">要删除的记录的编号</param>
       /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList=this.DbSession.UserInfoDal.LoadEntities(u=>list.Contains(u.ID));
            foreach (var userInfo in userInfoList)
            {
                this.DbSession.UserInfoDal.DeleteEntity(userInfo);
            }
           return this.DbSession.SaveChanges();
        }

       /// <summary>
       /// 完成用户信息搜索
       /// </summary>
       /// <param name="userInfoSearch"></param>
       /// <returns></returns>
        public IQueryable<UserInfo> LoadSearchEntities(Model.Search.UserInfoSearch userInfoSearch,short delFlag)
        {
            var temp = this.DbSession.UserInfoDal.LoadEntities(u=>u.DelFlag==delFlag);
            if (!string.IsNullOrEmpty(userInfoSearch.UName))
            {
                temp = temp.Where<UserInfo>(u=>u.UName.Contains(userInfoSearch.UName));
            }
            if (!string.IsNullOrEmpty(userInfoSearch.URemark))
            {
                temp = temp.Where<UserInfo>(u=>u.Remark.Contains(userInfoSearch.URemark));
            }
            userInfoSearch.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo,int>(u=>u.ID).Skip<UserInfo>((userInfoSearch.PageIndex-1)*userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize);
        }

       /// <summary>
       /// 完成用户角色的分配
       /// </summary>
       /// <param name="userId">用户编号</param>
       /// <param name="roleIdList">要分配的角色的编号</param>
       /// <returns></returns>
        public bool SetUserRoleInfo(int userId, List<int> roleIdList)
        {
            var userInfo = this.DbSession.UserInfoDal.LoadEntities(u=>u.ID==userId).FirstOrDefault();
            if (userInfo != null)
            {
                //删除用户已经有的角色。
                userInfo.RoleInfo.Clear();
                foreach (int roleId in roleIdList)
                {
                    var roleInfo = this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    userInfo.RoleInfo.Add(roleInfo);
                }
                return this.DbSession.SaveChanges();
            }
            return false;
        }
    }
   //public class OrderInfoSerive : BaseService<OrderInfo>
   //{

   //    public override void SetCurrentDal()
   //    {
   //        CurrentDal = this.DbSession.OrderInfoDal;
   //    }
   //}
   
}
