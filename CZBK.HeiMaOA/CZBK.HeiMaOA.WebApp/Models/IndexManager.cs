﻿using CZBK.HeiMaOA.Model.Enum;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace CZBK.HeiMaOA.WebApp.Models
{
    public sealed class IndexManager
    {
        private static readonly IndexManager indexManager = new IndexManager();
        private IndexManager()
        {
        }
        public static IndexManager GetInstance()
        {
            return indexManager;
        }
        Queue<IndexContent> queue = new Queue<IndexContent>();
        /// <summary>
        /// 调用该方法，向队列中添加数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public  void AddOrEditQueue(string id,string title,string content)
        {
            IndexContent indexContent = new IndexContent();
            indexContent.Content = content;
            indexContent.Id = id;
            indexContent.Title = title;
            indexContent.LuceneEnumType = LuceneEnumType.Add;
            queue.Enqueue(indexContent);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public  void DeleteQueue(string id)
        {
            IndexContent indexContent = new IndexContent();
            indexContent.Id = id;
            indexContent.LuceneEnumType = LuceneEnumType.Delete;
            queue.Enqueue(indexContent);
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        public  void CreateThread()
        {
            Thread myThread = new Thread(CreateSearchIndex);
            myThread.IsBackground = true;
            myThread.Start();
        }
        private  void CreateSearchIndex()
        {
         
                while (true)
                {
                    if (queue.Count > 0)
                    {
                        try
                        {
                            WriteSearchContent();
                        }
                        catch
                        {
                            //写到日志中。
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
        
         
        }
        private  void WriteSearchContent()
        {
            string indexPath = @"C:\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。
            while (queue.Count > 0)
            {
              IndexContent indexContent =queue.Dequeue();//出队
              writer.DeleteDocuments(new Term("id", indexContent.Id.ToString()));
              if (indexContent.LuceneEnumType == LuceneEnumType.Delete)
              {
                  continue;
              }
                Document document = new Document();//表示一篇文档。

                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("id", indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("title", indexContent.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                document.Add(new Field("content", indexContent.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                writer.AddDocument(document);
            }

            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }

    }
}