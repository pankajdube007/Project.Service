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
    public class OrderDetailExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetOrderDetailsExecutive")]
        public HttpResponseMessage GetDetails(ListsofOrderDetailExecutives ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();

            try
            {
                string data1;

                List<OrderDetailExecutives> alldcr = new List<OrderDetailExecutives>();
                List<OrderDetailExecutive> alldcr1 = new List<OrderDetailExecutive>();
                List<OrderDetailExecutiveFinal> final = new List<OrderDetailExecutiveFinal>();
                var dr = g1.return_dt("AppOrderDetailseEX " + ula.ExId+",'" + ula.CIN + "','" + ula.FromDate + "','" + ula.ToDate + "','"+ula.Search+"',"+ Convert.ToBoolean(ula.Hierarchy)+","+ula.Index+","+ula.Count);

                bool more = false;

                if (dr.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.Index))
                    {
                        more = true;
                    }
                    else
                    {
                        more = false;
                    }
                    for (int i = 0; i < dr.Rows.Count; i++)
                    {
                        alldcr1.Add(new OrderDetailExecutive
                        {
                            partynm = dr.Rows[i]["displaynm"].ToString(),
                            ponum = dr.Rows[i]["ponum"].ToString(),
                            podt = dr.Rows[i]["podt"].ToString(),
                            potime = dr.Rows[i]["potime"].ToString(),
                            amount = dr.Rows[i]["finaltotal"].ToString(),
                            logstatus = dr.Rows[i]["logstatus"].ToString(),
                            orderstatus = dr.Rows[i]["orderstatus"].ToString(),
                            orderurl = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "/salesorder-print.aspx?id=" + dr.Rows[i]["SLNo"].ToString() + "&uniquekey=" + dr.Rows[i]["uniquekey"].ToString() + "&brnchname=" + dr.Rows[i]["branchid"].ToString(),
                        });
                    }
                    final.Add(new OrderDetailExecutiveFinal
                    {
                        Orderdata = alldcr1,
                        ismore = more
                    });
                    g1.close_connection();
                    alldcr.Add(new OrderDetailExecutives
                    {
                        result = true,
                        message = string.Empty,
                        servertime = DateTime.Now.ToString(),
                        data = final,
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