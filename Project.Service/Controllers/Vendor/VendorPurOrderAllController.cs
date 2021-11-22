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
    public class VendorPurOrderAllController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getvendorpurorderall")]
        public HttpResponseMessage GetDetails(ListofVendorPurOrderAll ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<VendorPurOrderAlls> alldcr = new List<VendorPurOrderAlls>();
                    List<VendorPurOrderAll> alldcr1 = new List<VendorPurOrderAll>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("VendorpurorderApp '" + ula.CIN + "','" + ula.FromDate+ "','" + ula.Todate + "','" + ula.Category + "','"+ula.PartyId+"'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new VendorPurOrderAll
                            {
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                TypeOfParty = Convert.ToString(dr["typeofparty"].ToString()),
                                Party = Convert.ToString(dr["PartyName"].ToString()),
                                Branch = Convert.ToString(dr["locnm"].ToString()),
                                BranchId = Convert.ToString(dr["branchid"].ToString()),
                                Total = Convert.ToString(dr["finaltotal"].ToString()),
                                Division = Convert.ToString(dr["DivisionName"].ToString()),
                                InvoiceDate = Convert.ToString(dr["podt"].ToString()),
                                InvoiceNo = Convert.ToString(dr["ponum"].ToString()),
                                ReceivedDate = Convert.ToString(dr["receivedate"].ToString()),
                                Fileurl = string.IsNullOrEmpty(dr["UploadFiles"].ToString().Trim(',')) ? "" : (baseurl + "vendorfiles/" + dr["UploadFiles"].ToString().Trim(','))
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new VendorPurOrderAlls
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data"), Encoding.UTF8, "application/json");

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