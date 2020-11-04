using CZBK.BookShop.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.DALFactory
{
   public partial class AbstractFactory
    {
       //private readonly static string DalAssembly = ConfigurationManager.AppSettings["DalAssembly"];
       //private readonly static string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];
       //public static IUserInfoDal CreateUserInfoDal()
       //{
       //    string fullClassName = DalNameSpace + ".UserInfoDal";
       //   return CreateInstance(fullClassName,DalAssembly) as IUserInfoDal;
       //}
       public static object CreateInstance(string assemblyPath,string fullClassName)
       {
        var assembly=Assembly.Load(assemblyPath);
        return assembly.CreateInstance(fullClassName);
       }

    }
}
