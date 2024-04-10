using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BE_WebAPI.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

}