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
using System.Web.UI;

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
           // WebClient web = new WebClient();
           // byte[] bufData = null;
            if (ula.ExecId != 0)
            {
                try
                {
                    string data1;

                    List<NukkadMeetSMSS> alldcr = new List<NukkadMeetSMSS>();
                    List<NukkadMeetSMS> alldcr1 = new List<NukkadMeetSMS>();
                  
                    var dr = g1.return_dt($"exec Nukkadmeetmobilesms '" + ula.Mobile+"'");
                    if (dr.Rows.Count>0)
                    {


                        for (int i = 0; i < dr.Rows.Count; i++)
                       
                        {
                            try
                            {
                                SendSMS(ula.MeetId, dr.Rows[i]["SlNo"].ToString(), dr.Rows[i]["UserFullName"].ToString(), ula.Mobile);
                            }
                            catch (Exception)
                            {


                            }




                        }



                        g1.close_connection();
                        alldcr.Add(new NukkadMeetSMSS
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcr1,
                        });
                        data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                        response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                        return response;
                    }
                    else
                    {
                        g1.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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

        private void SendSMS(int MeetId,string userid,string usernm, string MobileNO)
        {

            var TemplateID = string.Empty;
            var Message = string.Empty;

            if (MeetId == 2)
            {
                var para = MeetId + "&userid=" + userid;
                Message = "Dear " + usernm + ",\r\n\r\nYou are cordially invited for the Electrician's Meet to be held on Saturday, 8th July 2023, 4.30 pm onwards at Shree Convention, Guntur. Please click https://erp.goldmedalindia.in/NukkadMeetEinvite.aspx?id=" + HttpUtility.UrlEncode(para) + " to download the e-invitation (you can also view the invite in your Dhan Barse account). For exact event location, please click https://tinyurl.com/mrxumvef . This event is exclusively for electricians registered in Dhanbarse.\r\n\r\nThank you,\r\nTeam Goldmedal";
                TemplateID = "1007586822796409511";
            }
            else if (MeetId == 3)
            {
                var para = MeetId + "&userid=" + userid;
                Message = "Dear " + usernm + ",\r\n\r\nYou are cordially invited for the Electrician's Meet to be held on Monday, 10th July 2023, 4.30 pm onwards at Shree Convention, Guntur. Please click https://erp.goldmedalindia.in/NukkadMeetEinvite.aspx?id=" + HttpUtility.UrlEncode(para) + " to download the e-invitation (you can also view the invite in your Dhan Barse account). For exact event location, please click https://tinyurl.com/mrxumvef . This event is exclusively for electricians registered in Dhanbarse.\r\n\r\nThank you,\r\nTeam Goldmedal";
                TemplateID = "1007574070602116210";
            }




            string MobileNoSend = string.Empty;
            WebClient web = new WebClient();
            byte[] bufData = null;          
            bufData = web.DownloadData("http://sms6.rmlconnect.net:8080/bulksms/bulksms?username=goldmedal&password=sd56jjaa&type=0&dlr=1&destination=" + MobileNO + "&source=GLDMDL&entityid=1601100000000001629&tempid=" + TemplateID + "&message=" + Message + "");
          
        }
    }
}