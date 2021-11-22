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
    public class TeamController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTeam")]
        public HttpResponseMessage GetDetails(ListTeam ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<TeamLists> alldcr = new List<TeamLists>();
                    List<TeamList> alldcr1 = new List<TeamList>();

                    var dr = g1.return_dr("getTeam '"+ula.CIN+"' ");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new TeamList
                            {
                                EventId = Convert.ToInt32(dr["sporteventid"].ToString()),
                                EventName = Convert.ToString(dr["EventName"].ToString()),
                                TeamId = Convert.ToInt32(dr["teamid"].ToString()),
                                TeamName = Convert.ToString(dr["team"].ToString()),
                                point = Convert.ToDecimal(dr["point"].ToString()),
                                isfinal = Convert.ToInt32(dr["isfinal"].ToString()),
                                issemifinal = Convert.ToInt32(dr["issemifinal"].ToString()),
                                iswinner = Convert.ToInt32(dr["iswinner"].ToString()),
                                ResultSemifinal = Convert.ToInt32(dr["ResultSemifinal"].ToString()),
                                ResultWinner = Convert.ToInt32(dr["ResultWinner"].ToString()),
                                isSelectionAvailable = Convert.ToString(dr["isSelectionAvailable"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TeamLists
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