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
    public class ProductionPlaningController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/Getproductionplaing")]
        public HttpResponseMessage GetDetails(ListofProductionPlaning ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<ProductionPlanings> alldcr = new List<ProductionPlanings>();
                    List<ProductionPlaning> alldcr1 = new List<ProductionPlaning>();
                    string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                    var dr = g1.return_dr("getItemsforProductionPlanningApp '" + ula.SubCategory + "','" + ula.Category + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ProductionPlaning
                            {
                                ItemCode = Convert.ToString(dr["BaseCode"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                VasaiStock = Convert.ToString(dr["VasaiStockqty"].ToString()),
                                BranchStock = Convert.ToString(dr["AllBrnchStock"].ToString()),
                                //PurchasePending = Convert.ToString(dr["thirdpirtypurchasepending"].ToString()),
                                PurchasePending = "0",
                                AverageSale = Convert.ToString(dr["G"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ProductionPlanings
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