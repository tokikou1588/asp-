﻿using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager
{
    public class AdminManagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
          
            context.MapRoute(
                "AdminManager_default",
                "AdminManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}
