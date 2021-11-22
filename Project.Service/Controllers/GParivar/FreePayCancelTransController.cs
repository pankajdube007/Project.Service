using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class FreePayCancelTransController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/CancelFreePayTranstions")]
        public HttpResponseMessage GetDetails(ListsofFreePayCancelTrans ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.transactionid != ""  && ula.CIN != "")
            {
                try
                {
                    var dr = g2.return_dr("FreepayTransCancel '" + ula.transactionid + "','" + ula.reasonoffailed + "'");

                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Transaction Cancelled!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not cancelled!!!"), Encoding.UTF8, "application/json");

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