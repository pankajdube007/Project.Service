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
    public class ApprovalPendingUsersDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getapprovalpendingusersdetails")]
        public HttpResponseMessage GetDetails(ListApprovalPendingUsersDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ApprovalPendingUsersDetailsLists> alldcr = new List<ApprovalPendingUsersDetailsLists>();
                    List<ApprovalPendingUsersDetailsList> alldcr1 = new List<ApprovalPendingUsersDetailsList>();
                    var dr = g1.return_dr("GetApprovalPendingUsersDetailsList '" + ula.Cat + "','" + ula.CIN + "','" + ula.StateId + "','" + ula.DaysPeriod + "','" + ula.ProfileFromDate + "','" + ula.ProfileToDate + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ApprovalPendingUsersDetailsList
                            {
                             
                                SlNo = Convert.ToString(dr["SlNo"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                FullName = Convert.ToString(dr["FullName"].ToString()),
                                MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                                Email = Convert.ToString(dr["Email"].ToString()),
                                UniqueKey = Convert.ToString(dr["UniqueKey"].ToString()),
                                UserMasterID = Convert.ToString(dr["UserMasterID"].ToString()),
                                ApprovalStatus = Convert.ToString(dr["ApprovalStatus"].ToString()),
                                DateOfBirth = Convert.ToString(dr["DateOfBirth"].ToString()),
                                ShopName = Convert.ToString(dr["ShopName"].ToString()),
                                MembershipID = Convert.ToString(dr["MembershipID"].ToString()),
                                KycVerified = Convert.ToString(dr["KycVerified"].ToString()),
                                IsPaytmVerified = Convert.ToString(dr["IsPaytmVerified"].ToString()),
                                IsScanAvailable = Convert.ToString(dr["IsScanAvailable"].ToString()),
                                Statenm = Convert.ToString(dr["Statenm"].ToString()),
                                District = Convert.ToString(dr["District"].ToString()),
                                City = Convert.ToString(dr["City"].ToString()),
                                Pincode = Convert.ToString(dr["Pincode"].ToString()),
                                RegDate = Convert.ToString(dr["RegDate"].ToString()),
                                ProfileCompleteDate = Convert.ToString(dr["ProfileCompleteDate"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ApprovalPendingUsersDetailsLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data Found"), Encoding.UTF8, "application/json");

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