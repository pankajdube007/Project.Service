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
    public class GetNukkadMeetRequestByIDController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/NukkadMeetRequestByID")]
        public HttpResponseMessage GetDetails(ListNukkadMeetRequestByID ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExecId != 0)
            {
                try
                {
                    //string data1;

                    //List<NukkadMeetGift> _gift = new List<NukkadMeetGift>();
                    //List<NukkadMeetRequestByIDS> alldcr = new List<NukkadMeetRequestByIDS>();
                    //List<NukkadMeetRequestByID> alldcr1 = new List<NukkadMeetRequestByID>();
                    //var dr = g1.return_dt($"exec Get_NukkadMeetRequestByID_List_API {ula.ExecId} ");
                    //if (dr.Rows.Count>0)
                    //{
                    //    var dr2 = g1.return_dr($"exec Get_NukkadMeetRequestByIDGift "+ Convert.ToString(dr.Rows[0]["Slno"]));

                    //    if(dr2.HasRows)
                    //    {

                    //        while(dr2.Read())
                    //        {
                    //            _gift.Add(new NukkadMeetGift
                    //            {
                    //                Slno = Convert.ToString(dr2["slno"].ToString()),
                    //                Itemnm = Convert.ToString(dr2["itemnm"].ToString()),
                    //                url = Convert.ToString(dr2["url"].ToString()),
                    //            });

                    //        }
                    //    }
                    //    for (int i = 0; i < dr.Rows.Count; i++)

                    //    {
                    //        alldcr1.Add(new NukkadMeetRequestByID
                    //        {

                    //            Slno = Convert.ToString(dr.Rows[i]["Slno"].ToString()),
                    //            TypeOfMeet = Convert.ToString(dr.Rows[i]["TypeofMeet"].ToString()),
                    //            Meetname = Convert.ToString(dr.Rows[i]["MeetName"].ToString()),
                    //            Meetdate = Convert.ToString(dr.Rows[i]["MeetDate"].ToString()),
                    //            Meettime = Convert.ToString(dr.Rows[i]["meetTime"].ToString()),
                    //            MeetVenueAddTypename = Convert.ToString(dr.Rows[i]["MeetVenueAddressTypeName"].ToString()),
                    //            MeetVenueAdd = Convert.ToString(dr.Rows[i]["MeetVenueAddress"].ToString()),
                    //            MeetPincode = Convert.ToString(dr.Rows[i]["MeetPincode"].ToString()),
                    //            Meetstate = Convert.ToString(dr.Rows[i]["MeetState"].ToString()),
                    //            Meetdistrict = Convert.ToString(dr.Rows[i]["MeetDistrict"].ToString()),
                    //            Meetcity = Convert.ToString(dr.Rows[i]["MeetCity"].ToString()),
                    //            AddtenceTotalCount = Convert.ToString(dr.Rows[i]["AttendeesTotalCount"].ToString()),
                    //            ExpectedExpense = Convert.ToString(dr.Rows[i]["expectedExpenses"].ToString()),
                    //           ListGiftItem = _gift,
                    //            PurposeName = Convert.ToString(dr.Rows[i]["PurposeName"].ToString()),
                    //            ListaddcomsalesExnm = Convert.ToString(dr.Rows[i]["List_AddCompanionSalesExnm"].ToString()),
                    //            Meetremark = Convert.ToString(dr.Rows[i]["MeetRemark"].ToString()),
                    //            Status = Convert.ToString(dr.Rows[i]["stat"].ToString())

                    //        });
                    //    }
                    //    g1.close_connection();
                    //    alldcr.Add(new NukkadMeetRequestByIDS
                    //    {
                    //        result = true,
                    //        message = string.Empty,
                    //        servertime = DateTime.Now.ToString(),
                    //        data = alldcr1,
                    //    });
                    //    data1 = JsonConvert.SerializeObject(alldcr, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                    //    response.Content = new StringContent(data1, Encoding.UTF8, "application/json");

                    //    return response;

                    string data1;

                    List<NukkadMeetGift> _giftold = new List<NukkadMeetGift>();
                    List<NukkadMeetRequestByIDS> alldcr = new List<NukkadMeetRequestByIDS>();
                    List<NukkadMeetRequestByID> alldcr1 = new List<NukkadMeetRequestByID>();
                    var dr = g1.return_dt($"exec Get_NukkadMeetRequestByID_List_API {ula.ExecId}");

                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            // Clear the _gift list for each meet request
                            List<NukkadMeetGift> _gift = new List<NukkadMeetGift>();

                            // Fetch gifts for the current meet request
                            var dr2 = g1.return_dr($"exec Get_NukkadMeetRequestByIDGift " + Convert.ToString(dr.Rows[i]["Slno"]));
                            if (dr2.HasRows)
                            {
                                while (dr2.Read())
                                {
                                    _gift.Add(new NukkadMeetGift
                                    {
                                        Slno = Convert.ToString(dr2["slno"]),
                                        Itemnm = Convert.ToString(dr2["itemnm"]),
                                        url = Convert.ToString(dr2["url"]),
                                    });
                                }
                            }

                            // Add the current meet request along with its distinct gift list
                            alldcr1.Add(new NukkadMeetRequestByID
                            {
                                Slno = Convert.ToString(dr.Rows[i]["Slno"]),
                                TypeOfMeet = Convert.ToString(dr.Rows[i]["TypeofMeet"]),
                                Meetname = Convert.ToString(dr.Rows[i]["MeetName"]),
                                Meetdate = Convert.ToString(dr.Rows[i]["MeetDate"]),
                                Meettime = Convert.ToString(dr.Rows[i]["meetTime"]),
                                MeetVenueAddTypename = Convert.ToString(dr.Rows[i]["MeetVenueAddressTypeName"]),
                                MeetVenueAdd = Convert.ToString(dr.Rows[i]["MeetVenueAddress"]),
                                MeetPincode = Convert.ToString(dr.Rows[i]["MeetPincode"]),
                                Meetstate = Convert.ToString(dr.Rows[i]["MeetState"]),
                                Meetdistrict = Convert.ToString(dr.Rows[i]["MeetDistrict"]),
                                Meetcity = Convert.ToString(dr.Rows[i]["MeetCity"]),
                                AddtenceTotalCount = Convert.ToString(dr.Rows[i]["AttendeesTotalCount"]),
                                ExpectedExpense = Convert.ToString(dr.Rows[i]["expectedExpenses"]),
                                ListGiftItem = _gift, // giftsForCurrentMeet, // Use the distinct gifts list
                                PurposeName = Convert.ToString(dr.Rows[i]["PurposeName"]),
                                ListaddcomsalesExnm = Convert.ToString(dr.Rows[i]["List_AddCompanionSalesExnm"]),
                                Meetremark = Convert.ToString(dr.Rows[i]["MeetRemark"]),
                                Status = Convert.ToString(dr.Rows[i]["stat"])
                            });
                        }

                        g1.close_connection();
                        alldcr.Add(new NukkadMeetRequestByIDS
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