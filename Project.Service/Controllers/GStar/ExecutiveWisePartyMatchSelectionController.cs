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
    public class ExecutiveWisePartyMatchSelectionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecutiveWisePartyMatchSelection")]
        public HttpResponseMessage GetDetails(ListExecutiveWisePartyMatchSelection ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ExecutiveWisePartyMatchSelectionLists> alldcr = new List<ExecutiveWisePartyMatchSelectionLists>();
                    List<ExecutiveWisePartyMatchSelectionList> alldcr1 = new List<ExecutiveWisePartyMatchSelectionList>();

                    var dr = g1.return_dr("partywisematchselforexecutive " + ula.ExId + ",'"+ula.leg+ "','" + ula.semi + "','" + ula.win + "','"+ula.cin+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExecutiveWisePartyMatchSelectionList
                            {
                                DealerName = Convert.ToString(dr["name"].ToString()),
                                ContactNo = Convert.ToString(dr["contact"].ToString()),
                                LeagueMatchPrediction = Convert.ToString(dr["issel"].ToString()),
                                SemiFinalPrediction = Convert.ToString(dr["issemisel"].ToString()),
                                WinnerPrediction = Convert.ToString(dr["iswinsel"].ToString()),
                                SaleAmt = Convert.ToString(dr["saleamt"].ToString()),
                                ChanceToWinPer = Convert.ToString(dr["chancetowinper"].ToString()),
                                ChanceToWinAmt = Convert.ToString(dr["chancetowinamt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExecutiveWisePartyMatchSelectionLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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