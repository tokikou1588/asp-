﻿using CZBK.BookShop.WebUi.Models;
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

namespace CZBK.BookShop.WebUi.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        IBLL.IBooksService BookService { get; set; }
        IBLL.ISearchDetailsService SearchDetailService { get; set; }
        IBLL.IkeyWordsRankService KeyWordRankService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchContent()
        {
            //如果表单中有多个submit，只会将单击的的submit的value值提交到服务端.
            if (!string.IsNullOrEmpty(Request["btnSearch"]))
            {
                List<ViewSearchResult>list=SearchIndexContent();
                ViewData["resultList"] = list;
                return View("Index");
            }
            else 
            {
                //创建
                CreateSearchIndex();
            }
            return Content("ok");
        }
        /// <summary>
        /// 搜索
        /// </summary>
        public List<ViewSearchResult> SearchIndexContent()
        {
            string indexPath = @"C:\lucenedir";//写到配置文件中。
            string keyWord=Request["txtSearchContent"];
            string[] kw = Common.WebCommon.PanGuSplitWord(keyWord);//对用户输入的搜索条件进行分词.
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            PhraseQuery query = new PhraseQuery();//指定搜索条件
            foreach (string word in kw)//先用空格，让用户去分词，空格分隔的就是词“计算机   专业”
            {
                query.Add(new Term("content", word));
            }
            //query.Add(new Term("body","语言"));--可以添加查询条件，两者是add关系.顺序没有关系.
            //   query.Add(new Term("body", "大学生"));
            //query.Add(new Term("body", kw));//body中含有kw的文章
            query.SetSlop(100);//多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);//根据query查询条件进行查询，查询结果放入collector容器
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;//得到所有查询结果中的文档,GetTotalHits():表示总条数   TopDocs(300, 20);//表示得到300（从300开始），到320（结束）的文档内容.
            //可以用来实现分页功能
            List<ViewSearchResult> searchResutList = new List<ViewSearchResult>();
            for (int i = 0; i < docs.Length; i++)
            {
                //
                //搜索ScoreDoc[]只能获得文档的id,这样不会把查询结果的Document一次性加载到内存中。降低了内存压力，需要获得文档的详细内容的时候通过searcher.Doc来根据文档id来获得文档的详细内容对象Document.
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//找到文档id对应的文档详细信息
                ViewSearchResult result = new ViewSearchResult();
               result.Id=int.Parse(doc.Get("id"));// 取出放进字段的值
              result.Title=doc.Get("title");
              result.Content = Common.WebCommon.CreateHightLight(keyWord, doc.Get("content"));
              result.PublishDate = doc.Get("publishDate");
              searchResutList.Add(result);
            }

            //向明细表中插入搜索的数据.
            Model.SearchDetails model = new Model.SearchDetails();
            model.Id = Guid.NewGuid();
            model.KeyWords = keyWord;
            model.SearchDateTime = DateTime.Now;
            SearchDetailService.AddEntity(model);

            return searchResutList;
        }


        /// <summary>
        /// 创建索引库
        /// </summary>
        public void CreateSearchIndex()
        {
           
        }

        public ActionResult GetKeyWord()
        {
            string msg = Request["term"];
           List<string>list= KeyWordRankService.GetKeyWord(msg);
           return Content(Common.SerializeHelper.SerializeToString(list.ToArray()));
        }

    }
}
