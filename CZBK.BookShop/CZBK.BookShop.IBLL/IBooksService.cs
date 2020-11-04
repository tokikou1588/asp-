using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.IBLL
{
   public partial interface IBooksService:IBaseService<Books>
    {
       int GetPageCount(int pageSize);
       void CreateHtmlPage(int bookId);
       bool CheckBannedWord(string msg);
       bool CheckModWord(string msg);
       string ReplaceWord(string msg);
    }
}
