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

namespace Project.Service.Controllers.GStar
{
    public class NukkedMeetCheckinController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/NukkadmeetCheckin")]
        public HttpResponseMessage GetDetails(NukkedMeetCheckinList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExecId != 0)
            {
                try
                {
                    string data1;


                    List<NukkedMeetCheckinS> alldcr = new List<NukkedMeetCheckinS>();
                    List<NukkedMeetCheckin> alldcr1 = new List<NukkedMeetCheckin>();
                    var dr = g1.return_dt($"exec NukkadmeetCheckin "+ula.ExecId + "," +ula.UserID + "," + ula.MeetID + ",'" + ula.Uniquekey+"'");
                    if (dr.Rows.Count>0)
                    {


                    
                            alldcr1.Add(new NukkedMeetCheckin
                            {

                                msg = dr.Rows[0]["msg"].ToString()
                            });
                  
                        g1.close_connection();
                        alldcr.Add(new NukkedMeetCheckinS
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
                        response.Content = new StringContent(cm.StatusTime(true, "User Checked in Already or Not Available in this Meet"), Encoding.UTF8, "application/json");

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