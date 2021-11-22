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
    public class ItemWiseMarginController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getmarginitemwisewise")]
        public HttpResponseMessage GetDetails(ListItemWieMargin ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ItemWieMarginLists> alldcr = new List<ItemWieMarginLists>();
                    List<ItemWieMarginList> alldcr1 = new List<ItemWieMarginList>();

                    var dr = g1.return_dr("Managementcostingrptitemwise'" + ula.ItemId + "','"+ula.Category+"'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemWieMarginList
                            {

                                ItemId = Convert.ToString(dr["ItemId"].ToString()),
                                ItemCode = Convert.ToString(dr["productCode1"].ToString()),
                                Cat = Convert.ToString(dr["categorynm"].ToString()),
                                Subcat = Convert.ToString(dr["rangenm"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                LastPurchaseval = Convert.ToString(dr["lastper"].ToString()),
                                PurchaseAmount = Convert.ToString(dr["purvalue"].ToString()),
                                PurchaseOverHead = Convert.ToString(dr["puroverheadval1"].ToString()),
                                TotalPurchase = Convert.ToString(dr["totpur"].ToString()),
                                Mrp = Convert.ToString(dr["Mrp"].ToString()),
                                UC = Convert.ToString(dr["uc"].ToString()),
                                DLP = Convert.ToString(dr["Offerprice"].ToString()),
                                DiscAmt = Convert.ToString(dr["dispervalue1"].ToString()),
                                PromotionalScheme = Convert.ToString(dr["schemevalue1"].ToString()),
                                CD = Convert.ToString(dr["cashpervalue1"].ToString()),
                                Regular = Convert.ToString(dr["regularpervalue1"].ToString()),
                                Qty = Convert.ToString(dr["Qtypervalue1"].ToString()),
                                TOD = Convert.ToString(dr["todpervalue1"].ToString()),
                                MD = Convert.ToString(dr["mdpervalue1"].ToString()),
                                WD = Convert.ToString(dr["wdpwervalue1"].ToString()),
                                PaytmPoint = Convert.ToString(dr["paytmpointvalue1"].ToString()),
                                BrandLoyalty = Convert.ToString(dr["BrandLoyaltyvalue1"].ToString()),
                                Commission = Convert.ToString(dr["commpervalue1"].ToString()),
                                BranchExpenses = Convert.ToString(dr["BranchExpensesvalue1"].ToString()),
                                Marketing = Convert.ToString(dr["Marketingvalue1"].ToString()),
                                Frieght = Convert.ToString(dr["frieghtvalue1"].ToString()),
                                PaytmAmount = Convert.ToString(dr["paytmamount"].ToString()),
                                SaleOverHead = Convert.ToString(dr["salesoverheadvalue1"].ToString()),
                                FinalAmount = Convert.ToString(dr["final"].ToString()),
                                FinalDiscount = Convert.ToString(dr["finaldicount"].ToString()),
                                Margin = Convert.ToString(dr["margin"].ToString()),
                                MarginPer = Convert.ToString(dr["marginper"].ToString()),
                                
                                

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ItemWieMarginLists
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