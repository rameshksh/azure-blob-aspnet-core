using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AzureBlobDemo.Models;
using System.IO;
using Microsoft.Azure.Storage.Blob;
using AzureBlobDemo.Services;

namespace AzureBlobDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            foreach (var file in Request.Form.Files)
            {
                // Container Name - picture  
                BlobManager BlobManagerObj = new BlobManager("photos");
                string FileAbsoluteUri = BlobManagerObj.UploadFile(file);

                return RedirectToAction("Get");
            }

            return View();
           
        }

        public ActionResult Get()
        {
            // Container Name - picture  
            BlobManager BlobManagerObj = new BlobManager("photos");
            List<string> fileList = BlobManagerObj.BlobList();
            return View("LIST", fileList);
        }

        public ActionResult Delete(string uri)
        {
            // Container Name - picture  
            BlobManager BlobManagerObj = new BlobManager("photos");
            BlobManagerObj.DeleteBlob(uri);
            return RedirectToAction("Get");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
