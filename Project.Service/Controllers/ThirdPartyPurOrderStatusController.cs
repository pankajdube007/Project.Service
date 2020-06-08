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
namespace Project.Service.Controllers
{
    public class ThirdPartyPurOrderStatusController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getthirdpartypurstatus")]
        public HttpResponseMessage GetDetails(ListofThirdPartyPurOrderStatus ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ThirdPartyPurOrderStatuss> alldcr = new List<ThirdPartyPurOrderStatuss>();
                    List<ThirdPartyPurOrderStatus> alldcr1 = new List<ThirdPartyPurOrderStatus>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("PurchaseOrderSelectAllBranchApp '" + ula.DivID + "','" + ula.Category + "','" + ula.Fromdate + "','" + ula.Todate + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ThirdPartyPurOrderStatus
                            {
                                Branch = Convert.ToString(dr["HomeBranch"].ToString()),
                                PONO = Convert.ToString(dr["ponum"].ToString()),
                                PoDate = Convert.ToString(dr["podttime"].ToString()),
                                Party = Convert.ToString(dr["PartyName"].ToString()),
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                OrderStatus = Convert.ToString(dr["OrderStatus"].ToString()),
                                Status = Convert.ToString(dr["status"].ToString()),
                                Total = Convert.ToString(dr["finaltotal"].ToString()),
                                Url = WebConfigurationManager.AppSettings["ErpUrl"].ToString()+ "POrderEntryPrint.aspx?id=" + Convert.ToString(dr["slno"].ToString())
                               

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ThirdPartyPurOrderStatuss
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