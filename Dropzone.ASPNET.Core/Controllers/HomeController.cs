using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Dropzone.ASPNET.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, string uuid)
        {
            try
            {
                var baseDir = Path.Combine(_hostEnvironment.WebRootPath, "Upload");

                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);

                var filePath = Path.Combine(baseDir, uuid + Path.GetExtension(file.FileName));

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Json(new { message = "Success" });
        }

        [HttpPost]
        public IActionResult Remove(string f)
        {
            try
            {
                var fileName = Path.Combine(_hostEnvironment.WebRootPath, "Upload", f);
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }


            return Ok();
        }
    }
}
