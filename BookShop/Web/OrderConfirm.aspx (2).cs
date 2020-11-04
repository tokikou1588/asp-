using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class OrderConfirm :Base.BasePage
    {
        public Model.User CurrentUserInfo { get; set; }
        public string StrHtml { get; set; }
        public decimal TotalMoney { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUserInfo=(Model.User)Session["userInfo"];
            if (!IsPostBack)
            {
                BindCartList();
            }
            else
            {
                CreateOrderAndPay();
            }

        }
        /// <summary>
        /// 下订单并且支付
        /// </summary>
        private void CreateOrderAndPay()
        {
            string orderId = DateTime.Now.ToString("yyyyMMddHHmmssfff") + CurrentUserInfo.Id;//订单号
            //收货人:adminsdfd,联系电话:11223333,地址:sadf,邮编:88899
            string address = string.Format("收货人:{0},联系电话:{1},地址:{2},邮编:{3}", Request["txtName"], Request["txtPhone"], Request["txtAddress"], Request["txtPostCode"]);
            BLL.OrdersManager orderManager = new BLL.OrdersManager();
            //调用存储过程，完成下订单。返回的是总金额。
            decimal totalMoney=orderManager.CreateOrder(CurrentUserInfo.Id, address, orderId);
            AliPay.Pay pay = new AliPay.Pay("图书","网上图书",orderId,totalMoney);
           string url= pay.GoPay();
           Response.Redirect(url);
        }

        private void BindCartList()
        {
            BLL.CartManager cartManger = new BLL.CartManager();
           List<Model.Cart>list= cartManger.GetModelList("UserId="+CurrentUserInfo.Id);
           if (list.Count < 1)
           {
               Response.Redirect("/BookList.aspx");
           }
           else
           {
               StringBuilder sb=new StringBuilder();
               decimal totalMoney = 0;
               foreach (Model.Cart cartModel in list)
               {
                      sb.Append("<tr class=\"align_Center\">");
                  sb.Append("<td style=\"PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px\">"+cartModel.Book.Id+"</td>");
                  sb.Append("<td class=align_Left><a onmouseover=\"\" onmouseout=\"\" onclick=\"\" href='#' target=\"_blank\" >"+cartModel.Book.Title+"</a>  </td>");
                    
                  sb.Append("<td><span class=\"price\">￥"+cartModel.Book.UnitPrice.ToString("0.00")+"</span></td>");
                  sb.Append("<td>"+cartModel.Count+"</td> </tr>");
              totalMoney=totalMoney+(cartModel.Book.UnitPrice*cartModel.Count);
            
               
               }
               StrHtml = sb.ToString();
               TotalMoney = totalMoney;
           }

        }
    }
}