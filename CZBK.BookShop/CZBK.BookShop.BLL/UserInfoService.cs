using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
   public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
       //public IQueryable<UserInfo> LoadEntities(System.Linq.Expressions.Expression<Func<UserInfo, bool>> whereLambda)
       //{
       //    return this.DbSession.UserInfoDal.LoadEntities(whereLambda);
       //}
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
        //public void SetUserRoleInfo(UserInfo userInfo)
        //{
        //    this.DbSession.UserInfoDal.AddEntity(userInfo);
        //    this.DbSession.RoleInfoDal.AddEntity(roleInfo);
        //    this.DbSession.ActionInfo.UpdateEntity(actionInfo);
        //    this.DbSession.SaveChanges();

        //}

       /// <summary>
       /// 批量删除用户数据
       /// </summary>
       /// <param name="list">要删除的用户的ID</param>
       /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.DbSession.UserInfoDal.LoadEntities(u=>list.Contains(u.ID));
            foreach (var userInfo in userInfoList)
            {
                this.DbSession.UserInfoDal.DeleteEntity(userInfo);
            }
        return  this.DbSession.SaveChanges();
        }

        #region 多条件搜索用户数据
        public IQueryable<UserInfo> LoadSearchUserInfo(Model.SearchParams.UserInfoSearch userInfoSearchParam)
        {
            var temp = this.DbSession.UserInfoDal.LoadEntities(c=>true);
            if (!string.IsNullOrEmpty(userInfoSearchParam.UserName))
            {
                temp = temp.Where<UserInfo>(u=>u.UserName.Contains(userInfoSearchParam.UserName));
            }
            if (!string.IsNullOrEmpty(userInfoSearchParam.UserEmail))
            {
                temp = temp.Where<UserInfo>(u=>u.Email.Contains(userInfoSearchParam.UserEmail));
            }
            userInfoSearchParam.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearchParam.PageIndex - 1) * userInfoSearchParam.PageSize).Take<UserInfo>(userInfoSearchParam.PageSize);
        }
        #endregion


        #region 完成对用户角色分配
        public bool SetUserRole(int userId, List<int> roleIdList)
        {
            var userInfo = this.DbSession.UserInfoDal.LoadEntities(u=>u.ID==userId).FirstOrDefault();//查询当前用户
            if (userInfo != null)
            {
                userInfo.Role.Clear();//删除用户已经有的角色
                foreach (var roleId in roleIdList)
                {
                    var roleInfo = this.DbSession.RoleDal.LoadEntities(r=>r.ID==roleId).FirstOrDefault();//获取对应的角色信息
                    userInfo.Role.Add(roleInfo);//给当前用户分配角色
                }
            }
        
          return  this.DbSession.SaveChanges();
        }
        #endregion
     
       
    }
}
