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


namespace Project.Service.Controllers.Vendor
{
    public class VendorSalesPendingOrderInternController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorsalependingorderIntern")]
        public HttpResponseMessage GetDetails(ListofvendorsalependingorderIntern ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ClientSecret != "")
            {
                try
                {
                    string data1;

                    List<vendorsalependingorderLists> alldcr = new List<vendorsalependingorderLists>();
                    List<vendorsalependingorderList> alldcr1 = new List<vendorsalependingorderList>();
                    var dr = g1.return_dr("Appvendorsalependingorder " + 198 + ",'" + "Vendor" + "','" + "12-05-2022" + "',20");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorsalependingorderList
                            {


                                Itemslno = Convert.ToString(dr["itemid"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                ColorName = Convert.ToString(dr["colornm"].ToString()),
                                Subcategory = Convert.ToString(dr["rangenm"].ToString()),
                                PendingQty = Convert.ToString(dr["pendingQty"].ToString()),
                                PendingDays = Convert.ToString(dr["datesince"].ToString()),
                                itemcode = Convert.ToString(dr["itemcode"].ToString()),
                                //download = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + Convert.ToString(dr["download"]),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorsalependingorderLists
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
                        response.Content = new StringContent(cm.StatusTime(true, "No  Division available"), Encoding.UTF8, "application/json");

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