using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileUploadSampleAspnetCor.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FileUploadSampleAspnetCor.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;

        public HomeController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SigleFileUpload(IFormFile file)
        {
            string dir = Path.Combine(_env.ContentRootPath, "wwwroot", "attach");

            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream fs = new FileStream(Path.Combine(dir,file.FileName), FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return RedirectToAction("About");
        }

        public IActionResult MultiFileUpload(IList<IFormFile> files)
        {
            string dir = Path.Combine(_env.ContentRootPath, "wwwroot", "attach");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (var file in files)
            {
                using (FileStream fs = new FileStream(Path.Combine(dir, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }          

            return RedirectToAction("About");
        }

        public IActionResult ModelInFileUpload(BoardModel model)
        {
            string title = model.title;
            string content = model.content;
            string dir = Path.Combine(_env.ContentRootPath, "wwwroot", "attach");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            foreach (var file in model.files)
            {
                using (FileStream fs = new FileStream(Path.Combine(dir, title +"_"+ file.FileName), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            return RedirectToAction("About");
        }
    }
}
