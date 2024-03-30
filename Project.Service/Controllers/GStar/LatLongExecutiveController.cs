using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
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
    public class LatLongExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/AddLatLong")]
        public HttpResponseMessage GetDetails(ListofLatLongEx ula)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            try
            {
                string data1;

                List<LatLongExs> alldcr = new List<LatLongExs>();
                List<LatLongEx> alldcr1 = new List<LatLongEx>();
                var dr = g2.return_dr("ApplatlongInsert " + ula.ExId + ",'" + ula.Lat + "','" + ula.Long + "','" + ula.Date + "','"+
                    ula.Distance+"','"+ula.BatteryStraingth+"','"+ula.SignalStraingth+"','"+ula.DevoiceIsOnline+"','"+ula.Speed+"','"+ula.EmpType + "'");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new LatLongEx
                        {
                            totalcount = Convert.ToInt32(dr["count1"]),
                            LastInserted = dr["createdt"].ToString(),
                        });
                    }
                    g2.close_connection();
                    alldcr.Add(new LatLongExs
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
                    //logger.Warn($"Error occours! API : {RequestContext.Url}  Input : '{JsonConvert.SerializeObject(ula)}'");
                    logger.Warn($"Error occours! API : {RequestContext.Url} ");
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Data not Inserted!!!! ExecutiveId:" + ula.ExId), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            catch (Exception ex)
            {
                g2.close_connection();
                //logger.Error($"Error occours! API : {RequestContext.Url}  Input : '{JsonConvert.SerializeObject(ula)}' Exception : {ex}");
                logger.Error($"Error occours! API : {RequestContext.Url}   Exception : {ex}");
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!! ExecutiveId:" + ula.ExId), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}