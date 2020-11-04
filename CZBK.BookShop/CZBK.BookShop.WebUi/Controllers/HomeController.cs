using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        IBLL.IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
           ViewData.Model=UserInfoService.LoadEntities(c=>true).ToList();
            return View();
        }

    }
}
