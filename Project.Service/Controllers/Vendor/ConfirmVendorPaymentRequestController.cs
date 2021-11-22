using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Project.Service.Controllers
{
    public class ConfirmVendorPaymentRequestController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ConfirmVendorPaymentRequest")]
        public HttpResponseMessage GetDetails(ListofConfirmVendorPayment ula)
        {
            Common cm = new Common();
            DataConnectionTrans g2 = new DataConnectionTrans();
            var request = Request;

            if (ula.CIN != "")
            {
                
                                var dr = g2.return_dr("VendorConfirmPaymentRequest '" + ula.transactionid + "','" + ula.CIN + "',"+ula.bankid);

                                if (dr.HasRows)
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(true, "Payment Processed successfully!!!!!!!!"), Encoding.UTF8, "application/json");
                                    return response;
                                }
                                else
                                {
                                    g2.close_connection();
                                    HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");
                                    return response;
                                }
                          

            }
            else
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please enter valid cin."), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}