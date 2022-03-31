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
    public class CategoryCostingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCategoryCosting")]
        public HttpResponseMessage GetDetails(CategoryCosting ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != "")
            {

                try
                {
                    string data1;

                    List<ListCategoryCostings> alldcr = new List<ListCategoryCostings>();
                    List<ListCategoryCosting> alldcr1 = new List<ListCategoryCosting>();

                    var dr = g1.return_dr("costingrptapp '" + ula.CIN + "','" + ula.Cat + "','" + ula.CategoryId + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ListCategoryCosting
                            {
                                ItemId = Convert.ToString(dr["ItemId"].ToString()),
                                ItemCode = Convert.ToString(dr["ItemCode"].ToString()),
                                ItemName = Convert.ToString(dr["ItemName"].ToString()),
                                Category = Convert.ToString(dr["Category"].ToString()),
                                SubCategory = Convert.ToString(dr["SubCategory"].ToString()),
                                Division = Convert.ToString(dr["Division"].ToString()),
                                MRP = Convert.ToString(dr["Mrp"].ToString()),
                                OfferPrice = Convert.ToString(dr["Offerprice"].ToString()),
                                FinalAmount = Convert.ToString(dr["final"].ToString()),
                                PurchaseValue = Convert.ToString(dr["purvalue"].ToString()),
                                TotalPurchaseValue = Convert.ToString(dr["totpur"].ToString()),
                                Margin = Convert.ToString(dr["margin"].ToString()),
                                MarginPer = Convert.ToString(dr["marginper"].ToString())



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ListCategoryCostings
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