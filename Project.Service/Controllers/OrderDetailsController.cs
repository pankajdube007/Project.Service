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
    public class OrderDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetOrderDetails")]
        public HttpResponseMessage GetDetails(ListsofOrderDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<OrderDetailss> alldcr = new List<OrderDetailss>();
                List<OrderDetails> alldcr1 = new List<OrderDetails>();
                var dr = g1.return_dr("AppOrderDetails '" + ula.CIN + "','" + ula.FromDate + "','" + ula.ToDate + "'");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        alldcr1.Add(new OrderDetails
                        {
                            ponum = dr["ponum"].ToString(),
                            podt = dr["podt"].ToString(),
                            potime = dr["potime"].ToString(),
                            amount = dr["finaltotal"].ToString(),
                            logstatus = dr["logstatus"].ToString(),
                            orderstatus = dr["orderstatus"].ToString(),
                            orderurl = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "/salesorder-print.aspx?id=" + dr["SLNo"].ToString() + "&uniquekey=" + dr["uniquekey"].ToString() + "&brnchname=" + dr["branchid"].ToString(),
                        });
                    }
                    g1.close_connection();
                    alldcr.Add(new OrderDetailss
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
                    response.Content = new StringContent(cm.StatusTime(true, "Oops! Data not Available"), Encoding.UTF8, "application/json");

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
    }
}