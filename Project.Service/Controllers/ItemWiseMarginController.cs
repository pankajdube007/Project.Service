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

                    var dr = g1.return_dr("Managementcostingrptitemwise'" + ula.ItemId + "'");


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ItemWieMarginList
                            {

                                ItemId = Convert.ToInt32(dr["ItemId"].ToString()),
                                ItemName = Convert.ToString(dr["productCode"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                Category = Convert.ToString(dr["categorynm"].ToString()),
                                Subcategory = Convert.ToString(dr["rangenm"].ToString()),
                                MRP = Convert.ToDecimal(dr["Mrp"].ToString()),
                                OfferPrice = Convert.ToDecimal(dr["Offerprice"].ToString()),
                                UC = Convert.ToDecimal(dr["uc"].ToString()),
                                dispervalue = Convert.ToString(dr["dispervalue1"].ToString()),
                                cashpervalue = Convert.ToString(dr["cashpervalue1"].ToString()),
                                regularpervalue = Convert.ToString(dr["regularpervalue1"].ToString()),
                                Qtypervalue = Convert.ToString(dr["Qtypervalue1"].ToString()),
                                todpervalue = Convert.ToString(dr["todpervalue1"].ToString()),
                                mdpervalue = Convert.ToString(dr["mdpervalue1"].ToString()),
                                wdpwervalue= Convert.ToString(dr["wdpwervalue1"].ToString()),
                                paytmpointvalue = Convert.ToString(dr["paytmpointvalue1"].ToString()),
                                BrandLoyaltyvalue = Convert.ToString(dr["BrandLoyaltyvalue1"].ToString()),
                                BranchExpensesvalue = Convert.ToString(dr["BranchExpensesvalue1"].ToString()),
                                Marketingvalue = Convert.ToString(dr["Marketingvalue1"].ToString()),
                                frieghtvalue = Convert.ToString(dr["frieghtvalue1"].ToString()),
                                commpervalue = Convert.ToString(dr["commpervalue1"].ToString()),
                                FinalAmount = Convert.ToDecimal(dr["final"].ToString()),
                                FinalDiscount = Convert.ToDecimal(dr["finaldicount"].ToString()),
                                PurchaseAmount = Convert.ToDecimal(dr["purvalue"].ToString()),
                                Margin = Convert.ToDecimal(dr["margin"].ToString()),


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