using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZBK.BookShop.WebUi.Models
{
    public class ViewSearchResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishDate { get; set; }
        public EnumType EnumType { get; set; }
    }
}