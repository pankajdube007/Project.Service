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
    public class BranshWiseNonMovementStockDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetBranchNonMovementStockValuationDetails")]
        public HttpResponseMessage GetDetails(ListofBranshWiseNonMovementStockDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<BranshWiseNonMovementStockDetailss> alldcr = new List<BranshWiseNonMovementStockDetailss>();
                    List<BranshWiseNonMovementStockDetails> alldcr1 = new List<BranshWiseNonMovementStockDetails>();

                    var dr = g1.return_dr("BranchNonMovementStockValuationManagementChild "+ula.branchid+","+ula.type);

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new BranshWiseNonMovementStockDetails
                            {
                                itmecode = Convert.ToString(dr["itemcode"].ToString()),
                                itemnm = Convert.ToString(dr["itemnm"].ToString()),
                                colornm = Convert.ToString(dr["colornm"].ToString()),
                                rangenm = Convert.ToString(dr["rangenm"].ToString()),
                                categorynm = Convert.ToString(dr["categorynm"].ToString()),
                                productcode = Convert.ToString(dr["ProductCode"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                stockamt = Convert.ToString(dr["stockAmt"].ToString()),
                                stockqty = Convert.ToString(dr["stockQty"].ToString()),
                                date = Convert.ToString(dr["dt1"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new BranshWiseNonMovementStockDetailss
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}