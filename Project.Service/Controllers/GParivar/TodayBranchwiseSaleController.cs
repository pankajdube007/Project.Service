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
    public class TodayBranchwiseSaleController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetTodaySaleBranchwise")]
        public HttpResponseMessage GetDetails(ListofTodayBranchwiseSale ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.Category != "")
            {
                try
                {
                    string data1;

                    List<TodayBranchwiseSales> alldcr = new List<TodayBranchwiseSales>();
                    List<TodayBranchwiseSale> alldcr1 = new List<TodayBranchwiseSale>();

                    var dr = g1.return_dr("TodayInvoiceReportManagementBranchwsise '"+ula.fromdate+"','"+ula.todate+ "','" + ula.CIN + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read()) 
                        {
                            alldcr1.Add(new TodayBranchwiseSale
                            {
                                branchid = Convert.ToString(dr["homebranchid"].ToString()),
                                branchnm = Convert.ToString(dr["locnm"].ToString()),
                                amount = Convert.ToString(dr["saleamt"].ToString()),
                                payment = Convert.ToString(dr["payment"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TodayBranchwiseSales
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