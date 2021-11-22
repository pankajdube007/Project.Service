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
    public class vendorpurchaseorderController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorpurchaseorder")]
        public HttpResponseMessage GetDetails(Listofvendorpurchaseorder ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<VendorPurchaseOrderLists> alldcr = new List<VendorPurchaseOrderLists>();
                    List<VendorPurchaseOrderList> alldcr1 = new List<VendorPurchaseOrderList>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("vendorpurchaseorderApp '" + ula.CIN + "','" + ula.DivID + "','" + ula.Category + "','" + ula.Fromdate + "','" + ula.Todate + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new VendorPurchaseOrderList
                            {
                                Branch = Convert.ToString(dr["HomeBranch"].ToString()),
                                PONO = Convert.ToString(dr["ponum"].ToString()),
                                PoDate = Convert.ToString(dr["podttime"].ToString()),
                                Party = Convert.ToString(dr["PartyName"].ToString()),
                                PartyId = Convert.ToString(dr["partyid"].ToString()),
                                Status = Convert.ToString(dr["status"].ToString()),
                                Total = Convert.ToString(dr["finaltotal"].ToString()),
                                Url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "salesorder-print.aspx?id=" + Convert.ToString(dr["slno"].ToString()) + "&uniquekey=" + Convert.ToString(dr["uniquekey"].ToString())

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new VendorPurchaseOrderLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

                        return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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