using Hms.Web.API.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Wshare.Controllers
{
    public class FileController : ApiController
    {

        [HttpPost]
        [Route("api/File")]
        public JsonResult<FileResponse> UploadFile()
        {
            List<string> files = new List<string>();
            //获取参数信息
            HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];
            HttpRequestBase request = context.Request;      //定义传统request对象
            string id = request["id"];       //获取请求参数：

            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"~/");
            //var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);
            HttpFileCollection filelist = HttpContext.Current.Request.Files;
            if (filelist != null && filelist.Count > 0)
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    HttpPostedFile file = filelist[i];
                    String Tpath = DateTime.Now.ToString("yyyy-MM-dd");
                    string filename = file.FileName;
                    //获取上传文件后缀名
                    string fileExt = filename.Substring(filename.LastIndexOf('.'));
                    string FileName = Guid.NewGuid().ToString() + fileExt;
                    string FilePath = "Content/upload/" + id + "/";
                    DirectoryInfo di = new DirectoryInfo(path+FilePath);
                    if (!di.Exists) { di.Create(); }
                    try
                    {
                        file.SaveAs(path + FilePath + FileName);
                        files.Add("/" + FilePath + FileName);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
            }
            return Json(new FileResponse { errno = 0, data = files });
        }

    }
}
