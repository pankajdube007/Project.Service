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
    public class HighDateForExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorhighestdaysandledgeragingdownload")]
        public HttpResponseMessage GetDetails(ListsofHighDateForExecutive ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<HighDateForExecutiveLists> alldcr = new List<HighDateForExecutiveLists>();
                    List<HighDateForExecutiveList> alldcr1 = new List<HighDateForExecutiveList>();
                    var dr = g1.return_dr("App_GetHighDateForExecutive " + ula.Id + ",'" + ula.Category + "','" + ula.FromDate + "','" + ula.ToDate + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new HighDateForExecutiveList
                            {
                               
                                highdays = Convert.ToString(dr["highdays"]),
                               
                                ledgerdownload = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + Convert.ToString(dr["ledgerdownload"]),
                                agingdownload = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + Convert.ToString(dr["agingdownload"]),
                                PurchaseLedgerAmt = Convert.ToString(dr["PurchaseLedgerAmt"].ToString()),
                                SaleLedgerAmt = Convert.ToString(dr["SalesPartyLedgerAmt"].ToString()),
                                Diffrence = Convert.ToString(dr["diff"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new HighDateForExecutiveLists
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