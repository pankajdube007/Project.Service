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

namespace Project.Service.Models.Management
{
    public class GetHotelwiseRoomCountController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getHotelwiseRoomCount")]
        public HttpResponseMessage GetDetails(ListofHotelwiseRoomCount ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;


                    List<HotelwiseRoomCounts> alldcr = new List<HotelwiseRoomCounts>();
                    List<HotelwiseRoomCount> alldcr1 = new List<HotelwiseRoomCount>();
                    var dr = g1.return_dr("GetHotelwiseRoomCount");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new HotelwiseRoomCount
                            {
                                HotelID = Convert.ToString(dr["HotelID"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                HotelName = Convert.ToString(dr["HotelName"].ToString()),
                                FromDate = Convert.ToString(dr["FromDate"].ToString()),
                                ToDate = Convert.ToString(dr["ToDate"].ToString()),
                                TotalRoomCount = Convert.ToString(dr["TotalRoomCount"].ToString()),
                                TotalRoomsAdded = Convert.ToString(dr["TotalRoomsAdded"].ToString()),
                                TotalBookedRoom = Convert.ToString(dr["TotalBookedRoom"].ToString()),
                                VacantRoom = Convert.ToString(dr["VacantRoom"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new HotelwiseRoomCounts
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