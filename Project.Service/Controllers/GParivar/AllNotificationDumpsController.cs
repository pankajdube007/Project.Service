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
    public class AllNotificationDumpsController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/AllNotifyMsg")]
        public HttpResponseMessage NotificMsg(AllNotificationDumpAction an)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (cm.Validate(an.uniquekey))
            {
                if (an.flag == 1)
                {
                    try
                    {
                        string data1;
                        List<Notifics> alldcr = new List<Notifics>();
                        List<Notific> alldcr1 = new List<Notific>();
                        var dr = g1.return_dr("getmobilenotific " + an.userid + "," + an.count + "");
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                alldcr1.Add(new Notific
                                {
                                    slno = Convert.ToInt32(dr["slno"].ToString()),
                                    msg = Convert.ToString(dr["notification"].ToString()),
                                    sendingstamp = Convert.ToString(dr["createdt"].ToString()),
                                    read = Convert.ToBoolean(dr["flag"].ToString()),

                                });
                            }
                            g1.close_connection();
                            alldcr.Add(new Notifics
                            {
                                result = "True",
                                message = "",
                                servertime = DateTime.Now.ToString(),
                                data = alldcr1,
                            });
                            data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(data1, Encoding.Unicode);
                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "You have not a single Notification"), Encoding.Unicode);
                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.Unicode);
                        return response;
                    }
                }
                else if (an.flag == 2)
                {
                    try
                    {
                        int row = g1.ExecDB("exec notificationUpdate '" + an.slno + "'");
                        if (row > 0)
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(true, "Sucessfully Updated"), Encoding.Unicode);

                            return response;
                        }
                        else
                        {
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                            response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong"), Encoding.Unicode);

                            return response;
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.Unicode);

                        return response;
                    }
                }
                else
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Please flag should be 1 or 2"), Encoding.Unicode);

                    return response;
                }
            }

            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.Unicode);
                return response;
            }
        }
    }
}

