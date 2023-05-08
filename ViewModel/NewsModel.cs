using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApiProject.ViewModel
{
    public class NewsModel
    {
        public int NewsId { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public System.DateTime Date { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ReadNumber { get; set; }
        public int MemberId { get; set; }

        public string NickName { get; set; }

    }
}