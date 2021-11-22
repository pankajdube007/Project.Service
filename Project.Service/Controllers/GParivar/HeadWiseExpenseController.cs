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
    public class HeadWiseExpenseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getManagementHeadwiseExpense")]
        public HttpResponseMessage GetDetails(ListofHeadWiseExpense ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<HeadWiseExpenses> alldcr = new List<HeadWiseExpenses>();
                    List<HeadWiseExpense> alldcr1 = new List<HeadWiseExpense>();
                    var dr = g1.return_dr("APPLedgerwiseExpanseReport '" + ula.fromdate + "','" + ula.todate + "'," + ula.BranchId + ",'" + ula.CIN + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new HeadWiseExpense
                            {
                                ledgerid = Convert.ToString(dr["paidtoledgnameid"]),
                                name = Convert.ToString(dr["paidtoledgname"]),
                                amount = Convert.ToString(dr["amount"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new HeadWiseExpenses
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