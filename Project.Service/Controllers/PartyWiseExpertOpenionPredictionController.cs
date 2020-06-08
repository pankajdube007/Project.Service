using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class PartyWiseExpertOpenionPredictionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/PartyWiseExpertOpenionPredictions")]
        public HttpResponseMessage GetDetails(ListPartyWiseExpertOpenionPrediction ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PartyWiseExpertOpenionPredictionsLits> alldcr = new List<PartyWiseExpertOpenionPredictionsLits>();
                    List<PartyWiseExpertOpenionPredictionList> alldcr1 = new List<PartyWiseExpertOpenionPredictionList>();
                    var dr = g2.return_dr("PartyWiseExpertOpenionPrediction '" + ula.CIN + "','" + ula.MatchSummaryId + "','" + ula.BatFirstTeam + "','" + ula.TossWinTeamId + "','" + ula.foureid + "','" + ula.sixid + "','" + ula.scoreid + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new PartyWiseExpertOpenionPredictionList
                        {
                            output = "Expert Opinion Prediction For Match Submitted Successfully"
                        });

                        g2.close_connection();
                        alldcr.Add(new PartyWiseExpertOpenionPredictionsLits
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Data Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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