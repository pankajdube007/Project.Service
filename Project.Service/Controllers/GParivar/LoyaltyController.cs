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
    public class LoyaltyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getLoyalty")]
        public HttpResponseMessage GetDetails(ListsofLoyaltyction ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<Loyaltys> alldcr = new List<Loyaltys>();
                    List<Loyalty> alldcr1 = new List<Loyalty>();
                    var dr = g1.return_dt("AppLoyaltyFileUploadActShow '" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new Loyalty
                            {
                                schemename = dr.Rows[i]["schemename"].ToString(),
                                fromdt = dr.Rows[i]["fromdate"].ToString(),
                                todate = dr.Rows[i]["todate"].ToString(),
                                url = string.IsNullOrEmpty(dr.Rows[i]["link"].ToString()) ? "" : (baseurl + "loyaltyfile/" + dr.Rows[i]["link"].ToString()),
                                appurl = string.IsNullOrEmpty(dr.Rows[i]["AppImages"].ToString()) ? "" : (baseurl + "othercompanypricelist/dist/" + dr.Rows[i]["AppImages"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new Loyaltys
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