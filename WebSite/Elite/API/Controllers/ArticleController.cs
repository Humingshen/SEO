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
    public class ArticleController : BaseController
    {
        [HttpGet]
        [Route("api/Article")]
        public JsonResult<BaseResponse> Article(int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Article
                       select ele;
            var query = eles.OrderByDescending(o => o.Id).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Msg = "", Count = eles.Count() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        // GET: api/Article/5
        public JsonResult<BaseResponse> Get(int id)
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Article.Find(id) , Msg = "" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        // POST: api/Article
        [HttpPost]
        [Route("api/Article")]
        public JsonResult<BaseResponse> Article(T_Article dTOs)
        {
            try
            {
                if (dTOs.Id > 0)
                {
                    var ele = _db.T_Article.Find(dTOs.Id);
                    ele.Title = dTOs.Title;
                    ele.Author = dTOs.Author;
                    ele.SubTitle = dTOs.SubTitle + ""; ;
                    ele.Tags = dTOs.Tags;
                    ele.Visitors = dTOs.Visitors;
                    ele.Reading = dTOs.Reading;
                    ele.Cover = dTOs.Cover + "";
                    ele.Source = dTOs.Source + "";
                    ele.Contents = dTOs.Contents + "";
                    ele.State = dTOs.State;
                    ele.Version = dTOs.Version;
                    ele.Updated = DateTime.Now;
                }
                else
                {
                    dTOs.Cover = dTOs.Cover + "";
                    dTOs.SubTitle = dTOs.SubTitle + "";
                    dTOs.Contents = dTOs.Contents + "";
                    dTOs.Source = dTOs.Source + "";
                    dTOs.Updated = DateTime.Now;
                    _db.T_Article.Add(dTOs);
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
        [Route("api/Article/Delete")]
        public JsonResult<BaseResponse> Delete(T_Article dTOs)
        {
            try
            {
                var model = _db.T_Article.Find(dTOs.Id);
                _db.T_Article.Remove(model);
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 200, Msg ="操作成功！" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 500, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }
        [HttpGet]
        [Route("api/Article/StateCk")]
        public JsonResult<BaseResponse> StateCk(int id)
        {
            try
            {
                var model = _db.T_Article.Find(id);
                model.State = model.State == 0 ? 1 : 0;
                _db.SaveChanges();
                return Json(new BaseResponse { Code = 200, Msg = "操作成功！", Data = null }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse { Code = 500, Msg = ex.Message, Data = null }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
        }
    }
}
