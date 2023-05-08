using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApiProject.ViewModel
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int MemberId { get; set; }
        public int NewsId { get; set; }
        public System.DateTime Date { get; set; }
        public string UsersName { get; set; }
        public string NewsHeadline { get; set; }

    }
}