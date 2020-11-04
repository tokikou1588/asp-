using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface ISettingsService:IBaseService<Settings>
    {
       string GetValue(string name);
    }
}
