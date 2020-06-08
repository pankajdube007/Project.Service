using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class FreePaySucessController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/FreePayTranstionsSuccess")]
        public HttpResponseMessage GetDetails(listsofFreePaySucess ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    //  string data1;

                    //List<DCRExecutives> alldcr = new List<DCRExecutives>();
                    //List<DCRExecutive> alldcr1 = new List<DCRExecutive>();

                    var dr = g2.return_dr("FreepayTranssucess '" + ula.transactionid + "','" + ula.freepaytransactionid + "','" + ula.reasonoffailed 
                        + "','" + ula.statuscode + "','"+ula.devicetype+"','"+ula.deviceid+"'");

                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Success!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Something Went Wrong,Transtion not updated!!!"), Encoding.UTF8, "application/json");

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