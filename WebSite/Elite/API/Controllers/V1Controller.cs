using Hms.Web.API.DTOs;
using Hms.Web.Persistence;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Hms.Web.API.Controllers
{
    public class V1Controller : BaseController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dTOs"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v1/login")]
        public JsonResult<BaseResponse> Login(LoginDTOs dTOs)
        {
            var users = (from a in _db.T_Admin
                         where a.LoginId == dTOs.LoginId && a.PassWord == dTOs.PassWord
                         select a);
            if (users.Count() > 0)
            {
                var u = users.FirstOrDefault();

                var payload = new Dictionary<string, object>
                {
                    { "Id",u.Id },
                    { "LoginId", u.LoginId },
                    { "PassWord",GenerateMD5(u.PassWord) },
                    { "Created",DateTime.UtcNow},
                    { "iss","hms"},
                    { "exp",new DateTimeOffset(DateTime.UtcNow).AddDays(15)}
                };
                string token = JwtUnit.SetJwtEncode(payload);


                return Json(new BaseResponse { Code = 0, Data = token }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            return Json(new BaseResponse { Code = 1, Msg = "登录失败" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }


        [HttpPost]
        [Route("api/v1/send")]
        public string Send(CommentDTO dTOs)
        {
            if(_db.T_Comment.Any(o=>o.Email==dTOs.email && o.Created.Day == DateTime.Now.Day))
            {
                return ("感谢您的留言！");
            }
            T_Comment com = new T_Comment()
            {
                IP = "",
                Contents = dTOs.message,
                Created = DateTime.Now,
                Reply = "",
                State = 0,
                Email = dTOs.email,
            };
            _db.T_Comment.Add(com);
            _db.SaveChanges();
            return ("感谢您的留言，我们会尽快与您联系！");
        }



        #region 页面维护

        [HttpGet]
        [Route("api/v1/pages")]
        public JsonResult<BaseResponse> Pages()
        {
            return Json(new BaseResponse { Code = 0, Data = _db.T_Pages.ToList() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }

        [HttpPost]
        [Route("api/v1/pages")]
        public JsonResult<BaseResponse> Pages(PagesDTOs dTOs)
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
                        ID = dTOs.ID,
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
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        [HttpPost]
        [Route("api/v1/pagesDel")]
        public JsonResult<BaseResponse> PagesDel(PagesDTOs dTOs)
        {
            return Json(new BaseResponse() { Code = 1, Msg = "当前栏目，不允许删除" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
        }

        #endregion
        #region 元素维护

        [HttpGet]
        [Route("api/v1/elements")]
        public JsonResult<BaseResponse> Elements(int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Elements
                       select ele;
            var query = eles.OrderByDescending(o => o.ID).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Msg = "", Count = eles.Count() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet]
        [Route("api/v1/elements")]
        public JsonResult<BaseResponse> Elements(string keyword, int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Elements
                       where ele.Title.Contains(keyword) || string.IsNullOrEmpty(keyword)
                       select ele;
            int count = eles.Count();
            if (count > 0)
            {
                if (count / limit < page)
                {
                    page = 1;
                }
            }
            var query = eles.OrderByDescending(o => o.ID).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Count = count }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        [Route("api/v1/elements")]
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
                    ele.Enable = dTOs.Enable;
                    ele.Code = dTOs.Code;
                    ele.HTML = dTOs.HTML;
                }
                else
                {
                    _db.T_Elements.Add(dTOs);
                }
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        [HttpPost]
        [Route("api/v1/elementsDel")]
        public JsonResult<BaseResponse> ElementsDel(T_Elements dTOs)
        {
            try
            {
                var obj = _db.T_Elements.Find(dTOs.ID);
                _db.T_Elements.Remove(obj);
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        #endregion

        #region 文章维护

        [HttpGet]
        [Route("api/v1/article")]
        public JsonResult<BaseResponse> Article(int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Article
                       select ele;
            var query = eles.OrderByDescending(o => o.Id).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Msg = "", Count = eles.Count() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet]
        [Route("api/v1/article")]
        public JsonResult<BaseResponse> Article(string keyword, int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Article
                       where ele.Title.Contains(keyword) || string.IsNullOrEmpty(keyword)
                       select ele;
            int count = eles.Count();
            if (count > 0)
            {
                if (count / limit < page)
                {
                    page = 1;
                }
            }
            var query = eles.OrderByDescending(o => o.Id).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Count = count }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        [Route("api/v1/article")]
        public JsonResult<BaseResponse> Article(T_Article dTOs)
        {
            try
            {
                if (dTOs.Id > 0)
                {
                    _db.T_Article.Attach(dTOs);
                }
                else
                {
                    _db.T_Article.Add(dTOs);
                }
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        [HttpPost]
        [Route("api/v1/ArticleDel")]
        public JsonResult<BaseResponse> ArticleDel(T_Elements dTOs)
        {
            try
            {
                var obj = _db.T_Article.Find(dTOs.ID);
                _db.T_Article.Remove(obj);
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        #endregion

        #region 评论维护

        [HttpGet]
        [Route("api/v1/Comment")]
        public JsonResult<BaseResponse> Comment(int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Comment
                       select ele;
            var query = eles.OrderByDescending(o => o.Id).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Msg = "", Count = eles.Count() }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpGet]
        [Route("api/v1/Comment")]
        public JsonResult<BaseResponse> Comment(string keyword, int page = 1, int limit = 10)
        {
            var eles = from ele in _db.T_Comment
                       where ele.Contents.Contains(keyword) || string.IsNullOrEmpty(keyword)
                       select ele;
            int count = eles.Count();
            if (count > 0)
            {
                if (count / limit < page)
                {
                    page = 1;
                }
            }
            var query = eles.OrderByDescending(o => o.Id).ToList();

            return Json(new BaseResponse { Code = 0, Data = query.Skip((page - 1) * limit).Take(limit), Count = count }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        [HttpPost]
        [Route("api/v1/Comment")]
        public JsonResult<BaseResponse> Comment(T_Comment dTOs)
        {
            try
            {
                _db.T_Comment.Add(dTOs);
                _db.SaveChanges();
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        [HttpPost]
        [Route("api/v1/CommentDel")]
        public JsonResult<BaseResponse> CommentDel(T_Comment dTOs)
        {
            try
            {
                var obj = _db.T_Comment.Find(dTOs.Id);
                _db.T_Comment.Remove(obj);
                return Json(new BaseResponse() { Code = 0, Msg = "success" }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                return Json(new BaseResponse() { Code = 1, Msg = ex.Message }, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
            }
        }

        #endregion
    }
}
