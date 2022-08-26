using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using NLog;
using Project.Service.Models.GParivar;

namespace Project.Service.Controllers.GParivar
{
    public class MenInBlueSumDetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getMenInBlueSumDetailList")]
        public HttpResponseMessage GetDetails(ListofMenInBlueSumDetail ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetMenInBlueSumDetailLists> alldcr = new List<GetMenInBlueSumDetailLists>();
                    List<GetMenInBlueSumDetailList> alldcr1 = new List<GetMenInBlueSumDetailList>();
                    var dr = g1.return_dr("dbo.meninbluesumdetail '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetMenInBlueSumDetailList
                            {
                                Partyid = Convert.ToString(dr["partyid"].ToString()),
                                Division = Convert.ToString(dr["Division"].ToString()),
                                Slab = Convert.ToString(dr["slb"].ToString()),
                                Sale = Convert.ToString(dr["Sale"].ToString()),
                                Point = Convert.ToString(dr["point"].ToString()),
                                PartyName = Convert.ToString(dr["PartyName"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetMenInBlueSumDetailLists
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