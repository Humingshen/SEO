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
    public class ElementController : BaseController
    {
      

        // GET: api/Element/5
        [HttpGet]
        [Route("api/Element/List")]
        public JsonResult<BaseResponse> List(int pageId)
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Elements.Where(i=>i.PageId==pageId).ToList() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet]
        [Route("api/Element/StateCk")]
        public JsonResult<BaseResponse> StateCk(int id)
        {
            try
            {
                var model = _db.T_Elements.Find(id);
                model.Enable = !model.Enable;
                _db.SaveChanges();
                return Json(new BaseResponse { Code = 200, Msg="操作成功！", Data = null }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse { Code = 500, Msg = ex.Message, Data = null }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
        }
      
        // GET: api/Element/5
        [HttpGet]
        [Route("api/Element")]
        public JsonResult<BaseResponse> Get(int id)
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Elements.Find(id) }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        // POST: api/Element
        [HttpPost]
        [Route("api/Element")]
        public JsonResult<BaseResponse> Elements(T_Elements dTOs)
        {
            try
            {
                if (dTOs.ID > 0)
                {
                    var ele = _db.T_Elements.Find(dTOs.ID);
                    ele.SEQ = dTOs.SEQ;
                    ele.Src = dTOs.Src;
                    ele.Url = dTOs.Url;
                    ele.Title = dTOs.Title;
                    ele.Code = dTOs.Code;
                    ele.HTML = dTOs.HTML;
                }
                else
                {
                    _db.T_Elements.Add(dTOs);
                }
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 200, Msg = "操作成功！" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 500, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        // PUT: api/Element/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Element/5
        public void Delete(int id)
        {
        }
    }
}
