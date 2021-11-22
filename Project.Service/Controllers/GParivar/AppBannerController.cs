using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class AppBannerController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetAppBanner")]
        public HttpResponseMessage GetDetails(ListAppbanner ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<AppbannerLists> alldcr = new List<AppbannerLists>();
                    List<AppbannerList> alldcr1 = new List<AppbannerList>();

                    var dr = g1.return_dr("getappbannerimg " + ula.CIN+" ");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new AppbannerList
                            {
                                url = string.IsNullOrEmpty(dr["Images"].ToString().Trim(',')) ? "" : (baseurl + "bannerapp/" + dr["Images"].ToString().Trim(','))

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AppbannerLists
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
                        g1.close_connection();
                       
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No Data"), Encoding.UTF8, "application/json");

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