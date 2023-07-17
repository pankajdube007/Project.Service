using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class inspectionfileuploadController : ApiController
    {
        private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/inspectionfileupload")]
        public HttpResponseMessage GetDetails(Listinspectionfileuplode ula)
        {
          //  DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<inspectionfileuplodes> alldcr = new List<inspectionfileuplodes>();
                    List<inspectionfileuplode> alldcr1 = new List<inspectionfileuplode>();
                    string uploadVehicleImage = string.Empty;
                    //ula.type=0 image 1 pdf
                    if (ula.img != "") uploadVehicleImage = GetImage(ula.img, ula.foldertype, ula.type);


                    alldcr1.Add(new inspectionfileuplode
                    {
                        output = uploadVehicleImage
                        });

                        //g2.close_connection();
                        alldcr.Add(new inspectionfileuplodes
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    
                }
                catch (Exception ex)
                {
                    //var a =ex.Message;
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }



        protected string GetImage(string img, int folderCreation, int type)
        {
            var _goldMedia = new GoldMedia();
            var result = "";
            string uniquefoldernm = "";
            if (folderCreation == 0)
            {
                uniquefoldernm = "inspectionimg";
            }
            else if (folderCreation == 2)
            {
                uniquefoldernm = "Vehicleimage";
            }
            else if(folderCreation == 3)
            {
                uniquefoldernm = "tripimg";
            }
            else if(folderCreation == 4)
            {
                uniquefoldernm = "addmerchantpdf";
            }


            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName = "erp" + '-' + Guid.NewGuid();

                if (type == 1)
                {
                    var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".pdf", stream, "application/pdf", false, false, true);
                    result = FileName + ".pdf";
                }
                if (type == 0)
                {
                    var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                   false, true);
                    result = FileName + ".jpg";
                }
            }



            return result;
        }
    }
}


