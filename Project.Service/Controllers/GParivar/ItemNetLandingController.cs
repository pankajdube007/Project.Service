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
    public class ItemNetLandingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getitemnetlanding")]
        public HttpResponseMessage GetDetails(ListsofItemNetLanding ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;

                    List<ItemNetLandingLists> alldcr = new List<ItemNetLandingLists>();
                    List<ItemNetLandingList> alldcr1 = new List<ItemNetLandingList>();

                    var dr = g1.return_dr("itemnetlanding'" + ula.ExId + "','" + ula.ItemId + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemNetLandingList
                            {

                                ItemId = Convert.ToInt32(dr["slno"].ToString()),
                                ItemName = Convert.ToString(dr["ProductCode"].ToString()),
                                MRP = Convert.ToDecimal(dr["mrp"].ToString()),
                                UC = Convert.ToDecimal(dr["uc"].ToString()),
                                DLP = Convert.ToDecimal(dr["dlp"].ToString()),
                                BasicDiscountper = Convert.ToDecimal(dr["disper"].ToString()),
                                BasicDiscountAmount = Convert.ToDecimal(dr["disperamt"].ToString()),
                                AfterBasicDiscount = Convert.ToDecimal(dr["afterdisperamt"].ToString()),
                                PromotionalDiscountper = Convert.ToDecimal(dr["scheme"].ToString()),
                                PromotionalDiscountAmount = Convert.ToDecimal(dr["sechemperamt"].ToString()),
                                AfterPromotionalDiscount = Convert.ToDecimal(dr["aftersechemperamt"].ToString()),
                                Taxper = Convert.ToDecimal(dr["tax"].ToString()),
                                TaxAmt = Convert.ToDecimal(dr["taxamt"].ToString()),
                                BillWithTax = Convert.ToDecimal(dr["aftertaxamt"].ToString()),
                                Regularper = Convert.ToDecimal(dr["regularsch"].ToString()),
                                RegularAmt = Convert.ToDecimal(dr["regularschemeamt"].ToString()),
                                AfterRegularScheme = Convert.ToDecimal(dr["afterregularschemeamt"].ToString()),
                                Qtyper = Convert.ToDecimal(dr["itemqtydis"].ToString()),
                                QtyAmt = Convert.ToDecimal(dr["qtydisamt"].ToString()),
                                AfterQtyScheme = Convert.ToDecimal(dr["afterqtydisamt"].ToString()),
                                Cashper = Convert.ToDecimal(dr["cashper"].ToString()),
                                CashAmt = Convert.ToDecimal(dr["cashperamt"].ToString()),
                                AfterCash = Convert.ToDecimal(dr["aftercashperamt"].ToString()),
                                Todper = Convert.ToDecimal(dr["todper"].ToString()),
                                TodAmount = Convert.ToDecimal(dr["todamt"].ToString()),
                                AfterTod = Convert.ToDecimal(dr["aftertodamt"].ToString()),
                                Netland = Convert.ToDecimal(dr["netland"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ItemNetLandingLists
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