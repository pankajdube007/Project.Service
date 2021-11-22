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
    public class vendorsalepndingsummaryController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorsalepndingsummary")]
        public HttpResponseMessage GetAllUserdetails(Listofvendorsalepndingsummary ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;
                    List<vendorsalepndingsummaryLists> alldcr = new List<vendorsalepndingsummaryLists>();
                    List<vendorsalepndingsummaryList> alldcr1 = new List<vendorsalepndingsummaryList>();
                    var dr = g1.return_dr("vendorsalepndingsummary '" + ula.PartyId + "','" + ula.Division + "','" + ula.Category + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new vendorsalepndingsummaryList
                            {
                                ItemCode = Convert.ToString(dr["ProductCode1"].ToString()),
                                HSN = Convert.ToString(dr["HSN"].ToString()),
                                ItemName = Convert.ToString(dr["itemnm"].ToString()),
                                Division = Convert.ToString(dr["divisionnm"].ToString()),
                                Color = Convert.ToString(dr["colornm"].ToString()),
                                SubCat = Convert.ToString(dr["rangenm"].ToString()),
                                Barnch = Convert.ToString(dr["BranchNm"].ToString()),
                                TotAprQty = Convert.ToString(dr["approvqty"].ToString()),
                                TotInvoiceQty = Convert.ToString(dr["invoiceQty"].ToString()),
                                TotCancelQty = Convert.ToString(dr["cancelqty"].ToString()),
                                TotPendingQty = Convert.ToString(dr["pendingQty"].ToString()),
                                                     

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new vendorsalepndingsummaryLists
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
                        response.Content = new StringContent(cm.StatusTime(false, "No Data available"), Encoding.UTF8, "application/json");

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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(cm.StatusTime(false, "Please Log In"), Encoding.UTF8, "application/json");

                return response;
            }
        }
    }
}