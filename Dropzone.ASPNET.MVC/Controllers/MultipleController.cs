using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dropzone.ASPNET.MVC.Controllers
{
    public class MultipleController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ActionResult Index(string[] uuid)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] file, string[] uuid)
        {
            try
            {
                string directoryPath = Server.MapPath("~/Upload");
                if (file != null && file.Length > 0)
                {
                    for (int i = 0; i < file.Length; i++)
                    {
                        if (file[i] != null && file[i].ContentLength > 0)
                        {
                            var targetFile = file[i];
                            string fileName = targetFile.FileName;
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNewName = uuid[i] + fileExtension;
                            string filePath = Path.Combine(directoryPath, fileNewName);
                            file[i].SaveAs(filePath);
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
}