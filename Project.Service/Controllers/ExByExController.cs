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
    public class ExByExController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExByEx")]
        public HttpResponseMessage GetDetails(ExByExAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.slno != 0)
            {
                try
                {
                    string data1;

                    List<ExByExs> alldcr = new List<ExByExs>();
                    List<ExByEx> alldcr1 = new List<ExByEx>();
                    var dr = g1.return_dt("Appsalesexechilddetails " + ula.slno);

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new ExByEx
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"]),
                                salesexnm = dr.Rows[i]["salesexnm"].ToString(),
                                printnm = dr.Rows[i]["printnm"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExByExs
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