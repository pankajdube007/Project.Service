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
    public class PurAndSalePartyLedgerController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getpurandsalepartyledger")]
        public HttpResponseMessage GetDetails(ListofPurAndSalePartyLedger ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<PurAndSalePartyLedgers> alldcr = new List<PurAndSalePartyLedgers>();
                    List<PurAndSalePartyLedger> alldcr1 = new List<PurAndSalePartyLedger>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("spPurchaseSalesPartyLedgerapp '" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PurAndSalePartyLedger
                            {
                                PurchasePartyName = Convert.ToString(dr["dealnm"].ToString()),
                                PurchasePartyId = Convert.ToString(dr["purchasepartyid"].ToString()),
                                PurchasePartyTypeCat = Convert.ToString(dr["purchasepartytypeid"].ToString()),
                                PurchaseLedgerAmt = Convert.ToString(dr["PurchasePartyLedgerAmt"].ToString()),
                                SaleLedgerAmt = Convert.ToString(dr["SalesPartyLedgerAmt"].ToString()),
                                Diffrence = Convert.ToString(dr["diff"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PurAndSalePartyLedgers
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