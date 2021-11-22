using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Models
{
    public class TodayPaymentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTodayPayment")]
        public HttpResponseMessage GetDetails(ListofTodayPayment ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ClientSecret != "")
            {
                try
                {
                    string data1;

                    List<TodayPayments> alldcr = new List<TodayPayments>();
                    List<TodayPayment> alldcr1 = new List<TodayPayment>();

                    var dr = g1.return_dr("TodayPaymentReportManagement '"+ula.CIN+"','"+ula.Category+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TodayPayment
                            {
                                today = Convert.ToString(dr["TodayPayment"].ToString()),
                                monthly = Convert.ToString(dr["MonthlyPayment"].ToString()),
                                quarterly = Convert.ToString(dr["QuarterlyPayment"].ToString()),
                                yearly = Convert.ToString(dr["YearlyPayment"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodayPayments
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
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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