using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class HomeController : BaseController //Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.UserName = LoginUser.UName;
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }

    }
}
