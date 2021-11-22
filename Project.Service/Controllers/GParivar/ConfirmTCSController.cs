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
    public class ConfirmTCSController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [Filters.ValidateModel]
        [Route("api/confirmtcsdata")]
        public HttpResponseMessage GetDetails(ListofConfirmTcs ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    List<ConfirmTcss> alldcr = new List<ConfirmTcss>();
                    List<ConfirmTcs> alldcr1 = new List<ConfirmTcs>();
                    var dr = g1.return_dr("TDSTCSApplicabilityAddApp '" + ula.CIN + "',"+ula.turnover+",'"+ula.pan+"','"+ula.designation+"','"+ula.emailid+"'");
                    if (dr.HasRows)
                    {
                        
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "We taken your request Sucessfully!!!!"), Encoding.UTF8, "application/json");

                        return response;

                    
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Already request Taken or Invalid CIN No.!!!!!!!!!!"), Encoding.UTF8, "application/json");

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