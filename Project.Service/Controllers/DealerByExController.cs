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
    public class DealerByExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDealerByEx")]
        public HttpResponseMessage GetDetails(DealerByExAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.slno != 0)
            {
                try
                {
                    string data1;

                    List<DealerByExs> alldcr = new List<DealerByExs>();
                    List<DealerByEx> alldcr1 = new List<DealerByEx>();
                    var dr = g1.return_dt("Appdealerselectpo2 " + ula.branchid + "," + ula.slno + ",'" + ula.Category + "'");

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new DealerByEx
                            {
                                cin = Convert.ToString(dr.Rows[i]["SLNo"]),
                                Exid = Convert.ToInt32(dr.Rows[i]["salesexid"]),
                                dealnm = dr.Rows[i]["dealnm"].ToString(),
                                dealnm2 = dr.Rows[i]["dealnm2"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DealerByExs
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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