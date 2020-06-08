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
    public class PurchaseAndLedgerBalanceOrderNoVendorController : ApiController
    {
          [HttpPost]
        [ValidateModel]
        [Route("api/getVendorPurchaseAndLedgerBalanceOrderNo")]
        public HttpResponseMessage GetDetails(ListPurchaseAndLedgerBalanceOrderNoVendor ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PurchaseAndLedgerBalanceOrderNoVendors> alldcr = new List<PurchaseAndLedgerBalanceOrderNoVendors>();
                    List<PurchaseAndLedgerBalanceOrderNoVendor> alldcr1 = new List<PurchaseAndLedgerBalanceOrderNoVendor>();
                    List<PurchaseAndLedgerBalanceOrderNoVendorFinal> PurchaseAndLedgerBalanceOrderNoVendorFinal = new List<PurchaseAndLedgerBalanceOrderNoVendorFinal>();
                    var dr = g1.return_dt("getPurchaseAndLedgerBalanceOrderNoVendor '" + ula.VendorId + "','" + ula.Cat + "','" + ula.CIN + "'," + ula.index + "," + ula.Count);

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                      //  string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
                        string baseurl = "https://erp.goldmedalindia.in/POrderEntryPrint.aspx?";




                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new PurchaseAndLedgerBalanceOrderNoVendor
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                                UniqueKey = Convert.ToString(dr.Rows[i]["uniquekey"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                Date = Convert.ToString(dr.Rows[i]["date"].ToString()),
                                Amount = Convert.ToDecimal(dr.Rows[i]["amount"].ToString()),

                                Fileurl = baseurl + "id=" + dr.Rows[i]["slno"].ToString()+"",


                                //Fileurl = string.IsNullOrEmpty(dr.Rows[i]["UploadFiles"].ToString().Trim(',')) ? "" : (baseurl + "vendorfiles/" + dr.Rows[i]["UploadFiles"].ToString().Trim(','))

                            });
                        }

                        PurchaseAndLedgerBalanceOrderNoVendorFinal.Add(new PurchaseAndLedgerBalanceOrderNoVendorFinal
                        {
                            PurchaseAndLedgerBalanceOrderNoVendordata = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new PurchaseAndLedgerBalanceOrderNoVendors
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = PurchaseAndLedgerBalanceOrderNoVendorFinal,
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