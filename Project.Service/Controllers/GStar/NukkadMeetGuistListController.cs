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
    public class NukkadMeetGuistListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/NukkadMeetRequestGuestList")]
        public HttpResponseMessage GetDetails(ListNukkadMeetGiustList ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExecId != 0)
            {
                try
                {
                    string data1;

                
                    List<NukkadMeetRequestGuiestByIDS> alldcr = new List<NukkadMeetRequestGuiestByIDS>();
                    List<NukkadMeetRequestGuisetByID> alldcr1 = new List<NukkadMeetRequestGuisetByID>();
                    var dr = g1.return_dr($"exec Nukkadmeetguiestlist {ula.NukkedId} ");
                    if (dr.HasRows)
                    {
                    

                        while (dr.Read())
                        {
                            alldcr1.Add(new NukkadMeetRequestGuisetByID
                            {

                                UserId = Convert.ToString(dr["UserID"].ToString()),
                                Name = Convert.ToString(dr["UserName"].ToString()),
                                Photo = Convert.ToString(dr["photo"].ToString()),
                                Mobile = Convert.ToString(dr["MobileNo"].ToString()),
                                Category = Convert.ToString(dr["RoleName"].ToString()),
                                Status = Convert.ToString(dr["stat"].ToString()),
                                CheckinStatus = Convert.ToString(dr["checkinstat"].ToString()),
                                url = Convert.ToString(dr["url"].ToString())
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new NukkadMeetRequestGuiestByIDS
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
    }
}