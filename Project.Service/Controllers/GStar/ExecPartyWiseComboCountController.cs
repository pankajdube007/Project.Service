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
    public class ExecPartyWiseComboCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getexecPartywiesecombo")]
        public HttpResponseMessage GetDetails(ListsPartyWiseCobmoCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<PartyWiseCobmoCounts> alldcr = new List<PartyWiseCobmoCounts>();
                    List<PartyWiseCobmoCount> alldcr1 = new List<PartyWiseCobmoCount>();

                    var dr = g1.return_dr("execPartywiesecombo " + ula.ExId + "");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PartyWiseCobmoCount
                            {
                                Cin = Convert.ToString(dr["partycin"]),
                                Name = Convert.ToString(dr["locnm"]),
                                Count = Convert.ToString(dr["cnt"]),
                                Used = Convert.ToString(dr["used"]),
                               
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PartyWiseCobmoCounts
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");

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