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
    public class DhanbarseQuickpayVideoController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDhanbarseQwikpayVideo")]
        public HttpResponseMessage GetDetails(ListofDhanbarseQuickpayVideo ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<DhanbarseQuickpayVideos> alldcr = new List<DhanbarseQuickpayVideos>();
                    List<DhanbarseQuickpayVideo> alldcr2 = new List<DhanbarseQuickpayVideo>();

                    var dr = g1.return_dr("AppYouTubeMasterDhanbarsheandQuickpay "+ula.Type);
                   
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr2.Add(new DhanbarseQuickpayVideo
                            {
                                videolink = Convert.ToString(dr["VideoLink"].ToString()),
                                images = baseurl + "youtube/" + Convert.ToString(dr["Images"].ToString().TrimStart(',').TrimEnd(',').Replace(",", "")),
                                subject = Convert.ToString(dr["Subject"].ToString()),
                                details = Convert.ToString(dr["Details"].ToString()),
                                hour = Convert.ToString(dr["TimeHH"].ToString()),
                                minute = Convert.ToString(dr["TimeMM"].ToString()),
                                second = Convert.ToString(dr["TimeTT"].ToString()),
                            });
                        }
                    }
                    
                    g1.close_connection();
                    alldcr.Add(new DhanbarseQuickpayVideos
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = alldcr2,
                    });
                    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    return response;
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