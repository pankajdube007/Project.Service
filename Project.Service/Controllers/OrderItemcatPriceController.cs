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

namespace Project.Service
{
    public class OrderItemcatPriceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOrderItemCatPriceDetails")]
        public HttpResponseMessage GetDetails(OrderItemcatPriceAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<OrderItemcatPrices> alldcr = new List<OrderItemcatPrices>();
                    List<OrderItemcatPrice> alldcr1 = new List<OrderItemcatPrice>();

                    var dr = g1.return_dr("AppItemCatalogePriceDetails '" + ula.CIN + "','" + ula.ItemCode + "','" + ula.ItemName + "'");

                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new OrderItemcatPrice
                            {
                                itemid = Convert.ToInt32(dr["slno"]),
                                CategoryId = dr["categoryid"].ToString(),
                                categorynm = dr["categorynm"].ToString(),
                                SubCategoryId = dr["rangeid"].ToString(),
                                Subcategorynm = dr["rangenm"].ToString(),
                                DivisionId = dr["divisionid"].ToString(),
                                divisionnm = dr["divisionnm"].ToString(),
                                itemcode = dr["ProductCode"].ToString(),
                                colornm = dr["colornm"].ToString(),
                                mrp = dr["mrp"].ToString(),
                                dlp = dr["dlp"].ToString(),
                                discount = dr["discount"].ToString(),
                                taxtype = string.IsNullOrEmpty(dr["tax"].ToString().Trim()) ? "" : dr["tax"].ToString().Trim().Split('~')[1].Split('@')[0].ToString(),
                                taxpercent = string.IsNullOrEmpty(dr["tax"].ToString().Trim()) ? "" : dr["tax"].ToString().Trim().Split('~')[2].ToString(),
                                pramotionaldiscount = dr["promotional"].ToString(),
                                ApproveQty = dr["approvedqty"].ToString(),
                                UnapproveQty = dr["unapprovedqty"].ToString(),
                                CartoonQty = dr["cartonqty"].ToString(),
                                BoxQty = dr["boxqty"].ToString(),
                                Unitnm = dr["unitnm"].ToString(),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new OrderItemcatPrices
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Data available"), Encoding.UTF8, "application/json");

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