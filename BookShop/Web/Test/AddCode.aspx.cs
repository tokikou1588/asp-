using BookShop.BLL;
using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Test
{
    public partial class AddCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string msg = Request["txtCode"];
                msg=msg.Trim();
                string[]words=msg.Split(new char[]{'\r','\n'},StringSplitOptions.RemoveEmptyEntries);
                Articel_WordsManager bll = new Articel_WordsManager();
                foreach (string word in words)
                {
                    string[]w=word.Split('=');
                    Articel_Words model = new Articel_Words();
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
                    bll.Add(model);
                }
               
            }
        }
    }
}