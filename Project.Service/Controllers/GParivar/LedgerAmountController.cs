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
    public class LedgerAmountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getLedgerAmount")]
        public HttpResponseMessage GetDetails(ListsofLedgerAmountAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<LedgerAmounts> alldcr = new List<LedgerAmounts>();
                    List<LedgerAmount> alldcr1 = new List<LedgerAmount>();
                    var dr = g1.return_dr("App_CreditLedgerDetails '" + ula.CIN + "','" + ula.FinYear + "','" + ula.searchtxt + "',"+ula.ReportType+","+ula.ReportValue);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new LedgerAmount
                            {
                                slno = Convert.ToInt32(dr["SlNo"]),
                                referenceno = Convert.ToString(dr["referenceno"]),
                                date = Convert.ToString(dr["date"].ToString()),
                                amount = Convert.ToString(dr["totalamount"]),
                                ledgerdec = Convert.ToString(dr["LedgerDesc"]),
                                type = Convert.ToString(dr["typo"]),
                                url = string.IsNullOrEmpty(Convert.ToString(dr["url"]))? "" : WebConfigurationManager.AppSettings["ErpUrl"].ToString()+  Convert.ToString(dr["url"]),
                                download= WebConfigurationManager.AppSettings["ErpUrl"].ToString()+ Convert.ToString(dr["download"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new LedgerAmounts
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