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
    public class PaytmTransactionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/PaytmTransactionAdd")]
        public HttpResponseMessage GetDetails(ListPaytmTransaction ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.Mobile != "")
            {
                try
                {
                    string data1;

                    List<PaytmTransactions> alldcr = new List<PaytmTransactions>();
                    List<PaytmTransaction> alldcr1 = new List<PaytmTransaction>();
                    var dr = g2.return_dr("AddPayatmTransaction '" + ula.Mobile + "','" + ula.Amount + "'");

                    if (dr.HasRows)
                    {
                        alldcr1.Add(new PaytmTransaction
                        {
                            output = "Data Sucessfully inserted"
                        });

                        g2.close_connection();
                        alldcr.Add(new PaytmTransactions
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
                        response.Content = new StringContent(cm.StatusTime(false, "Not Created!!!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    // response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.Message), Encoding.UTF8, "application/json");
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