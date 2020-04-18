using Hms.Web.API.DTOs;
using Hms.Web.Persistence;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Hms.Web.API.Controllers
{
    public class PageController : BaseController
    {
        // GET: api/Page
        [HttpGet]
        [Route("api/Page")]
        public JsonResult<BaseResponse> Get()
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Pages.ToList() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        // GET: api/Page/5
        [HttpGet]
        [Route("api/Page")]
        public JsonResult<BaseResponse> Get(int id)
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Pages.Find(id) }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        // POST: api/Page
        [HttpPost]
        [Route("api/Page")]
        public JsonResult<BaseResponse> Post(PagesDTOs dTOs)
        {
            try
            {
                if (dTOs.ID > 0)
                {
                    var model = _db.T_Pages.Find(dTOs.ID);
                    model.PageTitle = dTOs.PageTitle;
                    model.SeoDescriptions = dTOs.SeoDescriptions;
                    model.SeoKeyWords = dTOs.SeoKeyWords;
                    model.SeoTitle = dTOs.SeoTitle;
                    model.SEQ = dTOs.SEQ;
                    model.Version = dTOs.Version;
                    model.Code = dTOs.Code;
                }
                else
                {
                    var page = new T_Pages()
                    {
                        Code = dTOs.Code,
                        Version = dTOs.Version,
                        SEQ = dTOs.SEQ,
                        SeoTitle = dTOs.SeoTitle,
                        SeoDescriptions = dTOs.SeoDescriptions,
                        PageTitle = dTOs.PageTitle,
                        SeoKeyWords = dTOs.SeoKeyWords
                    };
                    _db.T_Pages.Add(page);
                }
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 200, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 500, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        [HttpPost]
        [Route("api/Page/Delete")]
        public JsonResult<BaseResponse> Delete(PagesDTOs dTOs)
        {
            return Json(new BaseResponse() { Code = 500, Msg = "当前栏目，不允许删除" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
        }
    }
}
