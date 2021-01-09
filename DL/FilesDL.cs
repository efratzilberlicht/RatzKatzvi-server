using GemBox.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
//using System.Web;
//using ICSharpCode.Decompiler.Util;
using System.Configuration;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
//using Grpc.Core;

namespace DL
{
    public static class FilesDL
    {

        public static string GetTextFromFile(string filePath)
        {
            try
            {
                ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                // Load a document from a Stream contained in FileUpload control.
                var document = DocumentModel.Load(new MemoryStream(File.ReadAllBytes(filePath)), LoadOptions.DocxDefault);

                // Initialize new StringBuilder.
                var sb = new StringBuilder();

                // Iterate over all paragraphs in the document and append new line to StringBuilder.
                foreach (Paragraph paragraph in document.GetChildElements(true, ElementType.Paragraph))
                {
                    // Iterate over all text elements in the parent paragraph and append its text to StringBuilder.
                    foreach (Run run in paragraph.GetChildElements(true, ElementType.Run))
                        sb.Append(run.Text);
                    sb.AppendLine();
                }
                return sb.ToString();

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string SaveFile(HttpRequest files, string specificFolder)
        {
            try
            {
                if (files.Files.Count > 0)
                {
                    foreach (string file in files.Files)
                    {
                        var postedFile = files.Files[file];
                        string FolderPath = "~\\Files\\" + specificFolder;
                        DirectoryInfo FolderDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(FolderPath));
                        if (!FolderDir.Exists)
                            FolderDir.Create();
                        string filePath = Path.Combine(HttpContext.Current.Server.MapPath(FolderPath), postedFile.FileName);
                        if (!File.Exists(filePath))
                        {
                            try
                            {
                                postedFile.SaveAs(filePath);
                                return Path.GetFileNameWithoutExtension(filePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                return "Error: Fail Uploading File";
                            }
                        }
                        return "Error: File is Exist";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return "Error";
            }
        }
        public static string[] GetAllFilesWithName(string name, string specificFolder)
        {
            string[] fileArray = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Files/" + specificFolder + name), "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < fileArray.Length; i++)
            {
                var split = fileArray[i].Split('\\');
                fileArray[i] = split[split.Length - 1];
            }
            return fileArray;
        }
        public static HttpResponseMessage DownloadFile(string fileName)
        {
            if ((System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Files/" + fileName))))

                using (var webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(HttpContext.Current.Server.MapPath("~/Files/" + fileName));
                    var ms = new MemoryStream(data);

                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(ms.ToArray())
                    };
                    result.Content.Headers.ContentDisposition =
                        new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = fileName
                        };
                    result.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    return result;
                }
            return new HttpResponseMessage(HttpStatusCode.NotFound);

        }
        public static bool Delete(string fileName)
        {
            if ((System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Files/" + fileName))))
            {
                try
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/Files/" + fileName));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }

            }
            return false;
        }
    }
}

