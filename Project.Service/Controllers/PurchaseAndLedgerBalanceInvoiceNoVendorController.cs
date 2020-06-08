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
    public class PurchaseAndLedgerBalanceInvoiceNoVendorController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorPurchaseAndLedgerBalanceInvoiceNo")]
        public HttpResponseMessage GetDetails(ListPurchaseAndLedgerBalanceInvoiceNoVendor ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PurchaseAndLedgerBalanceInvoiceNoVendors> alldcr = new List<PurchaseAndLedgerBalanceInvoiceNoVendors>();
                    List<PurchaseAndLedgerBalanceInvoiceNoVendor> alldcr1 = new List<PurchaseAndLedgerBalanceInvoiceNoVendor>();
                    List<PurchaseAndLedgerBalanceInvoiceNoVendorFinal> PurchaseAndLedgerBalanceInvoiceNoVendorFinal = new List<PurchaseAndLedgerBalanceInvoiceNoVendorFinal>();
                    var dr = g1.return_dt("getPurchaseAndLedgerBalanceInvoiceNoVendor '" + ula.VendorId + "','"+ula.Cat+"','" + ula.CIN + "'," + ula.index + "," + ula.Count);

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
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
                            alldcr1.Add(new PurchaseAndLedgerBalanceInvoiceNoVendor
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                                UniqueKey = Convert.ToString(dr.Rows[i]["uniquekey"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                Date = Convert.ToString(dr.Rows[i]["date"].ToString()),
                                Amount = Convert.ToDecimal(dr.Rows[i]["amount"].ToString()),
                                Fileurl = string.IsNullOrEmpty(dr.Rows[i]["UploadFiles"].ToString().Trim(',')) ? "" : (baseurl + "vendorfiles/" + dr.Rows[i]["UploadFiles"].ToString().Trim(','))

                            });
                        }

                        PurchaseAndLedgerBalanceInvoiceNoVendorFinal.Add(new PurchaseAndLedgerBalanceInvoiceNoVendorFinal
                        {
                            PurchaseAndLedgerBalanceInvoiceNoVendordata = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new PurchaseAndLedgerBalanceInvoiceNoVendors
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = PurchaseAndLedgerBalanceInvoiceNoVendorFinal,
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


        public string getimageurl(string url)
        {
            string imgurl = string.Empty;
            GoldMedia _goldMedia = new GoldMedia();
            string baseurl = _goldMedia.MapPathToPublicUrl(string.Empty);
            if (!string.IsNullOrEmpty(url))
            {
                string[] split = url.Split(',');

                foreach (var item in split)
                {
                    if (item != string.Empty)
                    {
                        imgurl = imgurl + baseurl + "schemefiles/" + item.ToString() + ",";
                    }


                }
            }


            return imgurl.TrimEnd(',');
        }
    }
}