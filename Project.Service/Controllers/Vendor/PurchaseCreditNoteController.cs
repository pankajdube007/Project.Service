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
    public class PurchaseCreditNoteController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getpurchasecreditnote")]
        public HttpResponseMessage GetDetails(ListsofPurchaseCreditNoteAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PurchaseCreditNotes> alldcr = new List<PurchaseCreditNotes>();
                    List<PurchaseCreditNote> alldcr1 = new List<PurchaseCreditNote>();
                    var dr = g1.return_dr("apppurchasecreditnote '" + ula.CIN + "','" + ula.FinYear + "','" + ula.searchtxt + "'," + ula.ReportType + "," + ula.ReportValue);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PurchaseCreditNote
                            {
                                referenceno = Convert.ToString(dr["referenceno"].ToString()),
                                date = Convert.ToString(dr["date"].ToString()),
                                amount = Convert.ToString(dr["totalamount"].ToString()),
                                ledgerdec = Convert.ToString(dr["LedgerDesc"].ToString()),
                                url = WebConfigurationManager.AppSettings["ErpUrl"].ToString() + "PurchaseSortPriceCreditNoteManualPrint.aspx?id=" + Convert.ToString(dr["SlNo"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new PurchaseCreditNotes
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