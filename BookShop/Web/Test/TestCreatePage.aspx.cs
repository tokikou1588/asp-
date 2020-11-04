using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Test
{
    public partial class TestCreatePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BLL.BookManager bll = new BLL.BookManager();
                List<Model.Book>list=bll.GetModelList("");
                foreach (Model.Book bookModel in list)
                {
                    bll.CreateHtmlPage(bookModel.Id);
                }
            }
        }
    }
}