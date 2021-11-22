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
    public class AnalyticsDataController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getAnalyticsDataadd")]
        public HttpResponseMessage GetDetails(ListAnalyticsData ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<AnalyticsDatas> alldcr = new List<AnalyticsDatas>();
                    List<AnalyticsData> alldcr1 = new List<AnalyticsData>();

                    var dr = g2.return_dr("AddAnalyticsData '" + ula.CIN + "','" + ula.DeviceId + "','" + ula.ClientSecret + "','" + ula.DateTime + "','" + ula.ScreenName + "','" + ula.ScreenId + "','" + ula.AppId + "','" + ula.OSVersion + "','"+ula.DeviceModel+"','"+ula.DeviceType+"'");
                    if (dr.HasRows)
                    {
                        alldcr1.Add(new AnalyticsData
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new AnalyticsDatas
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Not Created!!!!!!!!"), Encoding.UTF8, "application/json");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"+ex.Message), Encoding.UTF8, "application/json");

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