using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.Model.SearchParams
{
   public class UserInfoSearch:BaseSearch
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
