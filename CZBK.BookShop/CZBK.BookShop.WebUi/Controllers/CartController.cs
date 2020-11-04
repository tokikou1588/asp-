using CZBK.BookShop.Common;
using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        IBLL.IBooksService BookService { get; set; }
        IBLL.ICartService CartService { get; set; }
        IBLL.IUsersService UserService { get; set; }
        IBLL.IOrdersService OrderService { get; set; }
        #region 获取用户放在购物车中的商品
        public ActionResult Index()
        {
            Users LoginUser = null;
            List<Cart> cartList = null;
            //判断用户是否登录.
            if (CheckUserIsLogin(out LoginUser))
            {
                //判断Cookie是否存储购物车中商品.
                if (Request.Cookies["cart"] != null)
                {
                    //将Cookie中的数据添加到数据库中，并且删除Cookie
                    foreach (string key in Request.Cookies["cart"].Values)
                    {
                        Cart cart = new Cart();
                        cart.UserId = LoginUser.Id;
                        cart.Count = Convert.ToInt32(Request.Cookies["cart"].Values[key].Split(new char[] { '|' })[1]);
                        cart.BookId = Convert.ToInt32(key);
                        cart.Books = BookService.LoadEntities(b => b.Id == cart.BookId).FirstOrDefault();
                        Cart cartModel = CartService.LoadEntities(c => c.UserId == cart.UserId && c.BookId == cart.BookId).FirstOrDefault();
                        if (cartModel != null)
                        {
                            cartModel.Count = cartModel.Count + cart.Count;
                            CartService.UpdateEntity(cartModel);
                        }
                        else
                        {
                            CartService.AddEntity(cart);
                        }
                    }
                    //删除Cookie
                    Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);
                }
                //不管Cookie中是否有值，都是查询数据库获取购物车商品项
                cartList = CartService.LoadEntities(c => c.UserId == LoginUser.Id).ToList();
            }
            else//表示用户没有登录
            {
                if (Request.Cookies["cart"] != null)
                {
                    cartList = new List<Cart>();
                    foreach (string key in Request.Cookies["cart"].Values)
                    {
                        Cart cart = new Cart();
                        cart.BookId = Convert.ToInt32(key);
                        cart.Count = Convert.ToInt32(Request.Cookies["cart"].Values[key].Split(new char[] { '|' })[1]);
                        cart.Books = BookService.LoadEntities(b => b.Id == cart.BookId).FirstOrDefault();
                        
                        cartList.Add(cart);
                    }
                }
            }
            ViewData["cartList"] = cartList;

            return View();
        }
        #endregion

        #region 向购物车中添加商品
        public ActionResult AddCart()
        {
            int bookId;
            if (int.TryParse(Request["bookId"], out bookId))//判断书的编号是否为整数
            {
                //判断该商品是否存在
                var bookModel = BookService.LoadEntities(b => b.Id == bookId).FirstOrDefault();
                if (bookModel != null)
                {
                    //判断用户是否登录.
                    Users user = null;
                    if (CheckUserIsLogin(out user))//用户登录
                    {
                        //判断Cookie是否有值(有可能用户在没有登录时已经选择了商品，这些商品放在了Cookie中)
                        if (Request.Cookies["cart"] == null)
                        {
                            //将商品信息保存到数据库中，但是要考虑到以前用户是否选择了该商品。
                            var cartModel = CartService.LoadEntities(c => c.UserId == user.Id && c.BookId == bookId).FirstOrDefault();
                            if (cartModel != null)
                            {
                                cartModel.Count = cartModel.Count + 1;
                                CartService.UpdateEntity(cartModel);
                            }
                            else
                            {
                                Cart cart = new Cart();
                                cart.BookId = bookId;
                                cart.UserId = user.Id;
                                cart.Count = 1;
                                CartService.AddEntity(cart);
                            }
                        }
                        else//表示Cookie中有值
                        {
                            //将Cookie中的数据取出来写到数据库中，然后再将用选择的商品添加到数据库中
                            foreach (string key in Request.Cookies["cart"].Values)
                            {
                                int bId = Convert.ToInt32((Request.Cookies["cart"].Values[key].Split(new char[] { '|' })[0]));//Cookie的键是商品编号
                                int count = Convert.ToInt32(Request.Cookies["cart"].Values[key].Split(new char[] { '|' })[1]);//Cookie的值是商品编号 +"|"+商品数量
                                var cartModel = CartService.LoadEntities(c => c.BookId == bId && c.UserId == user.Id).FirstOrDefault();
                                if (cartModel != null)
                                {
                                    cartModel.Count = cartModel.Count + count;
                                    CartService.UpdateEntity(cartModel);
                                }
                                else
                                {
                                    Cart cart = new Cart();
                                    cart.BookId = bId;
                                    cart.Count = count;
                                    cart.UserId = user.Id;
                                    CartService.AddEntity(cart);
                                }
                            }
                            //删除Cookie。
                            Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);
                            //将用户选择的商品加入到数据库中。
                            var modelCart = CartService.LoadEntities(c => c.UserId == user.Id && c.BookId == bookId).FirstOrDefault();
                            if (modelCart != null)
                            {
                                modelCart.Count = modelCart.Count + 1;
                                CartService.UpdateEntity(modelCart);
                            }
                            else
                            {
                                Cart cart = new Cart();
                                cart.BookId = bookId;
                                cart.UserId = user.Id;
                                cart.Count = 1;
                                CartService.AddEntity(cart);

                            }
                        }
                    }
                    else//用户没有登录
                    {
                        //将商品的信息写到Cookie中.也要判断一下Cookie中以前是否存有该商品.
                        bool isExt = false;
                        string itemKey = string.Empty;
                        HttpCookie cartCookie = null;
                        if (Request.Cookies["cart"] == null)
                        {
                            cartCookie = new HttpCookie("cart");
                        }
                        else
                        {
                            //表示以前已经有该Cookie,看一下Cookie中存储的商品是否存在当前用户选择的商品.
                            cartCookie = Request.Cookies["cart"];
                            foreach (string key in cartCookie.Values)
                            {
                                if (bookId == Convert.ToInt32(key))
                                {
                                    isExt = true;
                                    itemKey = key;
                                    break;
                                }
                            }

                        }
                        if (isExt)//表示Cookie已经存有该商品
                        {
                            int count = Convert.ToInt32(cartCookie.Values[itemKey].Split(new char[] { '|' })[1]);
                            count = count + 1;
                            cartCookie.Values.Remove(itemKey);
                            cartCookie.Values.Add(itemKey, itemKey + "|" + count);
                        }
                        else
                        {
                            cartCookie.Values.Add(bookId.ToString(), bookId + "|" + 1);

                        }
                        cartCookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cartCookie);
                    }

                }
                else
                {
                    return Content("no:没有此商品");
                }
            }
            else
            {
                return Content("no:参数错误!!");
            }
            return Content("ok:添加成功");
        }
        #endregion

        #region 判断用户是否登录.
        private bool CheckUserIsLogin(out Users user)
        {
            bool isExt = false;
            user = null;
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj = Common.MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                    user = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());
                    isExt = true;

                }
            }
           else  //问题:如果用户在第一次登陆时选择记住我，并且向购物车中添加商品，这时商品保存到数据中。然后用户关闭了浏览器，然后下次再访问但是直接输入的地址是购物车的地址这时商品并没有立即显示，因为自己创建的Request.Cookies["sessionId"]，所以解决方法是，在该方法中校验cp1,cp2Cookie的值，最后Cart的购物车视图页面不要再套用我们这个模板页。
            {
                if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
                {
                    string name = Request.Cookies["cp1"].Value;
                    string pwd = Request.Cookies["cp2"].Value;
                  
               
                    var user2 = UserService.LoadEntities(u => u.LoginId == name).FirstOrDefault();
                    if (user2 != null)
                    {
                        if (pwd == WebCommon.Md5String(WebCommon.Md5String(user2.LoginPwd)))
                        {
                            // Session["userInfo"] = user;
                            user = user2;
                            string sessionId = Guid.NewGuid().ToString();
                            MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(user2), DateTime.Now.AddMinutes(20));
                            Response.Cookies["sessionId"].Value = sessionId;
                            isExt = true;
                        }
                    }

               }
            }
            return isExt;
        }
        #endregion

        #region 更新购物车中商品的数量
        public ActionResult EditCart()
        {
            int count = int.Parse(Request["count"]);
            int bookId = int.Parse(Request["bookId"]);
            int pkId = int.Parse(Request["pkId"]);
            Users user=null;
            if (CheckUserIsLogin(out user))
            {
                //更新数据库中商品的数量.
             var cartModel= CartService.LoadEntities(c=>c.BookId==bookId&&c.UserId==user.Id).FirstOrDefault();
             cartModel.Count = count;
             CartService.UpdateEntity(cartModel);
             return Content("ok");
            }
            else
            {
                HttpCookie cookieCart = Request.Cookies["cart"];
                cookieCart.Values.Remove(bookId.ToString());
                cookieCart.Values.Add(bookId.ToString(), bookId.ToString() + "|" + count);
                Response.Cookies.Add(cookieCart);
                return Content("ok");
            }
        }
        #endregion

        #region 删除购物车中的商品
        public ActionResult DeleteCart()
        {
            int bookId = int.Parse(Request["bookId"]);
            int pkId = int.Parse(Request["pkId"]);
            Users user = null;
            if (CheckUserIsLogin(out user))
            {
              var cartModel=CartService.LoadEntities(c=>c.Id==pkId).FirstOrDefault();
              CartService.DeleteEntity(cartModel);
            }
            else
            {
                HttpCookie cookieCart = Request.Cookies["cart"];
                cookieCart.Values.Remove(bookId.ToString());
                if (cookieCart.Values.Count < 1)
                {
                    cookieCart.Expires = DateTime.Now.AddDays(-1);
                }
                Response.Cookies.Add(cookieCart);
            }
            return Content("ok");
        }
        #endregion

        #region 订单确认
        public ActionResult OrderConfirm()
        {
            Users user = null;
            if (CheckUserIsLogin(out user))
            {
                //判断cart 这个Cookie中是否有值，如果有值，将Cookie中的数据插入到Cart表中。（注意封装）(如果用户在匿名状态向购物车中添加了商品，这时商品的信息是存储在Cookie中，然后单击了结算，但是OrderConfirm这个方法必须登录以后才能访问，所以跳转到登录页面，登录成功以后，跳转到 OrderConfirm订单确认页面，该页面中首先要展示用户放在购物车中的商品信息，所以这里要判断Cookie）
                if (Request.Cookies["cart"] != null)
                {
                    //将Cookie中的数据添加到数据库中，并且删除Cookie
                    foreach (string key in Request.Cookies["cart"].Values)
                    {
                        Cart cart = new Cart();
                        cart.UserId = user.Id;
                        cart.Count = Convert.ToInt32(Request.Cookies["cart"].Values[key].Split(new char[] { '|' })[1]);
                        cart.BookId = Convert.ToInt32(key);
                        cart.Books = BookService.LoadEntities(b => b.Id == cart.BookId).FirstOrDefault();
                        Cart cartModel = CartService.LoadEntities(c => c.UserId == cart.UserId && c.BookId == cart.BookId).FirstOrDefault();
                        if (cartModel != null)
                        {
                            cartModel.Count = cartModel.Count + cart.Count;
                            CartService.UpdateEntity(cartModel);
                        }
                        else
                        {
                            CartService.AddEntity(cart);
                        }
                    }
                    //删除Cookie
                    Response.Cookies["cart"].Expires = DateTime.Now.AddDays(-1);

                }
                List<Cart> cartList = CartService.LoadEntities(c => c.UserId == user.Id).ToList();
                if (cartList.Count < 1)//购物车中没有商品
                {
                    return RedirectToAction("/BookList/Index");
                }
                else
                {
                  StringBuilder sb=new StringBuilder();
                    decimal totleMoney=0;
                    foreach(var cart in cartList)
                    {
                         sb.Append("<tr class=\"align_Center\">");
                  sb.Append("<td style=\"PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 5px\">"+cart.BookId+"</td>");
                  sb.Append("<td class=align_Left><a onmouseover=\"\" onmouseout=\"\" onclick=\"\" href='#' target=\"_blank\" >"+cart.Books.Title+"</a></td>");
                      
                  sb.Append("<td><span class=\"price\">￥"+cart.Books.UnitPrice+"</span></td>");
                  sb.Append("<td>"+cart.Count+"</td>    </tr>");
                  totleMoney=totleMoney+(cart.Count*cart.Books.UnitPrice);
                    }
                    ViewData["totleMoney"]=totleMoney.ToString("0.00");
                    ViewData["str"]=sb.ToString();
                    ViewData["userName"] = user.LoginId;
                    ViewData["address"] = user.Address;
                    ViewData["phone"] = user.Phone;
                

                    return View();
                }
            }
            else//没有登录
            {
                return Redirect("/Login/Index/?returnUrl="+Request.Url.ToString());
            }
          

        }


        public ActionResult CreateOrder()
        {
            Users user=null;
            if (CheckUserIsLogin(out user))
            {
                int userId = user.Id;
                string orderId = DateTime.Now.ToString("yyyyMMddHHmmssfff")+userId;
                //收件人:itcast,联系电话:123,地址:ddd,邮编:3333366
                string address = string.Format("收件人:{0},联系电话:{1},地址:{2},邮编:{3}", Request["txtName"], Request["txtPhone"], Request["txtAddress"], Request["txtPostCode"]);
               decimal totalMoney=OrderService.GetTotalMoney(orderId, address, userId);
                //将相关的数据发送到支付宝。

               AliPay.Pay pay = new AliPay.Pay("图书", "图书商城", orderId, totalMoney);
               string payUrl = pay.GoPay();
               return Redirect(payUrl);
            }
            else
            {
                return Redirect("/Login/Index/?returnUrl=/Cart/OrderConfirm");
            }
            
        }
        #endregion

    }
}
