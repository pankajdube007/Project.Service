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
    public class GetFreePayPaymentDetailController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetFreePayPaymentDetail")]
        public HttpResponseMessage GetDetails(GetFreePayPaymentDetailList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != 0)
            {
                try
                {
                    string data1;

                    List<GetFreePayPaymentDetails> alldcr = new List<GetFreePayPaymentDetails>();
                    List<GetFreePayPaymentDetail> alldcr1 = new List<GetFreePayPaymentDetail>();

                    var dr = g1.return_dr("spGetFreePayPaymentDetails " + ula.slno);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetFreePayPaymentDetail
                            {
                                totalamt = Convert.ToDecimal(dr["totalamt"].ToString()),
                                savedamt = Convert.ToDecimal(dr["savedamt"].ToString()),
                                adjustedamt = Convert.ToDecimal(dr["adjustedamt"].ToString()),
                            });
                        }
                        g1.close_connection();

                        alldcr.Add(new GetFreePayPaymentDetails
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
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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