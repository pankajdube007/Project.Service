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

                    List<NukkadMeetGift> _gift = new List<NukkadMeetGift>();
                    List<NukkadMeetRequestByIDS> alldcr = new List<NukkadMeetRequestByIDS>();
                    List<NukkadMeetRequestByID> alldcr1 = new List<NukkadMeetRequestByID>();
                    var dr = g1.return_dt($"exec Get_NukkadMeetRequestByID_List_API {ula.ExecId} ");
                    if (dr.Rows.Count>0)
                    {
                        var dr2 = g1.return_dr($"exec Get_NukkadMeetRequestByIDGift "+ Convert.ToString(dr.Rows[0]["Slno"]));

                        if(dr2.HasRows)
                        {

                            while(dr2.Read())
                            {
                                _gift.Add(new NukkadMeetGift
                                {
                                    Slno = Convert.ToString(dr2["slno"].ToString()),
                                    Itemnm = Convert.ToString(dr2["itemnm"].ToString()),
                                    url = Convert.ToString(dr2["url"].ToString()),
                                });

                            }
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                       
                        {
                            alldcr1.Add(new NukkadMeetRequestByID
                            {

                                Slno = Convert.ToString(dr.Rows[0]["Slno"].ToString()),
                                TypeOfMeet = Convert.ToString(dr.Rows[0]["TypeofMeet"].ToString()),
                                Meetname = Convert.ToString(dr.Rows[0]["MeetName"].ToString()),
                                Meetdate = Convert.ToString(dr.Rows[0]["MeetDate"].ToString()),
                                Meettime = Convert.ToString(dr.Rows[0]["meetTime"].ToString()),
                                MeetVenueAddTypename = Convert.ToString(dr.Rows[0]["MeetVenueAddressTypeName"].ToString()),
                                MeetVenueAdd = Convert.ToString(dr.Rows[0]["MeetVenueAddress"].ToString()),
                                MeetPincode = Convert.ToString(dr.Rows[0]["MeetPincode"].ToString()),
                                Meetstate = Convert.ToString(dr.Rows[0]["MeetState"].ToString()),
                                Meetdistrict = Convert.ToString(dr.Rows[0]["MeetDistrict"].ToString()),
                                Meetcity = Convert.ToString(dr.Rows[0]["MeetCity"].ToString()),
                                AddtenceTotalCount = Convert.ToString(dr.Rows[0]["AttendeesTotalCount"].ToString()),
                                ExpectedExpense = Convert.ToString(dr.Rows[0]["expectedExpenses"].ToString()),
                               ListGiftItem = _gift,
                                PurposeName = Convert.ToString(dr.Rows[0]["PurposeName"].ToString()),
                                ListaddcomsalesExnm = Convert.ToString(dr.Rows[0]["List_AddCompanionSalesExnm"].ToString()),
                                Meetremark = Convert.ToString(dr.Rows[0]["MeetRemark"].ToString()),
                                Status = Convert.ToString(dr.Rows[0]["stat"].ToString())

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