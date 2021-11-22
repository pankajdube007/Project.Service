using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class ThirdPartyAgingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getthirdpartyaging")]
        public HttpResponseMessage GetDetails(ListofThirdPartyAging ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ThirdPartyAgings> alldcr = new List<ThirdPartyAgings>();
                    List<ThirdPartyAging> alldcr1 = new List<ThirdPartyAging>();

                    var dr = g1.return_dr("thirdpartyoutsatndinghistrypartywiseduedays2allbrancholddateapp '" + ula.Date + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ThirdPartyAging
                            {
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                Party = Convert.ToString(dr["partyname"].ToString()),
                                To30Days = Convert.ToString(dr["a30d"].ToString()),
                                To60Days = Convert.ToString(dr["a60d"].ToString()),
                                To90Days = Convert.ToString(dr["a90d"].ToString()),
                                To120Days = Convert.ToString(dr["a120d"].ToString()),
                                To150Days = Convert.ToString(dr["a150d"].ToString()),
                                Ab150Days = Convert.ToString(dr["ab150"].ToString()),
                                total = Convert.ToString(dr["total"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ThirdPartyAgings
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data"), Encoding.UTF8, "application/json");

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