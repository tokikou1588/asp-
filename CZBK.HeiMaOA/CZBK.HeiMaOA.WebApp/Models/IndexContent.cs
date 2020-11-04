using CZBK.HeiMaOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZBK.HeiMaOA.WebApp.Models
{
    public class IndexContent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LuceneEnumType LuceneEnumType { get; set; }
    }
}