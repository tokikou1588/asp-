using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.IBLL
{
   public partial interface IKeyWordsRankService:IBaseService<KeyWordsRank>
    {
       bool DeleteKeyWords();
       bool InsertKeyWords();
       List<string> GetSearchWord(string msg);
    }
}
