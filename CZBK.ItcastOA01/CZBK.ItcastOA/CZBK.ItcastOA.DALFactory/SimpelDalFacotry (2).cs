﻿ 

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IDAL.IActionInfoDal CreateActionInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
          
		   string fullClassName = NameSpace + ".ActionInfoDal";
		    var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IActionInfoDal;
        }
		
	    public static IDAL.IDepartmentDal CreateDepartmentDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".DepartmentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
          
		   string fullClassName = NameSpace + ".DepartmentDal";
		    var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IDepartmentDal;
        }
		
	    public static IDAL.IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".R_UserInfo_ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
          
		   string fullClassName = NameSpace + ".R_UserInfo_ActionInfoDal";
		    var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IR_UserInfo_ActionInfoDal;
        }
		
	    public static IDAL.IRoleInfoDal CreateRoleInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".RoleInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
          
		   string fullClassName = NameSpace + ".RoleInfoDal";
		    var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IRoleInfoDal;
        }
		
	    public static IDAL.IUserInfoDal CreateUserInfoDal()
        {

            //string classFulleName = ConfigurationManager.AppSettings["DALNameSpace"] + ".UserInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
          
		   string fullClassName = NameSpace + ".UserInfoDal";
		    var obj  = CreateInstance(fullClassName);


            return obj as IDAL.IUserInfoDal;
        }
	}
	
}