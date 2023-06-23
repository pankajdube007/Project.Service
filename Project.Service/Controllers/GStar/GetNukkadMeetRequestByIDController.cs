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
                    string data1;

                    List<NukkadMeetRequestByIDS> alldcr = new List<NukkadMeetRequestByIDS>();
                    List<NukkadMeetRequestByID> alldcr1 = new List<NukkadMeetRequestByID>();
                    var dr = g1.return_dr($"exec Get_NukkadMeetRequestByID_List_API {ula.ExecId} ");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new NukkadMeetRequestByID
                            {

                                Slno = Convert.ToString(dr["Slno"].ToString()),
                                TypeOfMeet = Convert.ToString(dr["TypeofMeet"].ToString()),
                                Meetname = Convert.ToString(dr["MeetName"].ToString()),
                                Meetdate = Convert.ToString(dr["MeetDate"].ToString()),
                                MeetVenueAddTypename = Convert.ToString(dr["MeetVenueAddressTypeName"].ToString()),
                                MeetVenueAdd = Convert.ToString(dr["MeetVenueAddress"].ToString()),
                                MeetPincode = Convert.ToString(dr["MeetPincode"].ToString()),
                                Meetstate = Convert.ToString(dr["MeetState"].ToString()),
                                Meetdistrict = Convert.ToString(dr["MeetDistrict"].ToString()),
                                Meetcity = Convert.ToString(dr["MeetCity"].ToString()),
                                AddtenceTotalCount = Convert.ToString(dr["AttendeesTotalCount"].ToString()),
                                ExpectedExpense = Convert.ToString(dr["expectedExpenses"].ToString()),
                                ListGiftItem = Convert.ToString(dr["List_GiftItemnm"].ToString()),
                                PurposeName = Convert.ToString(dr["PurposeName"].ToString()),
                                ListaddcomsalesExnm = Convert.ToString(dr["List_AddCompanionSalesExnm"].ToString()),
                                Meetremark = Convert.ToString(dr["MeetRemark"].ToString())

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