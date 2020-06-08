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
    public class MatchSummaryExpertOpenionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetMatchSummaryExpertOpenion")]
        public HttpResponseMessage GetDetails(ListMatchSummaryExpertOpenion ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ListMatchSummaryExpertOpenionLists> alldcr = new List<ListMatchSummaryExpertOpenionLists>();
                    List<ListMatchSummaryExpertOpenionList> alldcr1 = new List<ListMatchSummaryExpertOpenionList>();

                    var dr = g1.return_dr("MatchSummaryListExpertOpenion '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new ListMatchSummaryExpertOpenionList
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
                                selectedteam = Convert.ToInt32(dr["selectedteamid"].ToString()),
                                team1selected = Convert.ToBoolean(dr["team1selected"]),
                                team2selected = Convert.ToBoolean(dr["team2selected"]),
                                prediction = Convert.ToBoolean(dr["prediction"]),
                                result = Convert.ToString(dr["result"].ToString()),
                                slab1four = Convert.ToString(dr["slab1four"].ToString()),
                                slab1fourslno = Convert.ToString(dr["slab1fourslno"].ToString()),
                                slab1fourpt = Convert.ToString(dr["slab1fourpt"].ToString()),
                                slab2four = Convert.ToString(dr["slab2four"].ToString()),
                                slab2fourslno = Convert.ToString(dr["slab2fourslno"].ToString()),
                                slab2fourpt = Convert.ToString(dr["slab2fourpt"].ToString()),
                                slab3four = Convert.ToString(dr["slab3four"].ToString()),
                                slab3fourslno = Convert.ToString(dr["slab3fourslno"].ToString()),
                                slab3fourpt = Convert.ToString(dr["slab3fourpt"].ToString()),
                                slab4four = Convert.ToString(dr["slab4four"].ToString()),
                                slab4fourslno = Convert.ToString(dr["slab4fourslno"].ToString()),
                                slab4fourpt = Convert.ToString(dr["slab4fourpt"].ToString()),
                                slab1six = Convert.ToString(dr["slab1six"].ToString()),
                                slab1sixslno = Convert.ToString(dr["slab1sixslno"].ToString()),
                                slab1sixpt = Convert.ToString(dr["slab1sixpt"].ToString()),
                                slab2six = Convert.ToString(dr["slab2six"].ToString()),
                                slab2sixslno = Convert.ToString(dr["slab2sixslno"].ToString()),
                                slab2sixpt = Convert.ToString(dr["slab2sixpt"].ToString()),
                                slab3six = Convert.ToString(dr["slab3six"].ToString()),
                                slab3sixslno = Convert.ToString(dr["slab3sixslno"].ToString()),
                                slab3sixpt = Convert.ToString(dr["slab3sixpt"].ToString()),
                                slab4six = Convert.ToString(dr["slab4six"].ToString()),
                                slab4sixslno = Convert.ToString(dr["slab4sixslno"].ToString()),
                                slab4sixpt = Convert.ToString(dr["slab4sixpt"].ToString()),
                                slab1scr = Convert.ToString(dr["slab1scr"].ToString()),
                                slab1scrslno = Convert.ToString(dr["slab1scrslno"].ToString()),
                                slab1scrpt = Convert.ToString(dr["slab1scrpt"].ToString()),
                                slab2scr = Convert.ToString(dr["slab2scr"].ToString()),
                                slab2scrslno = Convert.ToString(dr["slab2scrslno"].ToString()),
                                slab2scrpt = Convert.ToString(dr["slab2scrpt"].ToString()),
                                slab3scr = Convert.ToString(dr["slab3scr"].ToString()),
                                slab3scrslno = Convert.ToString(dr["slab3scrslno"].ToString()),
                                slab3scrpt = Convert.ToString(dr["slab3scrpt"].ToString()),
                                slab4scr = Convert.ToString(dr["slab4scr"].ToString()),
                                slab4scrslno = Convert.ToString(dr["slab4scrslno"].ToString()),
                                slab4scrpt = Convert.ToString(dr["slab4scrpt"].ToString()),
                                TossPrediction = Convert.ToString(dr["TossPrediction"].ToString()),
                                BatFirstPrediction = Convert.ToString(dr["BatFirstPrediction"].ToString()),
                                TossPredictiontteamid = Convert.ToString(dr["TossPredictiontteamid"].ToString()),
                                BatFirstPredictionteamid = Convert.ToString(dr["BatFirstPredictionteamid"].ToString()),
                                selected4slabid = Convert.ToString(dr["selected4slabid"].ToString()),
                                selected6slabid = Convert.ToString(dr["selected6slabid"].ToString()),
                                selectedscoreslabid = Convert.ToString(dr["selectedscoreslabid"].ToString()),
                                selected4slab = Convert.ToString(dr["selected4slab"].ToString()),
                                selected6slab = Convert.ToString(dr["selected6slab"].ToString()),
                                selectedscoreslab = Convert.ToString(dr["selectedscoreslab"].ToString()),
                                selected4slabpoint = Convert.ToString(dr["selected4slabpoint"].ToString()),
                                selected6slabpoint = Convert.ToString(dr["selected6slabpoint"].ToString()),
                                selectedscoreslabpoint = Convert.ToString(dr["selectedscoreslabpoint"].ToString()),


                                resulttosswinteam = Convert.ToString(dr["resulttosswinteam"].ToString()),
                                resulttosswinteamid = Convert.ToString(dr["resulttosswinteamid"].ToString()),
                                resultbatswinteam = Convert.ToString(dr["resultbatswinteam"].ToString()),
                                resultbatswinteamid = Convert.ToString(dr["resultbatswinteamid"].ToString()),




                                resultfourslab = Convert.ToString(dr["resultfourslab"].ToString()),
                                resultsixslab = Convert.ToString(dr["resultsixslab"].ToString()),
                                resultscoreslab = Convert.ToString(dr["resultscoreslab"].ToString()),
                                resultfourslabslno = Convert.ToString(dr["resultfourslabslno"].ToString()),
                                resultsixslabslno = Convert.ToString(dr["resultsixslabslno"].ToString()),
                                resultscoreslabslno = Convert.ToString(dr["resultscoreslabslno"].ToString()),


                                PredictionResultToss = Convert.ToBoolean(dr["PredictionResultToss"].ToString()),
                                PredictionResultBat = Convert.ToBoolean(dr["PredictionResultBat"].ToString()),
                                PredictionResultFour = Convert.ToBoolean(dr["PredictionResultFour"].ToString()),
                                PredictionResultSix = Convert.ToBoolean(dr["PredictionResultSix"].ToString()),
                                PredictionResultScor = Convert.ToBoolean(dr["PredictionResultScor"].ToString()),
                                PredictionResultFourPoint = Convert.ToString(dr["PredictionResultFourPoint"].ToString()),
                                PredictionResultsixPoint = Convert.ToString(dr["PredictionResultsixPoint"].ToString()),
                                PredictionResultscorPoint = Convert.ToString(dr["PredictionResultscorPoint"].ToString()),
                                PredictionResultTossPoint = Convert.ToString(dr["PredictionResultTossPoint"].ToString()),
                                PredictionResultBatPoint = Convert.ToString(dr["PredictionResultBatPoint"].ToString()),
                                IsAbandoned = Convert.ToString(dr["IsAbandoned"].ToString()),
                                IsOpen = Convert.ToString(dr["IsOpen"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ListMatchSummaryExpertOpenionLists
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