using System;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Web;

namespace BL
{
    public static class FilesBL
    {
        public static string GetTextFromFile(string folderName)
        {
            string FolderPath = HttpContext.Current.Server.MapPath("~\\Files\\" + folderName + "\\");
            DirectoryInfo FolderDir = new DirectoryInfo(FolderPath);
            if (!FolderDir.Exists)
            {
                return "Not Found Folder";
            }
            var filePath = Directory.GetFiles(FolderPath).OrderByDescending(file => File.GetLastWriteTime(file)).First();
            return DL.FilesDL.GetTextFromFile(filePath);
        }

        public static string SaveFile(HttpRequest files, string specificFolder, int ItemKind)
        {
            try
            {
                var result = DL.FilesDL.SaveFile(files, specificFolder);
                if (!result.Contains("Error"))
                {
                    BL.ItemsBL.AddItem(new Dto.Items1 { EnableSearch = true, ItemKind = ItemKind, ItemName = result });
                    return "Success";
                }
                return result;
            }
            catch (Exception)
            {
                return "Error Saving File";
            }
        }
        public static string[] GetAllFilesWithName(string name, string specificFolder)
        {
            return DL.FilesDL.GetAllFilesWithName(name, specificFolder);
        }
        public static HttpResponseMessage DownloadFile(string fileName)
        {
            return DL.FilesDL.DownloadFile(fileName);
        }
        public static bool Delete(string fileName)
        {
            return DL.FilesDL.Delete(fileName);
        }
    }
}
