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
    public class AppPaymentController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getAppPayment")]
        public HttpResponseMessage GetDetails(ListApppayment ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ApppaymentLists> alldcr = new List<ApppaymentLists>();
                    List<ApppaymentList> alldcr1 = new List<ApppaymentList>();

                    var dr = g1.return_dr("getAppPayment '" + ula.Cat + "','" + ula.FromDate + "','" + ula.Todate + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ApppaymentList
                            {

                                slno = "",
                                BANKTDATE ="",
                                AppAmt = Convert.ToString(dr["AppAmt"].ToString()),
                                Voucherdt = Convert.ToString(dr["Voucherdt"].ToString()),
                                ClearanceAmt = Convert.ToString(dr["ClearanceAmt"].ToString()),
                                PendingAmt = Convert.ToString(dr["PendingAmt"].ToString()),
                                ChequeReturn =Convert.ToString(dr["ChequeReturn"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ApppaymentLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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