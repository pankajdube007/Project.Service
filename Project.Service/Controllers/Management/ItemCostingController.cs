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
    public class ItemCostingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getItemCosting")]
        public HttpResponseMessage GetDetails(ItemCosting ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    List<ListItemCostings> alldcr = new List<ListItemCostings>();
                    List<ListItemCosting> alldcr1 = new List<ListItemCosting>();

                    var dr = g1.return_dr("costingrptappitem '" + ula.CIN + "','" + ula.Cat + "','" + ula.ItemId + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ListItemCosting
                            {


                                ItemCode = Convert.ToString(dr["ItemCode"].ToString()),
                                ItemName = Convert.ToString(dr["ItemName"].ToString()),
                                Category = Convert.ToString(dr["Category"].ToString()),
                                SubCategory = Convert.ToString(dr["SubCategory"].ToString()),
                                Division = Convert.ToString(dr["Division"].ToString()),
                                MRP = Convert.ToString(dr["Mrp"].ToString()),
                                OfferPrice = Convert.ToString(dr["Offerprice"].ToString()),
                                uc = Convert.ToString(dr["uc"].ToString()),
                                RegularPer = Convert.ToString(dr["RegularPer"].ToString()),
                                QtyPer = Convert.ToString(dr["QtyPer"].ToString()),
                                TodPer = Convert.ToString(dr["TodPer"].ToString()),
                                MdPer = Convert.ToString(dr["MD"].ToString()),
                                WdPer = Convert.ToString(dr["WD"].ToString()),
                                AddPer = Convert.ToString(dr["adddisc"].ToString()),
                                BrandLoyaltyPer = Convert.ToString(dr["BrandLoyalty"].ToString()),
                                CommPer = Convert.ToString(dr["Comm"].ToString()),
                                BranchExpensesPer = Convert.ToString(dr["BranchExpenses"].ToString()),
                                MarketingPer = Convert.ToString(dr["Marketing"].ToString()),
                                FrieghtPer = Convert.ToString(dr["frieghtPer"].ToString()),
                                SalesoverheadPer = Convert.ToString(dr["salesoverhead"].ToString()),
                                PurchaseoverheadPer = Convert.ToString(dr["purchaseoverhead"].ToString()),
                                DiscountPer = Convert.ToString(dr["disper"].ToString()),
                                CDPer = Convert.ToString(dr["cashper"].ToString()),
                                PromotionalPer = Convert.ToString(dr["scheme"].ToString()),
                                PaytmAmt = Convert.ToString(dr["paytmamount"].ToString()),
                                DiscountAmt = Convert.ToString(dr["dispervalue"].ToString()),
                                PromotionalAmt = Convert.ToString(dr["schemevalue"].ToString()),
                                CDAmt = Convert.ToString(dr["cashpervalue"].ToString()),
                                RegularAmt = Convert.ToString(dr["regularpervalue"].ToString()),
                                QtyAmt = Convert.ToString(dr["Qtypervalue"].ToString()),
                                TODAmt = Convert.ToString(dr["todpervalue"].ToString()),
                                MDAmt = Convert.ToString(dr["mdpervalue"].ToString()),
                                WdAmt = Convert.ToString(dr["wdpwervalue"].ToString()),
                                AddValue = Convert.ToString(dr["adddiscvalue"].ToString()),
                                BranchExpensesvalue = Convert.ToString(dr["BranchExpensesvalue"].ToString()),
                                Marketingvalue = Convert.ToString(dr["Marketingvalue"].ToString()),
                                BrandLoyaltyvalue = Convert.ToString(dr["BrandLoyaltyvalue"].ToString()),
                                frieghtvalue = Convert.ToString(dr["frieghtvalue"].ToString()),
                                commpervalue = Convert.ToString(dr["commpervalue"].ToString()),
                                salesoverheadvalue = Convert.ToString(dr["salesoverheadvalue"].ToString()),
                                final = Convert.ToString(dr["final"].ToString()),
                                purvalue = Convert.ToString(dr["purvalue"].ToString()),
                                totalpurvalue = Convert.ToString(dr["totpur"].ToString()),
                                margin = Convert.ToString(dr["margin"].ToString()),
                                marginper = Convert.ToString(dr["marginper"].ToString()),




                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ListItemCostings
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }

    }
}