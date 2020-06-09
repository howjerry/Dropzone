using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Dropzone.ASPNET.MVC.Controllers
{
    public class FormsController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Upload(FormsUploadRequest request)
        {
            try
            {
                Logger.Info(JsonConvert.SerializeObject(new {Email = request.Email, Password = request.Password }));

                string directoryPath = Server.MapPath("~/Upload");
                if (request.Files != null && request.Files.Length > 0)
                {
                    for (int i = 0; i < request.Files.Length; i++)
                    {
                        if (request.Files[i] != null && request.Files[i].ContentLength > 0)
                        {
                            var targetFile = request.Files[i];
                            string fileName = targetFile.FileName;
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNewName = request.UUIDs[i] + fileExtension;
                            string filePath = Path.Combine(directoryPath, fileNewName);
                            request.Files[i].SaveAs(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { message = "失敗" });
            }

            return Json(new { message = "上傳成功" });
        }
    }

    public class FormsUploadRequest
    {
        public HttpPostedFileBase[] Files { get; set; }

        public string[] UUIDs { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}