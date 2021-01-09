//using ICSharpCode.Decompiler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web;
using System.Web.Http;

namespace RatzKatzvi.Controllers
{
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        [HttpGet]
        [Route("GetCvText")]
        public IHttpActionResult GetCvText()
        {
            try
            {
                return Ok(BL.FilesBL.GetTextFromFile("CV"));
            }
            catch (Exception ex) { return NotFound(); }
        }


        //[HttpGet]
        //[Route("GetTextFromFile")]
        //public IHttpActionResult GetTextFromFile()
        //{
        //    try
        //    {
        //        return Ok(BL.FilesBL.GetTextFromFile());
        //    }
        //    catch (Exception ex) { return NotFound(); }
        //}
        //POST 
        public IHttpActionResult Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //TODO
                //change the third arg in th next row to the correct type
                string itemName = BL.FilesBL.SaveFile(httpRequest, "",0);
                // לשנות את ItemKind בכל פונקציה למה שמתאים
                BL.ItemsBL.AddItem(new Dto.Items1 { EnableSearch = true, ItemKind = 1, ItemName = itemName });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        //POST CV
        [Route("PostCV")]
        public IHttpActionResult PostCV()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //TODO
                //change the third arg in th next row to the correct type
                return Ok(BL.FilesBL.SaveFile(httpRequest, "CV/",0));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        //POST Video
        [Route("PostVideo")]
        public IHttpActionResult PostVideo()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //TODO
                //change the third arg in th next row to the correct type
                return Ok(BL.FilesBL.SaveFile(httpRequest, "Video/",0));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        //POST Image
        [Route("PostLesson")]
        public IHttpActionResult PostLesson()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //TODO
                //change the third arg in th next row to the correct type
                return Ok(BL.FilesBL.SaveFile(httpRequest, "Lesson/",0));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        //POST Image
        [Route("PostImage")]
        public IHttpActionResult PostImage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //TODO
                //change the third arg in th next row to the correct type
                return Ok(BL.FilesBL.SaveFile(httpRequest, "Image/",0));
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        [Route("GetAllFilesWithName/{name}")]
        public IHttpActionResult GetAllFilesWithName(string name)
        {
            try
            {
                return Ok(BL.FilesBL.GetAllFilesWithName(name, ""));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }
        [Route("GetImageWithName/{name}")]
        public IHttpActionResult GetImageWithName(string name)
        {
            try
            {
                return Ok(BL.FilesBL.GetAllFilesWithName(name, "Image/"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
        }
        //DOWNLOAD
        [Route("DownloadFile/{fileName}")]
        public HttpResponseMessage DownloadFile(string fileName)
        {
            return BL.FilesBL.DownloadFile(fileName);
        }

        //DELETE
        public IHttpActionResult Delete(string fileName)
        {
            bool result = BL.FilesBL.Delete(fileName);
            if (result)
                return Ok(result);
            return NotFound();
        }
    }
}