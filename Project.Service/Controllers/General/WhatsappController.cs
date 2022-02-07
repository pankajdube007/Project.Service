using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.General;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers.General
{
    public class WhatsappController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Whatsapp")]
        public HttpResponseMessage GetDetails(ListsofWhatsapp ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.mobileno != null && ula.msg != null && ula.module != null)
            {
                try
                {
                    var dr = g1.return_dt("addwhatsapp '" + ula.mobileno + "','"+ula.msg+"','"  + ula.date + "','" + ula.imgurl + "','" + ula.module + "','" + ula.uid+"'");

                    if (dr.Rows.Count > 0)
                    {

                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Added Sucessfully!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, Please Try Again After Sometime!!!!"), Encoding.UTF8, "application/json");

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