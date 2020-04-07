using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hms.Web.API.DTOs
{
    public class PagesDTOs
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string PageTitle { get; set; }
        public int SEQ { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyWords { get; set; }
        public string SeoDescriptions { get; set; }
        public string Version { get; set; }
    }
}