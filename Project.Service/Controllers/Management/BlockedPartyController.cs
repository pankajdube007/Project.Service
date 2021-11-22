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
    public class BlockedPartyController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getBlockedParty")]
        public HttpResponseMessage GetDetails(ListsofBlockedParty ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<BlockedPartys> alldcr = new List<BlockedPartys>();
                    List<BlockedParty> alldcr1 = new List<BlockedParty>();
                    List<BlockedPartyfinal> BlockedPartyfinal = new List<BlockedPartyfinal>();
                    var dr = g1.return_dt("AppgetblockedPartybyid " + ula.ExId + ",'" + ula.CIN + "'," + Convert.ToBoolean(ula.Hierarchy) + "," + ula.index + "," + ula.count);

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new BlockedParty
                            {
                                partyName = Convert.ToString(dr.Rows[i]["name"].ToString()),
                                exnm = Convert.ToString(dr.Rows[i]["salesexname"].ToString()),
                                partyCategory = Convert.ToString(dr.Rows[i]["creditlimitheadernm"].ToString()),
                                permanentLimit = Convert.ToString(dr.Rows[i]["credilimt"].ToString()),
                                outstanding = Convert.ToString(dr.Rows[i]["outstanding"].ToString()),
                                templimitleft = Convert.ToString(dr.Rows[i]["increaselimit"].ToString()),
                            });
                        }

                        BlockedPartyfinal.Add(new BlockedPartyfinal
                        {
                            BlockedPartyDetails = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new BlockedPartys
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = BlockedPartyfinal,
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
    }
}