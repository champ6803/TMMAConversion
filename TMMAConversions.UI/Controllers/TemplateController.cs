using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMMAConversions.DAL.Models;

namespace TMMAConversions.UI.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        [CustomAuthorize]
        public ActionResult Index()
        {
            DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/Files/Template"));
            FileInfo[] Files = d.GetFiles("*");

            List<FilesModel> filesList = new List<FilesModel>();

            foreach (var a in Files)
            {
                var path = Path.Combine(Server.MapPath("~/Files/Template"), a.Name);
                if (System.IO.File.Exists(path))
                {
                    var f = new FilesModel()
                    {
                        Name = Path.GetFileNameWithoutExtension(path),
                        FileName = a.Name,
                        Extension = a.Extension
                    };

                    filesList.Add(f);
                }
            }

            return View(filesList);
        }

    }
}