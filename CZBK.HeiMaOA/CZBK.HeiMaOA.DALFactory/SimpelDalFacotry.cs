 

using CZBK.HeiMaOA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.DALFactory
{
    public partial class DALAbstractFactory
    {
      
   
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".ActionInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IActionInfoDal;
        }
		
	    public static IBooksDal CreateBooksDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".BooksDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IBooksDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".DepartmentDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IDepartmentDal;
        }
		
	    public static IFileInfoDal CreateFileInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".FileInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IFileInfoDal;
        }
		
	    public static IKeyWordsRankDal CreateKeyWordsRankDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".KeyWordsRankDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IKeyWordsRankDal;
        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".R_UserInfo_ActionInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IR_UserInfo_ActionInfoDal;
        }
		
	    public static IRoleInfoDal CreateRoleInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".RoleInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IRoleInfoDal;
        }
		
	    public static ISearchDetailsDal CreateSearchDetailsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".SearchDetailsDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as ISearchDetailsDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UserInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IUserInfoDal;
        }
		
	    public static IWF_InstanceDal CreateWF_InstanceDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".WF_InstanceDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IWF_InstanceDal;
        }
		
	    public static IWF_StepInfoDal CreateWF_StepInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".WF_StepInfoDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IWF_StepInfoDal;
        }
		
	    public static IWF_TempDal CreateWF_TempDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".WF_TempDal";

		
            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(classFulleName,ConfigurationManager.AppSettings["DalAssembly"]);


            return obj as IWF_TempDal;
        }
	}
	
}