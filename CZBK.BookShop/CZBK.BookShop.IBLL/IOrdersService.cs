using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface IOrdersService:IBaseService<Orders>
    {
       decimal GetTotalMoney(string orderId, string address, int userId);
    }
}
