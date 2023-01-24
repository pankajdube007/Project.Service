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

namespace Project.Service.Controllers.GStar
{
    public class VehicleMstController : ApiController
    {
        private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/AddVehicleMaster")]
        public HttpResponseMessage GetDetails(ListAddVehicleMst ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddVehicleMsts> alldcr = new List<AddVehicleMsts>();
                    List<AddVehicleMst> alldcr1 = new List<AddVehicleMst>();
                    string uploadVehicleImage = string.Empty;
                    string uploadOdometerImage = string.Empty;

                    if (ula.img != "") uploadVehicleImage = GetImage(ula.img, 1);
                    if (ula.odoimg != "") uploadOdometerImage = GetImage(ula.odoimg, 1);



                    var dr = g2.return_dr("dbo.AddVehicleMst '" + ula.ExId + "','" + ula.VehicleType + "','" + ula.VehicleNo + "','" + ula.OwnedBy + "','" + uploadVehicleImage + "','" + ula.model + "','" + ula.mfgby + "','" + uploadOdometerImage + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AddVehicleMst
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AddVehicleMsts
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
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Vehicle Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
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



        protected string GetImage(string img, int folderCreation)
        {
            var _goldMedia = new GoldMedia();
            var result = "";
            string uniquefoldernm = "";
            if (folderCreation == 1)
            {
                uniquefoldernm = "Vehicleimage";
            }
           

            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName = "erp" + '-' + Guid.NewGuid();

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                    false, true);
                result = FileName + ".jpg";
            }
            return result;
        }
    }
}