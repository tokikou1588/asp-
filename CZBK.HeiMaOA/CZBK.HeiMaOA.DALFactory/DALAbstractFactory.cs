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
    /// <summary>
    /// 抽象工厂：都是解决对象的创建问题。（抽象工厂是通过反射的方式创建类的实例.）
    /// </summary>
   public partial class DALAbstractFactory
    {
       //private static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];//获取命名空间.
     //  private static readonly string DalAssembly = ConfigurationManager.AppSettings["DalAssembly"];
       //public static IUserInfoDal CreateUserInfoDal()
       //{
       //    string fullClassName = DalNameSpace + ".UserInfoDal";//构建类的全名称.
       //  return  CreateInstance(fullClassName,DalAssembly) as IUserInfoDal;
       //}
   
       
       private static object   CreateInstance(string fullClassName,string assemblyPath)
       {
           var assembly=Assembly.Load(assemblyPath);//加载程序集.
          return  assembly.CreateInstance(fullClassName);
       }
    }
}
