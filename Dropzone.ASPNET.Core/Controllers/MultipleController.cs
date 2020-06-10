using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dropzone.ASPNET.Core.Controllers
{
    public class MultipleController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public MultipleController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(string[] uuid)
        {
            try
            {
                //NOTE:List<IFormFile> 接不到
                var files = Request.Form.Files;
                string directoryPath = Path.Combine(_hostEnvironment.WebRootPath, "Upload");

                int index = 0;
                foreach (IFormFile file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var targetFile = file;
                        string fileName = targetFile.FileName;
                        string fileExtension = Path.GetExtension(fileName);
                        string fileNewName = uuid[index] + fileExtension;
                        string filePath = Path.Combine(directoryPath, fileNewName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            targetFile.CopyToAsync(fileStream);
                        }
                    }

                    index++;
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = "失敗" });
            }

            return Json(new { message = "上傳成功" });
        }
    }
}
