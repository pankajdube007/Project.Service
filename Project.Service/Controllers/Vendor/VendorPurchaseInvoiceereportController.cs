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
using System.Web.Configuration;


namespace Project.Service.Controllers
{
    public class VendorPurchaseInvoiceereportController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getvendorpurchaseinvoiceereport")]
        public HttpResponseMessage GetDetails(ListofVendorPurchaseInvoicereport ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<VendorPurchaseInvoicereports> alldcr = new List<VendorPurchaseInvoicereports>();
                    List<VendorPurchaseInvoicereport> alldcr1 = new List<VendorPurchaseInvoicereport>();
                    var dr = g1.return_dr("spVendorProcPurchaseInvoiceheadselectreportapp '" + ula.FromDate + "','" + ula.Todate + "','" + ula.PartyID + "','" + ula.Cat + "'");
                    if (dr.HasRows)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl("");
                        while (dr.Read())
                        {
                            alldcr1.Add(new VendorPurchaseInvoicereport
                            {
                               
                                uniquekey = Convert.ToString(dr["uniquekey"].ToString()),
                                SLNo = Convert.ToString(dr["SLNo"].ToString()),
                                invoiceno = Convert.ToString(dr["invoiceno"].ToString()),
                                invoicedate = Convert.ToString(dr["invoicedate"].ToString()),
                                itemamount = Convert.ToString(dr["itemamount"].ToString()),
                                discountamount = Convert.ToString(dr["discountamount"].ToString()),
                                taxamount = Convert.ToString(dr["taxamount"].ToString()),
                                totalamount = Convert.ToString(dr["totalamount"].ToString()),
                                finalamount = Convert.ToString(dr["finalamount"].ToString()),
                                roundoff = Convert.ToString(dr["roundoff"].ToString()),
                                EWayBillno = Convert.ToString(dr["EWayBillno"].ToString()),
                                divisionnm = Convert.ToString(dr["divisionnm"].ToString()),
                                BranchName = Convert.ToString(dr["BranchName"].ToString()),
                                taxamount2 = Convert.ToString(dr["taxamount2"].ToString()),
                                Party = Convert.ToString(dr["displaynmwitharea"].ToString()),
                                url = WebConfigurationManager.AppSettings["ErpUrl"] + "Report-TaxInvoice.aspx?type=transporter&id=" + Convert.ToString(dr["slno"].ToString() + "&uniquekey=" + Convert.ToString(dr["uniquekey"].ToString() + "&viewtype=App")),
                             
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new VendorPurchaseInvoicereports
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