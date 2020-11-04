 

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
      
   
		
	    public static IActionGroupDal CreateActionGroupDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".ActionGroupDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IActionGroupDal;
        }
		
	    public static IActionInfoDal CreateActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".ActionInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IActionInfoDal;
        }
		
	    public static IArticel_WordsDal CreateArticel_WordsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".Articel_WordsDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IArticel_WordsDal;
        }
		
	    public static IBookCommentDal CreateBookCommentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".BookCommentDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IBookCommentDal;
        }
		
	    public static IBooksDal CreateBooksDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".BooksDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IBooksDal;
        }
		
	    public static ICartDal CreateCartDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".CartDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ICartDal;
        }
		
	    public static ICategoriesDal CreateCategoriesDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".CategoriesDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ICategoriesDal;
        }
		
	    public static ICheckEmailDal CreateCheckEmailDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".CheckEmailDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ICheckEmailDal;
        }
		
	    public static IDepartmentDal CreateDepartmentDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".DepartmentDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IDepartmentDal;
        }
		
	    public static IkeyWordsRankDal CreatekeyWordsRankDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".keyWordsRankDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IkeyWordsRankDal;
        }
		
	    public static IOrderBookDal CreateOrderBookDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".OrderBookDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IOrderBookDal;
        }
		
	    public static IOrdersDal CreateOrdersDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".OrdersDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IOrdersDal;
        }
		
	    public static IPublishersDal CreatePublishersDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".PublishersDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IPublishersDal;
        }
		
	    public static IR_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".R_UserInfo_ActionInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IR_UserInfo_ActionInfoDal;
        }
		
	    public static IRoleDal CreateRoleDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".RoleDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IRoleDal;
        }
		
	    public static ISearchDetailsDal CreateSearchDetailsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".SearchDetailsDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ISearchDetailsDal;
        }
		
	    public static ISettingsDal CreateSettingsDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".SettingsDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ISettingsDal;
        }
		
	    public static ISysFunDal CreateSysFunDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".SysFunDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as ISysFunDal;
        }
		
	    public static IUserInfoDal CreateUserInfoDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UserInfoDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IUserInfoDal;
        }
		
	    public static IUsersDal CreateUsersDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UsersDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IUsersDal;
        }
		
	    public static IUserStatesDal CreateUserStatesDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".UserStatesDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IUserStatesDal;
        }
		
	    public static IVidoFileDal CreateVidoFileDal()
        {

            string classFulleName = ConfigurationManager.AppSettings["DalNameSpace"] + ".VidoFileDal";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            var obj  = CreateInstance(ConfigurationManager.AppSettings["DalAssembly"], classFulleName);


            return obj as IVidoFileDal;
        }
	}
	
}