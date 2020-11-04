using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminWordController : Controller
    {
        //
        // GET: /AdminManager/AdminWord/
        IBLL.IArticel_WordsService Articel_WordService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        #region 添加词库
        public ActionResult AddCode()
        {
            string msg = Request["txtCode"];
            msg = msg.Trim();
           string[]words= msg.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
           foreach (string word in words)
           {
               string[]w= word.Split('=');
               Model.Articel_Words model = new Model.Articel_Words();
               model.WordPattern = w[0];
               if (w[1] == "{BANNED}")
               {
                   model.IsForbid = true;
               }
               else if (w[1] == "{MOD}")
               {
                   model.IsMod = true;
               }
               else
               {
                   model.ReplaceWord = w[1];
               }
               Articel_WordService.AddEntity(model);
           }
           return Content("ok");
        }
        #endregion

    }
}
