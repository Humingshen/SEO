using Hms.Web.API.DTOs;
using Hms.Web.Persistence;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Hms.Web.API.Controllers
{
    public class BaseController : ApiController
    {
        protected string AuthToken => HttpContext.Current.Request.Headers["Authorization"];

        protected WebSiteEntities _db = new WebSiteEntities();

        protected JwtUser JwtUser
        {
            get
            {
                var user = JwtUnit.GetJwtDecode(AuthToken.Replace("Bearer ", ""));

                if (user == null)
                {
                    throw new Exception("JWT authorization failed");
                }
                return user;
            }
        }
    }
}