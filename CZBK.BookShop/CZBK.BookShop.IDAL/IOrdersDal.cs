using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IDAL
{
   public partial interface IOrdersDal:IBaseDal<Orders>
    {
       decimal GetTotalMoney(string orderId,string address,int userId);
    }
}
