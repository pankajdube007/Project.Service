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
    public class BranshWiseNonMovementStockController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetBranchNonMovementStockValuation")]
        public HttpResponseMessage GetDetails(ListofBranshWiseNonMovementStock ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranshWiseNonMovementStocks> alldcr = new List<BranshWiseNonMovementStocks>();
                    List<BranshWiseNonMovementStock> alldcr1 = new List<BranshWiseNonMovementStock>();

                    var dr = g1.return_dr("BranchNonMovementStockValuationManagement '"+ula.CIN+ "','" + ula.Category + "' ");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranshWiseNonMovementStock
                            {
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                branchnm = Convert.ToString(dr["branchNm"].ToString()),
                                stockamt = Convert.ToString(dr["stockAmt"].ToString()),
                                asondt = Convert.ToString(dr["dt1"].ToString()),
                                slab30 = Convert.ToString(dr["Slab30"].ToString()),
                                slab60 = Convert.ToString(dr["Slab60"].ToString()),
                                slab90 = Convert.ToString(dr["Slab90"].ToString()),
                                slab120 = Convert.ToString(dr["Slab120"].ToString()),
                                slab150 = Convert.ToString(dr["Slab150"].ToString()),
                                slab180 = Convert.ToString(dr["Slab180"].ToString()),
                                slab200 = Convert.ToString(dr["Slab200"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranshWiseNonMovementStocks
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