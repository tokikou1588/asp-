using CZBK.BookShop.IDAL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.DAL
{
   public partial class OrdersDal:BaseDal<Orders>,IOrdersDal
    {
       /// <summary>
       /// 执行存储完成下订单
       /// </summary>
       /// <param name="orderId"></param>
       /// <param name="address"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
        public decimal GetTotalMoney(string orderId, string address, int userId)
        {
            book_shopEntities db = (book_shopEntities)DbContextFactory.CreateCurrentDbContext();
            var toalMoney = 0;
            var par = new System.Data.Objects.ObjectParameter("totalMoney", toalMoney);
            db.CreateOrder(orderId, userId, address, par);
            return Convert.ToDecimal(par.Value);
        }
    }
}
