using CZBK.HeiMaOA.Model;
using CZBK.HeiMaOA.WebApp.Models;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        IBLL.IBooksService bookService { get; set; }
        IBLL.ISearchDetailsService searchDetailService { get; set; }
        IBLL.IKeyWordsRankService keywordService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchContent()
        {
            if (!string.IsNullOrEmpty(Request["btnSearch"]))
            {
               List<SearchResultViewModel> list =SearchBookContent();
               ViewData["list"] = list;
               return View("Index");
            }
            else
            {
                CreateContent();
            }
            return Content("OK");
        }
        /// <summary>
        /// 搜索
        /// </summary>
        public List<SearchResultViewModel> SearchBookContent()
        {
            string indexPath = @"C:\lucenedir";
            string kw = Request["txtSearchContent"];
            string[] keywords = Common.WebCommon.PanGuSplitWord(kw);
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            PhraseQuery query = new PhraseQuery();
            foreach (string word in keywords)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("content", word));
            }
            //query.Add(new Term("body","语言"));--可以添加查询条件，两者是add关系.顺序没有关系.
            // query.Add(new Term("body", "大学生"));
         //   query.Add(new Term("content", kw));//body中含有kw的文章
            query.SetSlop(100);//多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);//根据query查询条件进行查询，查询结果放入collector容器
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;//得到所有查询结果中的文档,GetTotalHits():表示总条数   TopDocs(300, 20);//表示得到300（从300开始），到320（结束）的文档内容.
            //可以用来实现分页功能
            List<SearchResultViewModel> searchResultList = new List<SearchResultViewModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                //
                //搜索ScoreDoc[]只能获得文档的id,这样不会把查询结果的Document一次性加载到内存中。降低了内存压力，需要获得文档的详细内容的时候通过searcher.Doc来根据文档id来获得文档的详细内容对象Document.
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//找到文档id对应的文档详细信息
                SearchResultViewModel viewModel = new SearchResultViewModel();
                viewModel.Id = int.Parse(doc.Get("id"));
                viewModel.Title = doc.Get("title");
                viewModel.Url = "/Book/ShowDetail/?id=" + viewModel.Id;
                viewModel.Content =Common.WebCommon.CreateHightLight(kw, doc.Get("content"));
                searchResultList.Add(viewModel);
            }

            SearchDetails searchDetail = new SearchDetails();
            searchDetail.KeyWords = kw;
            searchDetail.Id = Guid.NewGuid();
            searchDetail.SearchDateTime = DateTime.Now;
            searchDetailService.AddEntity(searchDetail);
            return searchResultList;

        }
        /// <summary>
        /// 创建索引库
        /// </summary>
        public void CreateContent()
        {
           
        }

        #region 获取热词
        public ActionResult GetSarch()
        {
            string term = Request["term"];
           List<string>list= keywordService.GetSearchWord(term);
           return Json(list.ToArray(),JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
