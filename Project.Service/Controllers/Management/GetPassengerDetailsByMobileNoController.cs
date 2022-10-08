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
    public class GetPassengerDetailsByMobileNoController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPassengerDetailsByMobileNo")]
        public HttpResponseMessage GetDetails(ListofPassengerDetailsByMobileNo ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PassengerDetailsByMobileNos> alldcr = new List<PassengerDetailsByMobileNos>();
                    List<PassengerDetailsByMobileNo> alldcr1 = new List<PassengerDetailsByMobileNo>();
                    var dr = g1.return_dr("GetPassengerDetailsByMobileNo '" + ula.PassengerID + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PassengerDetailsByMobileNo
                            {
                                SlNo = Convert.ToString(dr["SlNo"]),
                                PassengerName = Convert.ToString(dr["PassengerName"]),
                                UserType = Convert.ToString(dr["UserType"]),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                PassengerQRCode = Convert.ToString(dr["PassengerQRCode"].ToString()),
                                PassportSizeImage = Convert.ToString(dr["PassportSizeImage"].ToString()),
                                EmailID = Convert.ToString(dr["EmailID"].ToString()),
                                GroupLeaderMobileNo = Convert.ToString(dr["GroupLeaderMobileNo"].ToString()),
                                State = Convert.ToString(dr["State"].ToString()),
                                FromFlightNo = Convert.ToString(dr["FromFlightNo"].ToString()),
                                FromDeparture = Convert.ToString(dr["FromDeparture"].ToString()),
                                FromArrival = Convert.ToString(dr["FromArrival"].ToString()),
                                DeparturePNR = Convert.ToString(dr["DeparturePNR"].ToString()),
                                ToFlightNo = Convert.ToString(dr["ToFlightNo"].ToString()),
                                ToDeparture = Convert.ToString(dr["ToDeparture"].ToString()),
                                ToArrival = Convert.ToString(dr["ToArrival"].ToString()),
                                ArrivalPNR = Convert.ToString(dr["ArrivalPNR"].ToString()),
                                HotelName = Convert.ToString(dr["HotelName"].ToString()),
                                HotelLocation = Convert.ToString(dr["HotelLocation"].ToString()),
                                CheckinDate = Convert.ToString(dr["CheckinDate"].ToString()),
                                CheckoutDate = Convert.ToString(dr["CheckoutDate"].ToString()),
                                RoomType = Convert.ToString(dr["RoomType"].ToString()),
                                RoomNumber = Convert.ToString(dr["RoomNumber"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PassengerDetailsByMobileNos
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