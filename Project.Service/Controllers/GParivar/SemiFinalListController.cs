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
    public class SemiFinalListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetSemiFinalList")]
        public HttpResponseMessage GetDetails(ListSemiFinalList ula)
        {
            DataConection g1 = new DataConection();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<SemiFinalLists> alldcr = new List<SemiFinalLists>();
                    List<SemiFinalList> alldcr1 = new List<SemiFinalList>();

                    var dr = g1.return_dr("SemifinalMatchSelectionList '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new SemiFinalList
                            {
                                teamid = Convert.ToInt32(dr["teamid"].ToString()),
                                team = Convert.ToString(dr["team"].ToString()),
                                partyid = Convert.ToInt32(dr["partyid"].ToString()),
                                typecat = Convert.ToInt32(dr["typeofparty"].ToString()),
                               
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new SemiFinalLists
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