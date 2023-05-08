using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsApiProject.ViewModel
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int CatNewsNumber { get; set; }

    }
}