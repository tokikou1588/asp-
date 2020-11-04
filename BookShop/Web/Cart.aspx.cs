using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class Cart :Base.BasePage
    {
        public List<Model.Cart> CartList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.CartManager cartManager = new BLL.CartManager();
            int userId = ((Model.User)Session["userInfo"]).Id;
            CartList =cartManager.GetModelList("UserId=" + userId);
        }
    }
}