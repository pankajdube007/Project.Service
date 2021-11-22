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
    public class singleopenticketController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getsingleopentickets")]
        public HttpResponseMessage GetAllUserdetails(Listofsingleopenticket ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<AllsingleopenticketLists> alldcr = new List<AllsingleopenticketLists>();
                    List<AllsingleopenticketList> alldcr1 = new List<AllsingleopenticketList>();
                    var dr = g1.return_dr("[crm].[GetSingleOpenTicketAPI] '" + ula.slno + "','" + ula.uniquekey + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new AllsingleopenticketList
                            {
                                
                                slno = Convert.ToString(dr["slno"].ToString()),
                                TktNo = Convert.ToString(dr["TktNo"].ToString()),
                                Tktdt = Convert.ToString(dr["Tktdt"].ToString()),
                                TicketOwner = Convert.ToString(dr["TicketOwner"].ToString()),
                                TktDisapproveRejectComment = Convert.ToString(dr["TktDisapproveRejectComment"].ToString()),
                                ProductDescription = Convert.ToString(dr["ProductDescription"].ToString()),
                                CustContactNo = Convert.ToString(dr["CustContactNo"].ToString()),
                                CustName = Convert.ToString(dr["CustName"].ToString()),
                                EmailID = Convert.ToString(dr["EmailID"].ToString()),
                                CustAddress = Convert.ToString(dr["CustAddress"].ToString()),
                                Address2 = Convert.ToString(dr["Address2"].ToString()),
                                Address3 = Convert.ToString(dr["Address3"].ToString()),
                                Pincode = Convert.ToString(dr["Pincode"].ToString()),
                                City = Convert.ToString(dr["City"].ToString()),
                                statenm = Convert.ToString(dr["statenm"].ToString()),
                                Distrctnm = Convert.ToString(dr["Distrctnm"].ToString()),
                                PersonCallingName = Convert.ToString(dr["PersonCallingName"].ToString()),
                                ContactPersonName = Convert.ToString(dr["ContactPersonName"].ToString()),
                                ContactPersonContactNo = Convert.ToString(dr["ContactPersonContactNo"].ToString()),
                                ItemEANNo = Convert.ToString(dr["ItemEANNo"].ToString()),
                                ItemSerialNo = Convert.ToString(dr["ItemSerialNo"].ToString()),
                                ItemQRCode = Convert.ToString(dr["ItemQRCode"].ToString()),
                                PurchaseDt = Convert.ToString(dr["PurchaseDt"].ToString()),
                                IsProductWarnty = Convert.ToString(dr["IsProductWarnty"].ToString()),
                                ProductIssue = Convert.ToString(dr["ProductIssue"].ToString()),
                                ProductIssueDesc = Convert.ToString(dr["ProductIssueDesc"].ToString()),
                                WarrantyUpToDt = Convert.ToString(dr["WarrantyUpToDt"].ToString()),
                                ProductName = Convert.ToString(dr["ProductName"].ToString()),
                                ProductDivision = Convert.ToString(dr["ProductDivision"].ToString()),
                                TktPriorityID = Convert.ToString(dr["TktPriorityID"].ToString()),
                                TktStatus = Convert.ToString(dr["TktStatus"].ToString()),
                                ProductIssues = Convert.ToString(dr["ProductIssues"].ToString()),
                                AssignedToName = Convert.ToString(dr["v"].ToString()),
                                AssignTo = Convert.ToString(dr["AssignTo"].ToString()),
                                AppointmentDate = Convert.ToString(dr["AppointmentDate"].ToString()),
                                TimeSlot = Convert.ToString(dr["TimeSlot"].ToString()),
                                AssignRemark = Convert.ToString(dr["AssignRemark"].ToString()),
                                PartyTypeName = Convert.ToString(dr["PartyTypeName"].ToString()),
                                PartyName = Convert.ToString(dr["PartyName"].ToString()),
                                PartyAddress = Convert.ToString(dr["PartyAddress"].ToString()),
                                TktPriority = Convert.ToString(dr["TktPriority"].ToString()),
                                ProductInputTypeName = Convert.ToString(dr["ProductInputTypeName"].ToString()),
                                TktSource = Convert.ToString(dr["TktSource"].ToString()),
                                RejectReason = Convert.ToString(dr["RejectReason"].ToString()),
                                ReAssignReason = Convert.ToString(dr["ReAssignReason"].ToString()),
                                ReScheduleDate = Convert.ToString(dr["ReScheduleDate"].ToString()),
                                uniquekey = Convert.ToString(dr["uniquekey"].ToString()),
                                Custuniquekey = Convert.ToString(dr["Custuniquekey"].ToString()),
                                Itemuniquekey = Convert.ToString(dr["Itemuniquekey"].ToString()),
                                ScName = Convert.ToString(dr["ScName"].ToString()),
                                CompanyID = Convert.ToString(dr["CompanyID"].ToString()),
                                EngineerRemark = Convert.ToString(dr["EngineerRemark"].ToString()),
                                VisitStatus = Convert.ToString(dr["VisitStatus"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                IsSCAddressVerified = Convert.ToString(dr["IsSCAddressVerified"].ToString()),
                                CustomerCallConfirmation= Convert.ToString(dr["CustomerCallConfirmation"].ToString()),


                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new AllsingleopenticketLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}