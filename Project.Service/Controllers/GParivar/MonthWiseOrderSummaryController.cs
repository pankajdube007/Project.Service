using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class MonthWiseOrderSummaryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getmonthwiseordersummary")]
        public HttpResponseMessage GetDetails(ListofMonthWiseOrderSummary ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<MonthWiseOrderSummarys> alldcr = new List<MonthWiseOrderSummarys>();
                    List<MonthWiseOrderSummary> alldcr1 = new List<MonthWiseOrderSummary>();
                   
                    var dr = g1.return_dr("getMonthWiseOrderItemsapi '" + ula.PatyId + "','" + ula.DivID + "','" + ula.Fromdate + "','" + ula.Todate + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new MonthWiseOrderSummary
                            {
                                ItemCode = Convert.ToString(dr["ProductCode1"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                TotalOrder = Convert.ToString(dr["tot_ord"].ToString()),
                                TotalReceived = Convert.ToString(dr["tot_rec"].ToString()),
                                OpeningBal = Convert.ToString(dr["OpeningBal"].ToString()),
                                Bal = Convert.ToString(dr["balance"].ToString()),
                                Unit = Convert.ToString(dr["unitnm"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new MonthWiseOrderSummarys
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data"), Encoding.UTF8, "application/json");

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