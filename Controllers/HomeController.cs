using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace RunTime_RDL_Report_In_Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public object Index()
        {
            return Resource("index.html");
        }

        [HttpGet("{file}")]
        public object Resource(string file)
        {
            Stream stream = GetType().Assembly.GetManifestResourceStream("RunTime_RDL_Report_in_Web.wwwroot." + file);
            if (stream == null)
            {
                return new NotFoundResult();
            }

            if (Path.GetExtension(file) == ".html")
            {
                return new ContentResult() { Content = new StreamReader(stream).ReadToEnd(), ContentType = "text/html" };
            }

            if (Path.GetExtension(file) == ".ico")
            {
                using MemoryStream memoryStream = new();
                stream.CopyTo(memoryStream);
                return new FileContentResult(memoryStream.ToArray(), "image/x-icon") { FileDownloadName = file };
            }

            using StreamReader streamReader = new(stream);
            return new FileContentResult(System.Text.Encoding.UTF8.GetBytes(streamReader.ReadToEnd()), GetType(file)) { FileDownloadName = file };
        }

        private static string GetType(string file)
        {
            return file.EndsWith(".css") ? "text/css" : file.EndsWith(".js") ? "text/javascript" : "text/html";
        }

        [HttpGet("reports")]
        public ActionResult Reports()
        {
            string[] validExtensions = [".rdl", ".rdlx", ".rdlx-master"];

            return new ObjectResult(
                typeof(HomeController).Assembly.GetManifestResourceNames()
                .Where(x => x.StartsWith(Startup.EmbeddedReportsPrefix) && validExtensions.Any(ext => x.EndsWith(ext, StringComparison.InvariantCultureIgnoreCase)))
                .Select(x => x[(Startup.EmbeddedReportsPrefix.Length + 1)..])
                .ToArray());
        }
    }
}
