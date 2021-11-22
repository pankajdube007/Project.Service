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
    public class IncreaseLimitDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetIncreaseLimitPartyDetails")]
        public HttpResponseMessage GetDetails(IncreaseLimitDetailsAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.userid != 0)
            {
                try
                {
                    string data;
                    List<IncreaseLimitDetails> alldcr = new List<IncreaseLimitDetails>();
                    List<IncreaseLimitDetail> alldcr1 = new List<IncreaseLimitDetail>();
                    var dr = g1.return_dr("App_PartyCreditLimitSelect " + ula.userid + ",'" + ula.searchtxt + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new IncreaseLimitDetail
                            {
                                slno = Convert.ToInt32(dr["SlNo"].ToString()),
                                partytype = Convert.ToString(dr["PartyType"].ToString()),
                                displaynm = Convert.ToString(dr["displaynm"].ToString()),
                                homebranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                city = Convert.ToString(dr["city"].ToString()),
                                increaselimit = Convert.ToString(dr["increaselimit"].ToString()),
                                creatdt = Convert.ToString(dr["createdt"].ToString()),
                                status = Convert.ToString(dr["stat"].ToString()),
                                uselimit = Convert.ToString(dr["uselimit"].ToString()),
                                usernm = Convert.ToString(dr["name"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new IncreaseLimitDetails
                        {
                            result = "True",
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "No  Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}