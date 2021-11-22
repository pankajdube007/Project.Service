using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace Project.Service.Controllers
{
    public class VendorPurchaseOrderEntryPendingItemSummeryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorpurchaseorderentrypendingitemsummery")]
        public HttpResponseMessage GetDetails(ListofVendorPurchaseOrderEntryPendingItemSummery ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<VendorPurchaseOrderEntryPendingItemSummerys> alldcr = new List<VendorPurchaseOrderEntryPendingItemSummerys>();
                    List<VendorPurchaseOrderEntryPendingItemSummery> alldcr1 = new List<VendorPurchaseOrderEntryPendingItemSummery>();
                    var dr = g1.return_dr("spVendProcePurchaseOrderEntryPendingItemsSummeryapp '" + ula.PartyID + "','" + ula.branchIDs + "','" + ula.Division + "','" + ula.ApprovedDt + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new VendorPurchaseOrderEntryPendingItemSummery
                            {
                                HSN = Convert.ToString(dr["HSN"].ToString()),
                                ProductCode1 = Convert.ToString(dr["ProductCode1"].ToString()),
                                itemnm = Convert.ToString(dr["itemnm"].ToString()),
                                colornm = Convert.ToString(dr["colornm"].ToString()),
                                rangenm = Convert.ToString(dr["rangenm"].ToString()),
                                pendingQty = Convert.ToString(dr["pendingQty"].ToString()),
                                approvqty = Convert.ToString(dr["approvqty"].ToString()),
                                cancelqty = Convert.ToString(dr["cancelqty"].ToString()),
                                invoiceQty = Convert.ToString(dr["invoiceQty"].ToString()),
                                BranchNm = Convert.ToString(dr["BranchNm"].ToString()),
                                PartyNm = Convert.ToString(dr["PartyNm"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                partyid = Convert.ToString(dr["partyid"].ToString()),
                                divisionid = Convert.ToString(dr["divisionid"].ToString()),
                                itemid = Convert.ToString(dr["taxamount2"].ToString()),



                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new VendorPurchaseOrderEntryPendingItemSummerys
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