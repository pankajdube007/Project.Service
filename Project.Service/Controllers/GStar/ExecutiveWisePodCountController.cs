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

namespace Project.Service.Controllers
{
    public class ExecutiveWisePodCountController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecutiveWisePodCount")]
        public HttpResponseMessage GetPodCount(ExecutiveWisePodCount ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.ExId != 0)
            {

                try
                {
                    string data1;

                    List<GetPodCount> alldcr = new List<GetPodCount>();
                    List<PodCountData> alldcr1 = new List<PodCountData>();

                    var dr = g2.return_dr("execwisepodtcount'" + ula.ExId + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.Cin + "','" + ula.Hierarchy + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PodCountData
                            {
                                SlNo = Convert.ToInt32(dr["SlNo"].ToString()),
                                ExecutiveName = Convert.ToString(dr["name"].ToString()),
                                TotalCount = Convert.ToInt32(dr["totalcount"].ToString()),
                                PodCount = Convert.ToInt32(dr["podcnt"].ToString()),
                                Pending = Convert.ToInt32(dr["Pending"].ToString()),

                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new GetPodCount
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
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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