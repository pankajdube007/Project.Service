using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Project.Service.Filters;
using Project.Service.Models.GStar;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Configuration;
using System.Web;

namespace Project.Service.Controllers.GStar
{
    public class NukkadMeetSMSController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/NukkadMeetSMSSend")]
        public HttpResponseMessage GetDetails(NukkadMeetSMSList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            WebClient web = new WebClient();
            byte[] bufData = null;
            if (ula.ExecId != 0)
            {
                try
                {
                    string data1;

                    List<NukkadMeetSMSS> alldcr = new List<NukkadMeetSMSS>();
                    List<NukkadMeetSMS> alldcr1 = new List<NukkadMeetSMS>();

                    string smsBody = HttpUtility.UrlEncode("Dear Business Partner, As you are aware, Finance Act 2020 had introduced Section 206C(1H) w.e.f 01.10.2020 - Requirement to collect TCS on sale of goods at 0.1% on sales consideration exceeding Rs.50 Lakhs in a financial year. We request you to kindly fill the form (Link Provided) at the earliest. https://erp.goldmedalindia.in/TDSTCSapplicability.aspx?UniqueKey=2  In case of any clarification (if required), please reach out to us at Aruna Gupta –aruna.gupta@goldmedalindia.com ( 022-42023000 Extn:3071 )-Team Goldmedal");

                    string url = "http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=" + "goldmedal&password=sd56jjaa"  + "&type=0&dlr=1&destination=" + ula.Mobile + "&source=GLDMDL&message=" + smsBody + "&entityid=1601100000000001629&tempid=1007756728420877008";
                    bufData = web.DownloadData(url);

                    //   var dr = g1.return_dr($"exec NukkadmeetmobileSearch {ula.Mobile} ");
                    //if (dr.HasRows)
                    //{

                        g1.close_connection();
                        alldcr.Add(new NukkadMeetSMSS
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = null,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    //}
                    //else
                    //{
                    //    g1.close_connection();
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    //    response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

                    //    return response;
                    //}
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