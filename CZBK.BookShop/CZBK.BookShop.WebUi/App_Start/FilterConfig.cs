using CZBK.BookShop.WebUi.Models;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());
        }
    }
}