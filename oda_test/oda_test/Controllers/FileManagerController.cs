using oda_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Web.Hosting;
using Syncfusion.EJ2.FileManager.Base;
using Syncfusion.EJ2.FileManager.PhysicalFileProvider;
using System.IO.Compression;
namespace oda_test.Controllers
{
    public class FileManagerController : Controller
    {
        // GET: FileManager
        private readonly database _database = new database();
        PhysicalFileProvider operation = new PhysicalFileProvider();
        public ActionResult Index()
        {
            return View();
        }
        public FileManagerController()
        {
            var path = HostingEnvironment.MapPath("/files");
            operation.RootFolder(path);
        }
        [HttpPost]
        public ActionResult FIleOperation(FileManagerDirectoryContent content)
        {
            switch (content.Action)
            {
                case "read":
                    return Json(operation.ToCamelCase(operation.GetFiles(content.Path,content.ShowHiddenItems)));
                case "delete":
                    return Json(operation.ToCamelCase(operation.Delete(content.Path,content.Names)));
                case "copy":
                    return Json(operation.ToCamelCase(operation.Copy(content.Path, content.TargetPath, content.Names, content.RenameFiles,content.TargetData)));
                case "move":
                    return Json(operation.ToCamelCase(operation.Copy(content.Path, content.TargetPath, content.Names, content.RenameFiles, content.TargetData)));
                case "details":
                    if (content.Names == null)
                        content.Names = new string[] { };
                    return Json(operation.ToCamelCase(operation.Details(content.Path, content.Names)));
                case "create":
                    return Json(operation.ToCamelCase(operation.Create(content.Path,content.Name)));
                case "search":
                    return Json(operation.ToCamelCase(operation.Search(content.Path,content.SearchString,content.ShowHiddenItems,content.CaseSensitive)));
                case "rename":
                    return Json(operation.ToCamelCase(operation.Rename(content.Path, content.Name, content.NewName)));
            }
            return null;
        }
        private string GetBasePath()
        {
            // Get the base path and create the directory if it doesn't exist
            var path = Server.MapPath("~/files");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public ActionResult Upload(string path,IList<System.Web.HttpPostedFileBase>fileBases, string action) {
            FileManagerResponse response;
            response=operation.Upload(path,fileBases,action,null);
            return Content("");
        }
        public ActionResult Download(string downloadInput)
        {
            FileManagerDirectoryContent content=JsonConvert.DeserializeObject<FileManagerDirectoryContent>(downloadInput);
            string basePath = GetBasePath();
            string relativePath=content.Path?.Trim('/')??String.Empty;
            string fullPath=Path.Combine(basePath, relativePath);
            string extent= Path.GetExtension(content.Names[0]);
            string namedownload;
            if (string.IsNullOrEmpty(relativePath))
            {
                string[] pathSegments = relativePath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                namedownload = pathSegments[pathSegments.Length - 1];
            }
            else
            {
                namedownload = "files";
            }
            List<string>   files = new List<string>();
            if (content.Names.Length == 1 && extent != "")
            {
                string stringFilePath = Path.Combine(fullPath, content.Names[0]);
                if (System.IO.File.Exists(stringFilePath))
                {
                    return HttpNotFound();
                }
                byte[] fileBytes = System.IO.File.ReadAllBytes(stringFilePath);
                string fileNames = content.Names[0];
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileNames);
            }
            else {
                foreach (string fileName in content.Names) {
                    string filePath = Path.Combine(fullPath, fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        files.Add(filePath);
                    }
                    else if (Directory.Exists(filePath)) {
                        DirectoryInfo directory = new DirectoryInfo(filePath);
                        var filesInDirectory = directory.GetFiles("*", SearchOption.AllDirectories);
                        foreach (var file in filesInDirectory) { 
                            files.Add(file.FullName);
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                using (MemoryStream memory = new MemoryStream())
                {
                    using (ZipArchive zip = new ZipArchive(memory,ZipArchiveMode.Create,true)) 
                    {
                        foreach (string fileName in files) 
                        { 
                            string entryName=relativePath+"/"+Path.GetFileName(fileName);
                            zip.CreateEntryFromFile(fileName, entryName);
                        }
                    }
                    memory.Position = 0;
                    return File(memory.ToArray(), "application/zip", namedownload + ".zip");
                }
            }
        }
        public ActionResult GetImage(FileManagerDirectoryContent args)
        {
            return operation.GetImage(args.Path,args.Id,false,null,null);
        }

    }
    public class FileManagerItem
    {
        public string Name { get; set; }
        public string Type { get; set; } // "file" hoặc "folder"
        public long Size { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool HasChild { get; set; }
    }
}