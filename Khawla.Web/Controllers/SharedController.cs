using Khawla.Entities;
using Khawla.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khawla.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult json = new JsonResult();
            var picturesJson = new List<object>();
            var pictures = Request.Files;

            for(int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var path = Server.MapPath("~/FileStore/images/") + fileName;

                //var picUrl= "~FileStore/images/"+fileName;

                picture.SaveAs(path);//into project images folder

                //into database

                var newPic = new Picture();
                newPic.Url = fileName;
                int picId = SharedService.Instance.SavePicture(newPic);

                picturesJson.Add(new { id = picId, url = fileName});

            }

            json.Data = picturesJson;


            return json;
        }
    }
}