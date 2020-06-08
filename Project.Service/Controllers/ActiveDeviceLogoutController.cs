using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class ActiveDeviceLogoutController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/ActiveDeviceLogout")]
        public HttpResponseMessage GetAllUserdetails(ListofActiveDeviceLogout ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            WebClient web = new WebClient();
            byte[] bufData = null;
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                  
                    List<ActiveDeviceLogouts> alldcr = new List<ActiveDeviceLogouts>();
                    List<ActiveDeviceLogout> alldcr1 = new List<ActiveDeviceLogout>();
                    var newpwd = cm.GenerateRandomString(10);
                    var dr = g1.return_dt("CurrentActiveDeviceLogout '" + ula.CIN + "','"+newpwd+"'");


                    if (dr.Rows.Count>0)
                    {
                      //  bufData = web.DownloadData("http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=goldmedal&password=sd56jjaa&type=0&dlr=0&destination=" + "8981804211,9518957760" + "&source=GLDMDL&message=Your G-Parivar new password is:" + dr.Rows[0]["password"].ToString() + " and please reset your OTP." + "&url=KKKK%3E");
                     bufData = web.DownloadData("http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=goldmedal&password=sd56jjaa&type=0&dlr=0&destination=" + dr.Rows[0]["contact"].ToString() + "&source=GLDMDL&message=Your G-Parivar new password is:" + dr.Rows[0]["password"].ToString() + " and please reset your OTP." + "&url=KKKK%3E");


                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Logout Successfully!!!!!!"), Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(false, "Error or no active device available!!!!"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}