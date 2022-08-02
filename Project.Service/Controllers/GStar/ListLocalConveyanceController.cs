using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using NLog;

namespace Project.Service.Controllers.GStar
{
    public class ListLocalConveyanceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getListLocalConveyance")]
        public HttpResponseMessage GetDetails(ListofListLocalConveyance ula)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            DataConnectionTrans g1 = new DataConnectionTrans();
            GoldMedia _goldMedia = new GoldMedia();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<GetLocalConveyanceLists> alldcr = new List<GetLocalConveyanceLists>();
                    List<GetLocalConveyanceList> alldcr1 = new List<GetLocalConveyanceList>();
                    var dr = g1.return_dr("GetListLocalConveyance '" + ula.ExId + "','" + ula.fdate + "','" + ula.tdate + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetLocalConveyanceList
                            {
                                slno = Convert.ToString(dr["slno"].ToString()),
                                execid = Convert.ToString(dr["execid"].ToString()),
                                trvldt = Convert.ToString(dr["trvldt"].ToString()),
                                TotalKms = Convert.ToString(dr["TotalKms"].ToString()),
                                PersonalKms = Convert.ToString(dr["PersonalKms"].ToString()),
                                ClaimableKms = Convert.ToString(dr["ClaimableKms"].ToString()),
                                claimableamt = Convert.ToString(dr["claimableamt"].ToString()),
                                ModeofTravel = Convert.ToString(dr["ModeofTravel"].ToString()),
                                SelfConveyance = Convert.ToString(dr["SelfConveyance"].ToString()),
                                status = Convert.ToString(dr["status"].ToString()),
                                TotalApproved = Convert.ToString(dr["TotalApproved"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetLocalConveyanceLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    g1.close_connection();
                    logger.Error($"Error occours! API : {RequestContext.Url}  Input : '{JsonConvert.SerializeObject(ula)}' Exception : {ex}");
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!! CIN:" + ula.ExId), Encoding.UTF8, "application/json");

                    return response;

                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");

                    //return response;
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