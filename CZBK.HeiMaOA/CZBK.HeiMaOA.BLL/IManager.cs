 

using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }   
	
	public partial class BooksService :BaseService<Books>,IBooksService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.BooksDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }   
	
	public partial class FileInfoService :BaseService<FileInfo>,IFileInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.FileInfoDal;
        }
    }   
	
	public partial class KeyWordsRankService :BaseService<KeyWordsRank>,IKeyWordsRankService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.KeyWordsRankDal;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoDal;
        }
    }   
	
	public partial class SearchDetailsService :BaseService<SearchDetails>,ISearchDetailsService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.SearchDetailsDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }   
	
	public partial class WF_InstanceService :BaseService<WF_Instance>,IWF_InstanceService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.WF_InstanceDal;
        }
    }   
	
	public partial class WF_StepInfoService :BaseService<WF_StepInfo>,IWF_StepInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.WF_StepInfoDal;
        }
    }   
	
	public partial class WF_TempService :BaseService<WF_Temp>,IWF_TempService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.WF_TempDal;
        }
    }   
	
}