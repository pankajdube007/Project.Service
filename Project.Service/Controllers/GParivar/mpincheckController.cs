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
    public class mpincheckController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/mpinchecks")]
        public HttpResponseMessage GetDetails(mpinchecklist ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    List<mpinchecks> alldcr = new List<mpinchecks>();
                    List<mpincheck> alldcr1 = new List<mpincheck>();

                    string data1;

                    var val = g2.return_dt("mpincheck '" + ula.newmpin + "','" + ula.deviceid + "','" + ula.appid + "','" + ula.CIN + "'");

                    if (val.Rows[0]["out1"].ToString() == "available")
                    {
                        alldcr1.Add(new mpincheck
                        {
                            isBlock = false,
                            isForcedLogout = Convert.ToBoolean(val.Rows[0]["Isforcedlogout"]),
                        });

                        g2.close_connection();
                        alldcr.Add(new mpinchecks
                        {
                            result = true,
                            message = "Available mpin.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                        //g2.close_connection();

                        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //response.Content = new StringContent(cm.StatusTime(true, "Mpin available."), Encoding.UTF8, "application/json");

                        //return response;
                    }
                    else if (val.Rows[0]["out1"].ToString() == "unavailable")
                    {
                        alldcr1.Add(new mpincheck
                        {
                            isBlock = false,
                            isForcedLogout = Convert.ToBoolean(val.Rows[0]["Isforcedlogout"]),
                        });

                        g2.close_connection();
                        alldcr.Add(new mpinchecks
                        {
                            result = false,
                            message = "Unavailable mpin.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;

                        //g2.close_connection();

                        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        //response.Content = new StringContent(cm.StatusTime(false, "Mpin unavailable."), Encoding.UTF8, "application/json");

                        //return response;
                    }
                    else if (val.Rows[0]["out1"].ToString() == "valid")
                    {
                        alldcr1.Add(new mpincheck
                        {
                            isBlock = false,
                            isForcedLogout = Convert.ToBoolean(val.Rows[0]["Isforcedlogout"]),
                        });

                        g2.close_connection();
                        alldcr.Add(new mpinchecks
                        {
                            result = true,
                            message = "Valid mpin.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else if (val.Rows[0]["out1"].ToString() == "invalid")
                    {
                        alldcr1.Add(new mpincheck
                        {
                            isBlock = false,
                            isForcedLogout = Convert.ToBoolean(val.Rows[0]["Isforcedlogout"]),
                        });

                        g2.close_connection();
                        alldcr.Add(new mpinchecks
                        {
                            result = false,
                            message = "Invalid mpin.",
                            servertime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
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
                        response.Content = new StringContent(cm.StatusTime(false, "Invalid mpin."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}