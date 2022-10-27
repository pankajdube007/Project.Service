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
    public class DivisionBrachWiseSaleExpenseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getDivisionBrachWiseSaleExpense")]
        public HttpResponseMessage GetDetails(ListofDivisionBrachWiseSaleExpense ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<DivisionBrachWiseSaleExpenseLists> alldcr = new List<DivisionBrachWiseSaleExpenseLists>();
                    List<DivisionBrachWiseSaleExpenseList> alldcr1 = new List<DivisionBrachWiseSaleExpenseList>();

                    var dr = g1.return_dr("divisionbrachsaleexpan '" + ula.CIN + "','" + ula.Category + "','" + ula.branchid + "','" + ula.divisiononid + "','" + ula.finyear + "','" + ula.Qtr + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new DivisionBrachWiseSaleExpenseList
                            {
                                homebranchid = Convert.ToString(dr["homebranchid"].ToString()),
                                HomeBranch = Convert.ToString(dr["HomeBranch"].ToString()),
                                Sale = Convert.ToString(dr["Sale"].ToString()),
                                DivId = Convert.ToString(dr["DivId"].ToString()),
                                DivisionName = Convert.ToString(dr["DivisionName"].ToString()),
                                salexp = Convert.ToString(dr["salexp"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new DivisionBrachWiseSaleExpenseLists
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