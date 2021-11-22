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
    public class mpininsertController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/mpinadds")]
        public HttpResponseMessage GetDetails(mpininsertlist ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<mpininserts> alldcr = new List<mpininserts>();
                    List<mpininsert> alldcr1 = new List<mpininsert>();

                    string val = g2.reterive_val(string.Format("exec mpinadd '{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}'", ula.category, ula.oldmpin, ula.newmpin, ula.deviceId, ula.appid, 0, 0, ula.CIN));

                    if (!string.IsNullOrEmpty(val))
                    {
                        //if (val == "DUPLICATE")
                        //{
                        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //    response.Content = new StringContent(cm.StatusTime(false, "Duplicate mpin."), Encoding.UTF8, "application/json");

                        //    return response;
                        //}
                        //else if (val == "WRONG")
                        //{
                        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //    response.Content = new StringContent(cm.StatusTime(false, "Wrong mpin."), Encoding.UTF8, "application/json");

                        //    return response;
                        //}
                        if (val == "mpin set sucessfully.")
                        {
                            alldcr1.Add(new mpininsert
                            {
                                output = val
                            });

                            g2.close_connection();
                            alldcr.Add(new mpininserts
                            {
                                result = true,
                                message = val,
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
                            //pushk start
                            alldcr1.Add(new mpininsert
                            {
                                output = val
                            });
                            g2.close_connection();
                            alldcr.Add(new mpininserts
                            {
                                result = false,
                                message = val,
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });

                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(data1, Encoding.UTF8, "application/json");
                            //pushka end

                            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            //response.Content = new StringContent(cm.StatusTime(false, val), Encoding.UTF8, "application/json");

                            return response;
                        }
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}