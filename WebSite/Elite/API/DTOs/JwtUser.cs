using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hms.Web.API.DTOs
{
    public class JwtUser
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string PassWord { get; set; }
        public DateTime Created { get; set; }
        public string Iss { get; set; }
        public string Exp { get; set; }
    }
}