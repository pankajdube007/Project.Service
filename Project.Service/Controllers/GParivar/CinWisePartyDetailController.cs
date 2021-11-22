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
    public class CinWisePartyDetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getcinwisepartydetail")]
        public HttpResponseMessage GetDetails(ListofCinWiseParty ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<CinWisePartyLists> alldcr = new List<CinWisePartyLists>();
                    List<CinWisePartyList> alldcr1 = new List<CinWisePartyList>();
                    var dr = g1.return_dr("getcinwisepartydetail '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CinWisePartyList
                            {
                                PartyName = Convert.ToString(dr["Name"]),
                                PartyType = Convert.ToString(dr["PartyType"]),
                                City = Convert.ToString(dr["city"]),
                                Area = Convert.ToString(dr["areanm"]),
                                ExecutiveName = Convert.ToString(dr["salesexname"]),
                                Executivephno = Convert.ToString(dr["salescontact"]),
                                ExtraCDDays = Convert.ToString(dr["ExtraCDDays"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CinWisePartyLists
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