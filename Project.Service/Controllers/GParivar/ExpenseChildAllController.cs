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
    public class ExpenseChildAllController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getExpenseChildAll")]
        public HttpResponseMessage GetDetails(ListofExpenseChildAll ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ExpenseChildAlls> alldcr = new List<ExpenseChildAlls>();
                    List<ExpenseChildAll> alldcr1 = new List<ExpenseChildAll>();
                    var dr = g1.return_dr("ExpenseChildAll '" + ula.fromdate + "','" + ula.todate + "','" + ula.CIN + "','" + ula.Category + "'," + ula.ledgerid+','+ula.branchid);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ExpenseChildAll
                            {
                                name = Convert.ToString(dr["name"]),
                                suppliername = Convert.ToString(dr["SupName1"]),
                                amount = Convert.ToString(dr["amt"]),
                            link = Convert.ToString(dr["SlNo"])=="0" ? "" : WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "/Party-Ledger-Report3.aspx?partyid=" + Convert.ToString(dr["SlNo"]) + "&fromdate=" + dateformat(ula.fromdate) + "&todate=" + dateformat(ula.todate) + "&branchid="+ Convert.ToString(dr["branchid"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ExpenseChildAlls
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
        public string dateformat(string input)
        {
            string output = string.Empty;

            string[] words = input.Split('/');

            if (words.Length == 3)
            {
                output = words[1] + "/" + words[0] + "/" + words[2];
            }

            return output;
        }

    }
}