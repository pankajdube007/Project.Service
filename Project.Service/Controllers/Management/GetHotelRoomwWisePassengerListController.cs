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

namespace Project.Service.Controllers.Management
{
    public class GetHotelRoomwWisePassengerListController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getHotelRoomwWisePassengerList")]
        public HttpResponseMessage GetDetails(ListofHotelRoomwWisePassenger ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;


                    List<HotelRoomwWisePassengerLists> alldcr = new List<HotelRoomwWisePassengerLists>();
                    List<HotelRoomwWisePassengerList> alldcr1 = new List<HotelRoomwWisePassengerList>();
                    var dr = g1.return_dr("GetHotelRoomwWisePassengerList_API '" + ula.HotelID + "','" + ula.Type + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new HotelRoomwWisePassengerList
                            {
                                HotelName = Convert.ToString(dr["HotelName"]),
                                RoomType = Convert.ToString(dr["RoomType"]),
                                RoomNumber = Convert.ToString(dr["RoomNumber"].ToString()),
                                PassengerName = Convert.ToString(dr["PassengerName"].ToString()),
                                PassengerContactNo = Convert.ToString(dr["PassengerContactNo"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new HotelRoomwWisePassengerLists
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