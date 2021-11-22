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
    public class AppthirdpartypaymentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/vendorwisethirdpartypayment")]
        public HttpResponseMessage GetDetails(ListofAppthirdpartypayment ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<AppthirdpartypaymentLists> alldcr = new List<AppthirdpartypaymentLists>();
                    List<AppthirdpartypaymentList> alldcr1 = new List<AppthirdpartypaymentList>();
                    var dr = g1.return_dr("AppThirdPartyPayment_PurchaseOutstandingSelect '" + ula.CIN + "','" + ula.Category + "','" + ula.Division + "','" + ula.Date + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new AppthirdpartypaymentList
                            {

                                Type = Convert.ToString(dr["Type"]),
                                TypeID = Convert.ToString(dr["TypeID"].ToString()),
                                SLNo = Convert.ToString(dr["SLNo"].ToString()),
                                refno = Convert.ToString(dr["refno"].ToString()),
                                date = Convert.ToString(dr["date"]),
                                displayinvoice = Convert.ToString(dr["displayinvoice"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                descrpt = Convert.ToString(dr["descrpt"].ToString()),
                                duedays = Convert.ToString(dr["duedays"].ToString()),
                                invoiceamt = Convert.ToString(dr["invoiceamt"].ToString()),
                                balanceamt = Convert.ToString(dr["balanceamt"].ToString()),
                                intper = Convert.ToString(dr["intper"].ToString()),
                                intamt = Convert.ToString(dr["intamt"].ToString()),
                                netpaybleamt = Convert.ToString(dr["netpaybleamt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AppthirdpartypaymentLists
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