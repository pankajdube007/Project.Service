using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GStar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TravelTxnManagerDetailsController : ApiController
    {
           
        [HttpPost]
        [ValidateModel]
        [Route("api/getListOfAllTravelManagerData")]
        public HttpResponseMessage GetListOfAllTravelManagerData(TravelTxnManager ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<getListOfTravelManagerDetails> alldcr = new List<getListOfTravelManagerDetails>();
                    List<TravelTxnManager> alldcr1 = new List<TravelTxnManager>();
                    var dr = g1.return_dr("dbo.getListOfAllTravelManagerData " + ula.ExId+"'"+ula.search+ "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new TravelTxnManager
                            {
                                PendingReqCount = Convert.ToInt32(dr["PendingReqCount"].ToString()),
                                ApprovedReqCount = Convert.ToInt32(dr["ApprovedReqCount"].ToString()),
                                WithdrawReqCount = Convert.ToInt32(dr["WithdrawReqCount"].ToString()),
                                RejectReqCount = Convert.ToInt32(dr["RejectReqCount"].ToString()),
                                TravelTxnId = Convert.ToInt32(dr["TravelTxnId"].ToString()),
                                TotalTravelDays = Convert.ToInt32(dr["TotalTravelDays"].ToString()),
                                TravelFromDate = Convert.ToString(dr["TravelFromDate"].ToString()),
                                TravelToDate = Convert.ToString(dr["TravelToDate"].ToString()),
                                Source = Convert.ToString(dr["Source"].ToString()),
                                Destination = Convert.ToString(dr["Destination"].ToString()),
                                Stop1 = Convert.ToString(dr["Stop1"].ToString()),
                                Stop2 = Convert.ToString(dr["Stop2"].ToString()),
                                ReturnSource = Convert.ToString(dr["ReturnSource"].ToString()),
                                ReturnDestination = Convert.ToString(dr["ReturnDestination"].ToString()),
                                PersonalTravelString = Convert.ToString(dr["PersonalTravel"].ToString()),
                                PersonalTravelDays = Convert.ToInt32(dr["PersonalTravelDays"].ToString()),
                                PersonalTFromDate = Convert.ToString(dr["PersonalTFromDate"].ToString()),
                                PersonalTToDate = Convert.ToString(dr["PersonalTToDate"].ToString()),
                                ModeOfTransportString = Convert.ToString(dr["ModeOfTransport"].ToString()),
                                AccomodationDays = Convert.ToInt32(dr["AccomodationDays"].ToString()),
                                PurposeString = Convert.ToString(dr["Purpose"].ToString()),
                                ApprovedBy1String = Convert.ToString(dr["ApprovedBy1"].ToString()),
                                ApprovedBy1Date = Convert.ToString(dr["ApprovedBy1Date"].ToString()),
                                ApprovedBy2String = Convert.ToString(dr["ApprovedBy2"].ToString()),
                                ApprovedBy2Date = Convert.ToString(dr["ApprovedBy2Date"].ToString()),
                                ApprovedStatus = Convert.ToString(dr["ApprovedStatus"].ToString()),
                                PersonalTravel = Convert.ToBoolean(dr["PersonalTravelStatus"].ToString()),
                                Withdraw = Convert.ToString(dr["Withdraw"].ToString()),
                                WithdrawDate = Convert.ToString(dr["WithdrawDate"].ToString()),
                                WithdrawRemark = Convert.ToString(dr["WithdrawRemark"].ToString()),
                                RequestDate = Convert.ToString(dr["RequestDate"].ToString()),
                                Employeename = Convert.ToString(dr["Employeename"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new getListOfTravelManagerDetails
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data available"), Encoding.UTF8, "application/json");
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


        [HttpPost]
        [ValidateModel]
        [Route("api/ApprovedRejectTravelTxnRequest")]
        public HttpResponseMessage withdrawRequest(TravelTxnManager ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                   //string data1;
                   //List<DCRExecutives> alldcr = new List<DCRExecutives>();
                   //List<DCRExecutive> alldcr1 = new List<DCRExecutive>();

                    var dr = g2.return_dr("updateApprovedRejectTravelTxnRequest " + ula.ExId + "," + ula.TravelTxnId +",'"+ ula.ApprovedStatus+"','" +ula.RejectRemark+"'");

                    if (dr.HasRows)
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Data Updated!!!"), Encoding.UTF8, "application/json");
                        return response;
                    }
                    else
                    {
                        g2.close_connection();
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(cm.StatusTime(true, "Something Went Wrong, Request is not submited!!!"), Encoding.UTF8, "application/json");
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