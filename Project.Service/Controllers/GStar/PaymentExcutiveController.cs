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
    public class PaymentExcutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPaymentExecutive")]
        public HttpResponseMessage GetDetails(ListsofPaymentExcutive ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<PaymentExcutives> alldcr = new List<PaymentExcutives>();
                    List<PaymentExcutive> alldcr1 = new List<PaymentExcutive>();
                    var dr = g1.return_dr("AppPaymentExcutive " + ula.ExId + ",'" + ula.FromDate + "','" + ula.ToDate + "'," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PaymentExcutive
                            {
                                division = dr["divisionnm"].ToString(),
                                payment = dr["payment"].ToString(),
                                target = dr["targetamt"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PaymentExcutives
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