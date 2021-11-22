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
    public class RaiseDisputeController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/RaiseDispute")]
        public HttpResponseMessage GetDetails(ListsofRaiseDispute ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != "" && ula.transactionid != "" && ula.disputeid != 0)
            {
                try
                {
                    string data1;

                    List<RaiseDisputes> alldcr = new List<RaiseDisputes>();
                    List<RaiseDispute> alldcr1 = new List<RaiseDispute>();
                    var val = g2.reterive_val("insertPaymentDisputeMaster " + ula.disputeid + ",'" + ula.details + "','" + ula.CIN + "','" + ula.transactionid + "'");

                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        
                        alldcr1.Add(new RaiseDispute
                        {
                            disputeno = val
                        });


                        g2.close_connection();
                        alldcr.Add(new RaiseDisputes
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
                        response.Content = new StringContent(cm.StatusTime(true, "Oops! Dispute not logged, may be transaction id is incorrect!!!!!"), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}