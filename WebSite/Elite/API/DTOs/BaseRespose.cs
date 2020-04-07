using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hms.Web.API.DTOs
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
        public int? Count { get; set; }
    }
}