using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// EditCart 的摘要说明
    /// </summary>
    public class EditCart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
           string action=context.Request["action"];
           if (action == "edit")
           {
               UpdateCart(context);
           }
           else if (action == "del")
           {
               DeleteCart(context);
           }
                
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="context"></param>
        private void DeleteCart(HttpContext context)
        {
            int cartId = Convert.ToInt32(context.Request["cartId"]);
            BLL.CartManager cartManager = new BLL.CartManager();
            cartManager.Delete(cartId);
            context.Response.Write("ok");
        }
        /// <summary>
        /// 更新商品的数量
        /// </summary>
        private void UpdateCart(HttpContext context)
        {
            int count = Convert.ToInt32(context.Request["count"]);
            int cartId = Convert.ToInt32(context.Request["cartId"]);
            BLL.CartManager cartManager = new BLL.CartManager();
            Model.Cart cartModel=cartManager.GetModel(cartId);
            cartModel.Count = count;
            cartManager.Update(cartModel);
            context.Response.Write("ok");
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