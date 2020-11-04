using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface IkeyWordsRankService
    {
       bool DeleteAll();
       bool InsertKeyWordRank();
       List<string> GetKeyWord(string msg);
    }

}
