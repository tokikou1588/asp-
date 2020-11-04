using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
    public partial class OrdersService : BaseService<Orders>, IOrdersService
    {
        public decimal GetTotalMoney(string orderId, string address, int userId)
        {
            return this.DbSession.OrdersDal.GetTotalMoney(orderId, address, userId);
        }
    }
}
