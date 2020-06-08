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
    public class ActiveDealerDeviceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getActiveDevice")]
        public HttpResponseMessage GetAllUserdetails(ListofActiveDealerDevice ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<ActiveDealerDevices> alldcr = new List<ActiveDealerDevices>();
                    List<ActiveDealerDevice> alldcr1 = new List<ActiveDealerDevice>();
                    var dr = g1.return_dr("CurrentActiveDevice '" + ula.CIN + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ActiveDealerDevice
                            {
                                DeviceType = Convert.ToString(dr["DeviceType"].ToString()),
                                ModalType = Convert.ToString(dr["ModalType"].ToString()),
                                DeviceId = Convert.ToString(dr["deviceid"].ToString()),
                                LastAcccessTime = Convert.ToString(dr["lstaccess"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ActiveDealerDevices
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}