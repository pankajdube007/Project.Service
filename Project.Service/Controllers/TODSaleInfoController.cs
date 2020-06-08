using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class TODSaleInfoController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getTODSalesInfo")]
        public HttpResponseMessage GetDetails(ListTODSaleInfo ula)
        {
            DataConection g1 = new DataConection();
          
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<TODSaleInfos> alldcr = new List<TODSaleInfos>();
                    List<TODSaleInfo> alldcr1 = new List<TODSaleInfo>();

                    var dr = g1.return_dr("todagreementselectAppinfo '" + ula.CIN + "'");

                    if (dr.HasRows)
                    {
                       
                        while (dr.Read())
                        {
                            alldcr1.Add(new TODSaleInfo
                            {
                               
                                todgroupnm = Convert.ToString(dr["CatGroupNm"].ToString()),
                                YearlyTargetAmt = Convert.ToString(dr["YearlyTargetAmt"].ToString()),
                                YearlySalesAmt = Convert.ToString(dr["yearlySaleAmt"].ToString()),
                                YearlytradeSale = Convert.ToString(dr["yearlyTradeSale"]),
                                YearlyprojectSale = Convert.ToString(dr["yearlyProjectSale"]), 
                                YearlyEarnedAmt = Convert.ToString(dr["yearlytodAmt"]),
                               

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new TODSaleInfos
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