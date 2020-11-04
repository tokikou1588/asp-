using CZBK.BookShop.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
    public partial class keyWordsRankService : BaseService<Model.keyWordsRank>, IkeyWordsRankService
    {
        /// <summary>
        /// 删除所有的汇总表中的数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
          return  this.DbSession.ExecuteSql("truncate table keyWordsRank")>0;
        }
        /// <summary>
        /// 统计明细表中的数据插入到汇总表中。
        /// </summary>
        /// <returns></returns>
        public bool InsertKeyWordRank()
        {
            string sql = "insert into keyWordsRank(Id,KeyWords,SearchTimes) select newid(),KeyWords, count(*) from SearchDetails where DateDiff(day,SearchDateTime,getdate())<=7 group by KeyWords";
             return  this.DbSession.ExecuteSql(sql)>0;
        }
        public List<string> GetKeyWord(string msg)
        {
            string sql = "select KeyWords from keyWordsRank where KeyWords like @msg";
          return  this.DbSession.ExecuteSelectSql<string>(sql, new System.Data.SqlClient.SqlParameter("@msg",msg+"%"));

        }
    }
}
