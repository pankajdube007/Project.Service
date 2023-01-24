using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
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
    public class AddTripController : ApiController
    {
        //private object _goldMedia;

        [HttpPost]
        [ValidateModel]
        [Route("api/AddTrip")]
        public HttpResponseMessage GetDetails(ListAddTrip ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<AddTripLists> alldcr = new List<AddTripLists>();
                    List<AddTripList> alldcr1 = new List<AddTripList>();
                    string uploadImage = string.Empty;
                    string uploadImage1 = string.Empty;

                    if(ula.StartTripImg!="0")
                    {
                        if (ula.StartTripImg != "") uploadImage = GetImage(ula.StartTripImg, 1);

                    }
                    else
                    {

                        uploadImage = "0";
                    }

                    if (ula.EndTripImg != "0")
                    {
                        if (ula.EndTripImg != "") uploadImage1 = GetImage(ula.EndTripImg, 1);

                    }
                    else
                    {

                        uploadImage1 = "0";
                    }



                    //if (ula.StartTripImg != "") uploadImage = GetImage(ula.StartTripImg, 1);
                    //if (ula.EndTripImg != "") uploadImage1 = GetImage(ula.EndTripImg, 1);


                    //var dr = g2.return_dr("dbo.AddTripMst '" + ula.ExId + "','" + ula.VehicleID + "','" + ula.Date + "','" + uploadImage + "','" + ula.FromKm + "','" + uploadImage1 + "','" + ula.ToKm + "'");

                    var dr = g2.return_dr("dbo.AddTripMst '" + ula.ExId + "','" + ula.VehicleID + "','" + ula.Date + "','" + uploadImage + "','" + ula.FromKm + "','" + uploadImage1 + "','" + ula.ToKm + "','" + ula.slno + "','" + ula.suploadby + "','" + ula.euploadby + "'");

                    if (dr.HasRows)
                    {
                        if (ula.slno == 0)
                        {
                            alldcr1.Add(new AddTripList
                            {
                                output = "Data Sucessfully Inserted"
                            });
                        }
                        else
                        {
                            alldcr1.Add(new AddTripList
                            {
                                output = "Data Sucessfully Updated"
                            });
                        }
                        //alldcr1.Add(new AddTripList
                        //{
                        //    output = "Data Sucessfully inserted"
                        //});

                        g2.close_connection();
                        alldcr.Add(new AddTripLists
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
                        //response.Content = new StringContent(cm.StatusTime(false, "Add Trip Not Created!!!!!!!!"), Encoding.UTF8, "application/json");
                        response.Content = new StringContent(cm.StatusTime(false, "Only one trip can be Add in a day!!!!!!!!"), Encoding.UTF8, "application/json");

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
                uniquefoldernm = "tripimg";
            }


            if (!string.IsNullOrEmpty(img))
            {
                var s = img.Trim().Replace(' ', '+').Replace("-", "+").Replace("_", "/");
                if (s.Length % 4 > 0) s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
                var binPdf = Convert.FromBase64String(s);
                Stream stream = new MemoryStream(binPdf);
                var FileName =  Guid.NewGuid().ToString();

                var retStr = _goldMedia.GoldMediaUpload(FileName, uniquefoldernm, ".jpg", stream, "image/jpeg", false,
                    false, true);
                result = _goldMedia.MapPathToPublicUrl(uniquefoldernm+'/'+FileName + ".jpg");
            }
            return result;
        }
    }
}