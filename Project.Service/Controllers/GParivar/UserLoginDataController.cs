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
    public class UserLoginDataController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [Filters.ValidateModel]
        [Route("api/userdataadd")]
        public HttpResponseMessage GetDetails(ListofUserLoginData ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {

                    var dr = g1.return_dr("Appdailylogsadd '" + ula.AppType + "','" + ula.CIN + "','" + ula.deviceid + "','" + ula.DeviceType + "','" + ula.AppVersion + "','" + ula.OSVersion + "','" + ula.Apphittime
                        + "','" + ula.pooswooshid + "','" + ula.IP + "','" + ula.Lat + "','" + ula.lng + "','" + ula.ModalType + "','"+ula.address+"'");
                    if (dr.HasRows)
                    {

                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Data added Sucessfully!!!!"), Encoding.UTF8, "application/json");

                        return response;


                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Something went wrong!!!!!!!!!!"), Encoding.UTF8, "application/json");

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