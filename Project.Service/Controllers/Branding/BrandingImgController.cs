using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class BrandingImgController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getBrandingImages")]
        public HttpResponseMessage GetDetails(BrandingImgAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<BrandingImgs> alldcr = new List<BrandingImgs>();
                    List<BrandingImg> alldcr1 = new List<BrandingImg>();

                    var dr = g1.return_dr("App_BrandingImg '" + ula.CIN + "'");
                    //  DataTable dr1 = g1.return_dt("App_dealerlstyearsale '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new BrandingImg
                            {
                                url = string.IsNullOrEmpty(dr["item"].ToString().Trim(',')) ? "" : (baseurl + "branding/" + dr["item"].ToString().Trim(','))
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new BrandingImgs
                        {
                            result = true,
                            message = "",
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
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

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
    }
}