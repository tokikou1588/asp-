﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CZBK.HeiMaOA.WebApp.Models
{
    public class SearchResultViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }
}