using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class GetOrderItemDetailsController : ApiController
    {
        private const string ERPBaseURL = "https://goldmedalblob.blob.core.windows.net/goldappdata/goldapp/base/erp/";

        [HttpPost]
        [Filters.ValidateModel]
        [Route("api/getOrderItemDetailList")]
        public HttpResponseMessage GetDetails(ListofGetOrderItemDetails ula)
        {

            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<GetOrderItemDetailss> alldcr = new List<GetOrderItemDetailss>();
                    List<GetOrderItemDetails> alldcr1 = new List<GetOrderItemDetails>();
                    var dr = g1.return_dr("GetOrderItemDetailsList '" + ula.CIN + "','" + ula.CategoryId + "','" + ula.DivisionId + "','" + ula.ItemId + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetOrderItemDetails
                            {

                                itemid = Convert.ToString(dr["slno"]),
                                itemcode = Convert.ToString(dr["ProductCode"]),
                                CategoryId = Convert.ToString(dr["categoryid"]),
                                DivisionId = Convert.ToString(dr["divisionid"]),
                                categorynm = Convert.ToString(dr["categorynm"]),
                                divisionnm = Convert.ToString(dr["divisionnm"]),
                                mrp = Convert.ToString(dr["mrp"]),
                                dlp = Convert.ToString(dr["dlp"]),
                                discount = Convert.ToString(dr["discount"]),
                                taxtype = string.IsNullOrEmpty(dr["tax"].ToString().Trim()) ? "" : dr["tax"].ToString().Trim().Split('~')[1].Split('@')[0].ToString(),
                                taxpercent = string.IsNullOrEmpty(dr["tax"].ToString().Trim()) ? "" : dr["tax"].ToString().Trim().Split('~')[2].ToString(),
                                //colornm = dr["colornm"].ToString(),
                                pramotionaldiscount = Convert.ToString(dr["promotional"]),
                                ApproveQty = Convert.ToString(dr["approvedqty"]),
                                UnapproveQty = Convert.ToString(dr["unapprovedqty"]),
                                CartoonQty = Convert.ToString(dr["cartonqty"]),
                                BoxQty = Convert.ToString(dr["boxqty"]),
                                Unitnm = Convert.ToString(dr["unitnm"]),
                                SubCategoryId = Convert.ToString(dr["rangeid"]),
                                Subcategorynm = Convert.ToString(dr["rangenm"]),
                                ItemImages = Convert.ToString(dr["ItemImages"].ToString().TrimEnd(',')),
                                ImageBaseURL = ERPBaseURL,
                                ColorHexValue = Convert.ToString(dr["ColorHexValue"]),
                                ColorName = Convert.ToString(dr["ColorName"])

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetOrderItemDetailss
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