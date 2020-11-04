using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.BLL
{
    public partial class KeyWordsRankService : BaseService<KeyWordsRank>, IKeyWordsRankService
    {
        /// <summary>
        /// 删除汇总表中的数据
        /// </summary>
        /// <returns></returns>
        public bool DeleteKeyWords()
        {
            return this.DbSession.ExecuteSql("truncate table  KeyWordsRank") > 0;
        }

        /// <summary>
        /// 分组统计明细表中的数据插入到汇总表中。
        /// </summary>
        /// <returns></returns>
        public bool InsertKeyWords()
        {
            string sql = "insert into KeyWordsRank(Id,KeyWords, SearchCount) select newid(),KeyWords,count(*) from SearchDetails where DateDiff(day,SearchDateTime,getdate()) <=7 group by KeyWords";
            return this.DbSession.ExecuteSql(sql)>0;
        }

        /// <summary>
        /// 返回查询
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public List<string> GetSearchWord(string msg)
        {
            string sql = "select KeyWords from KeyWordsRank where KeyWords like @msg";
           return this.DbSession.ExecuteQuery<string>(sql, new System.Data.SqlClient.SqlParameter("@msg",msg+"%"));
        }
    }
}
