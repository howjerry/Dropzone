using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Dropzone.ASPNET.MVC.Controllers
{
    public class HomeController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string uuid)
        {
            try
            {
                var baseDir = Server.MapPath($"~/Upload");
                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);

                file.SaveAs(Path.Combine(baseDir, uuid + Path.GetExtension(file.FileName)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }

            return Json(new { message = "Success" });
        }

        [HttpPost]
        public ActionResult Remove(string f)
        {
            try
            {
                var fileName = Server.MapPath($"~/Upload/{f}");
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }


            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}