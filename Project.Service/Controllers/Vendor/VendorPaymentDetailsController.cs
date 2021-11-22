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
    public class VendorPaymentDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetVendorPayment")]
        public HttpResponseMessage GetDetails(GetVendorPaymentDetailsList ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<VendorPaymentDetailss> alldcr = new List<VendorPaymentDetailss>();
                    List<VendorPaymentDetails> alldcr1 = new List<VendorPaymentDetails>();

                    var dr = g2.return_dr("GetVendorPaymentdetails '" + ula.CIN + "','" + ula.fromdate + "','" + ula.todate + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new VendorPaymentDetails
                            {
                                slno = Convert.ToInt32(dr["slno"].ToString()),
                                Receipt = Convert.ToString(dr["Receipt"].ToString()),
                                Voucherdt = Convert.ToString(dr["Voucherdt"].ToString()),
                                payamt = Convert.ToDecimal(dr["payamount"].ToString()),
                                status = Convert.ToString(dr["stat"].ToString()),
                                statusflag = Convert.ToInt32(dr["stat1"].ToString()),
                                totalamt = Convert.ToDecimal(dr["totalamt"].ToString()),
                                intamt = Convert.ToDecimal(dr["intamt"].ToString()),
                                transactionid = Convert.ToString(dr["TRANSID"].ToString()),
                                retry = Convert.ToBoolean(dr["retry"].ToString()),
                                paymentmode = Convert.ToString(dr["PaymentMode"].ToString()),
                                paymenttype = Convert.ToString(dr["PaymentType"].ToString()),
                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new VendorPaymentDetailss
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
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}