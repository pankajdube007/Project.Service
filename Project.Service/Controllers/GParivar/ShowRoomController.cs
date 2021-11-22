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
    public class ShowRoomController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getShowRoom")]
        public HttpResponseMessage GetDetails()
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            try
            {
                string data1;

                List<ShowRooms> alldcr = new List<ShowRooms>();
                List<ShowRoom> alldcr1 = new List<ShowRoom>();
                var dr = g1.return_dr("AppShowRoomMaster");

                if (dr.HasRows)
                {
                    string baseurl = _goldMedia.MapPathToPublicUrl("");
                    while (dr.Read())
                    {
                        alldcr1.Add(new ShowRoom
                        {
                            name = dr["Name"].ToString(),
                            address = dr["Address"].ToString(),
                            area = dr["areanm"].ToString(),
                            city = dr["citynm"].ToString(),
                            state = dr["statenm"].ToString(),
                            country = dr["countrynm"].ToString(),
                            image = string.IsNullOrEmpty(dr["Images"].ToString().Trim(',')) ? "" : (baseurl + "showroom/" + dr["Images"].ToString().Trim(','))
                        });
                    }
                    g1.close_connection();
                    alldcr.Add(new ShowRooms
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
    }
}