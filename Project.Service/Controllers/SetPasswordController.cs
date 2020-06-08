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
    public class SetPasswordController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/SetPassword")]
        public HttpResponseMessage Getdetails(SetPasswordAction ula)
        {
            string errormsg = string.Empty;
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<SetPassword> alldcr = new List<SetPassword>();

                    var dr = g2.return_dr("App_CheckOldPassword '" + ula.CIN + "','" + ula.Category + "','" + ula.OldPassword + "'");

                    if (dr.HasRows)
                    {
                        int row1 = g2.ExecDB("exec App_SetPassword '" + ula.CIN + "','" + ula.Category + "','" + ula.NewPassword + "'");
                        if (row1 == 0)
                        {
                            errormsg = "Something Went Wrong.";
                        }

                        g2.close_connection();
                        alldcr.Add(new SetPassword
                        {
                            result = true,
                            message = errormsg,
                            servertime = DateTime.Now.ToString(),
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
                        response.Content = new StringContent(cm.StatusTime(false, "Old Password Not Matched Matched"), Encoding.UTF8, "application/json");

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