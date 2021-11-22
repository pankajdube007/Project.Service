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
    public class GetVendProcNotificationController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorNotification")]
        public HttpResponseMessage GetDetails(ListofVendorNotification ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetVendorNotificationDataDetails> alldcr = new List<GetVendorNotificationDataDetails>();
                    List<GetVendorNotificationDataDetail> alldcr1 = new List<GetVendorNotificationDataDetail>();
                    var dr = g1.return_dr("dbo.spGetVendProcNotificationapp '" + ula.PartyID + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetVendorNotificationDataDetail
                            {
                                Message = Convert.ToString(dr["Message"].ToString()),
                                createdt = Convert.ToString(dr["createdt"].ToString()),
                                Status = Convert.ToInt32(dr["Status"].ToString()),
                                



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetVendorNotificationDataDetails
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