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
    public class MatchSummaryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetMatchSummary")]
        public HttpResponseMessage GetDetails(ListMatchSummary ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN!="")
            {
                try
                {
                    string data1;

                    List<MatchSummaryLists> alldcr = new List<MatchSummaryLists>();
                    List<MatchSummaryList> alldcr1 = new List<MatchSummaryList>();

                    var dr = g1.return_dr("MatchSummaryList '"+ula.CIN+"'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new MatchSummaryList
                            {
                                Matchsummaryid = Convert.ToInt32(dr["matchsummaryid"].ToString()),
                                Event = Convert.ToString(dr["EventName"].ToString()),
                                Team1id = Convert.ToInt32(dr["team1"].ToString()),
                                Team1 = Convert.ToString(dr["team1nm"].ToString()),
                                team1point = Convert.ToDecimal(dr["team1winpt"].ToString()),
                                Team2id = Convert.ToInt32(dr["team2"].ToString()),
                                Team2 = Convert.ToString(dr["team2nm"].ToString()),
                                team2point = Convert.ToDecimal(dr["team2winpt"].ToString()),
                                MatchDate = Convert.ToString(dr["mtdt"].ToString()),
                                MatchTime = Convert.ToString(dr["mt"].ToString()),
                                winnerlock = Convert.ToBoolean(dr["winlk"].ToString()),
                                winnerteam = Convert.ToString(dr["wintm"].ToString()),
                                othetplay = Convert.ToBoolean(dr["otherpl"].ToString()),
                                othetplayteam = Convert.ToString(dr["otherplaytm"].ToString()),
                                othetplayteamid = Convert.ToInt32(dr["otherplayteam"].ToString()),
                                winnerlockteamid = Convert.ToInt32(dr["winnerteam"].ToString()),
                                team1per = Convert.ToDecimal(dr["team1per"].ToString()),
                                team2per = Convert.ToDecimal(dr["team2per"].ToString()),
                                venue = Convert.ToString(dr["venue"].ToString()),
                                selectedteam= Convert.ToInt32(dr["selectedteamid"].ToString()),
                                team1selected = Convert.ToBoolean(dr["team1selected"]),
                                team2selected = Convert.ToBoolean(dr["team2selected"]),
                                prediction = false,
                                result = "",
                                prediction1 = Convert.ToBoolean(dr["prediction"]),
                                result1 = Convert.ToString(dr["result"].ToString()),

                              
                                isSelectionAvailable = Convert.ToString(dr["isSelectionAvailable"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new MatchSummaryLists
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