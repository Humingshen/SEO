using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hms.Web.API.DTOs
{
    public class FileResponse
    {
        public int errno { get; set; } = 0;
        public object data { get; set; }
    }
}