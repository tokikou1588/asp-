using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ProcessCart 的摘要说明
    /// </summary>
    public class ProcessCart : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int id = Convert.ToInt32(context.Request["bookId"]);
            BLL.BookManager bookManager = new BLL.BookManager();
            Model.Book bookModel = bookManager.GetModel(id);
            if (bookModel != null)
            {
                //判断用户是否选择了该商品。
                int userId = ((Model.User)context.Session["userInfo"]).Id;
                BLL.CartManager cartManager = new BLL.CartManager();
                Model.Cart cartModel=cartManager.GetModel(userId, id);//判断用户是否选择了某个商品.
                if (cartModel != null)//如果有，更新数量
                {
                    cartModel.Count = cartModel.Count + 1;
                    cartManager.Update(cartModel);
                }
                else
                {
                    //没有则添加，数量为1.
                    Model.Cart modelCart = new Model.Cart();
                    modelCart.Count = 1;
                    modelCart.Book = bookModel;
                    modelCart.User = (Model.User)context.Session["userInfo"];
                    cartManager.Add(modelCart);

                }
                context.Response.Write("ok:加入购物车成功");
            }
            else
            {
                context.Response.Write("no:没有此商品");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}