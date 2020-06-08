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
    public class PartySaleWorldCupController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetSaleWorldCup")]
        public HttpResponseMessage GetDetails(ListPartySaleWorldCup ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PartySaleWorldCupLists> alldcr = new List<PartySaleWorldCupLists>();
                    List<PartySaleWorldCupList> detail = new List<PartySaleWorldCupList>();
                    List<WorldcupImgList> img = new List<WorldcupImgList>();
                    List<WorldcupsaleFinal> final = new List<WorldcupsaleFinal>();


                    var dr = g1.return_dr("[appdealersaleforworldcup] '" + ula.CIN + "'");
                    var dr1 = g1.return_dr("[worldcupimg] ");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            detail.Add(new PartySaleWorldCupList
                            {
                                PartyName = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                Sale = Convert.ToString(dr["saleamt"].ToString()),
                               
                                AmountEarned = Convert.ToString(dr["earnedamt"].ToString()),
                                AmountEarnedPer = Convert.ToString(dr["earnedamtper"].ToString()),


                                AmountEarned1 = Convert.ToString(dr["earnedamt"].ToString()),
                                AmountEarnedPer1 = Convert.ToString(dr["earnedamtper"].ToString()),

                                ChancesToWin = Convert.ToString(dr["chancetowinamt"].ToString()),
                                ChancesToWinper = Convert.ToString(dr["chancetowinper"].ToString()),

                                MatchWon = Convert.ToString(dr["wonmatch"].ToString()),
                                MatchLost = Convert.ToString(dr["lostmatch"].ToString()),

                                MatchWon1 = Convert.ToString(dr["wonmatch"].ToString()),
                                MatchLost1 = Convert.ToString(dr["lostmatch"].ToString()),

                                MatchBal = Convert.ToString(dr["balmatch"].ToString()),
                                MatchBal1 = Convert.ToString(dr["balmatch"].ToString()),
                                totalmatch = Convert.ToString(dr["totmatch"].ToString()),
                                pdfurl = string.IsNullOrEmpty(dr["pdflink"].ToString().Trim(',')) ? "" : (baseurl + "worldcuppdf/" + dr["pdflink"].ToString().Trim(','))
                            });

                           

                        }
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                img.Add(new WorldcupImgList
                                {
                                    imgurl = string.IsNullOrEmpty(dr1["item"].ToString().Trim(',')) ? "" : (baseurl + "worldcuimage/" + dr1["item"].ToString().Trim(','))

                                });
                            }
                        }

                        final.Add(new WorldcupsaleFinal
                        {
                            detail = detail,
                            img = img,

                        });

                        g1.close_connection();
                        alldcr.Add(new PartySaleWorldCupLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = final,
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
