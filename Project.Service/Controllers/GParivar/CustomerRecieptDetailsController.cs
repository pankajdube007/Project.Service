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
    public class CustomerRecieptDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesPaymentReportDetails")]
        public HttpResponseMessage GetDetails(CustomerReciptDetailsAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != string.Empty)
            {
                try
                {
                    string data1;

                    List<CustomerReciptDetails> alldcr = new List<CustomerReciptDetails>();
                    List<CustomerReciptDetail> alldcr1 = new List<CustomerReciptDetail>();

                    var dr = g1.return_dr("App_cusrcptcheckparticular " + ula.SlNo);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CustomerReciptDetail
                            {
                                Invoice = Convert.ToString(dr["invoice"].ToString()),
                                InvoiceDate = Convert.ToString(dr["invoicedate"].ToString()),
                                Type = Convert.ToString(dr["type"].ToString()),
                                AdjustedAmount = Convert.ToString(dr["adjustedamount"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CustomerReciptDetails
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
                        response.Content = new StringContent(cm.StatusTime(true, "Party or CIN Not Available, Please contact to Administrator!!!!"), Encoding.UTF8, "application/json");

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