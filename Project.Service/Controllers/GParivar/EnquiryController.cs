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
    public class EnquiryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/SendEnquiry")]
        public HttpResponseMessage GetDetails(ListofEnquiryAction ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    string body = string.Empty;
                    string Subject = string.Empty;
                    string Email = string.Empty;

                    List<Enquirys> alldcr = new List<Enquirys>();

                    var dr = g2.return_dt("App_Enquiry " + ula.CIN + "," + ula.Subject + ",'" + ula.EquiryText + "'");
                    if (dr.Rows.Count > 0)
                    {
                        g2.close_connection();
                        Email = ula.Email;
                        // Email = "sanchit.goldmedal@gmail.com";
                        body = "Thanks You enquiring for '" + ula.EquiryText + "' with Goldmedal Electrical Pvt. Ltd., Your Ticket No. " + dr.Rows[0]["Slno"].ToString() + " for Future Assistant. Goldmedal Team will Contact You Soon..";
                        Subject = "Re:" + dr.Rows[0]["SubjectName"].ToString();
                        g2.sendmail(Email, body, Subject, "GoldMedal", "File");
                        alldcr.Add(new Enquirys
                        {
                            result = true,
                            servertime = DateTime.Now.ToString(),
                            message = "",
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
                        response.Content = new StringContent(cm.StatusTime(true, "Party or CIN Not Available, Please contact to Administrator!!!!"), Encoding.UTF8, "application/json");

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