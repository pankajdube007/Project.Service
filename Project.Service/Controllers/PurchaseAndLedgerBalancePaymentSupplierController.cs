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
    public class PurchaseAndLedgerBalancePaymentSupplierController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSupplierPurchaseAndLedgerBalancePayment")]
        public HttpResponseMessage GetDetails(ListPurchaseAndLedgerBalancePaymentSupplier ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            GoldMedia _goldMedia = new GoldMedia();

            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PurchaseAndLedgerBalancePaymentSuppliers> alldcr = new List<PurchaseAndLedgerBalancePaymentSuppliers>();
                    List<PurchaseAndLedgerBalancePaymentSupplier> alldcr1 = new List<PurchaseAndLedgerBalancePaymentSupplier>();
                    List<PurchaseAndLedgerBalancePaymentSupplierFinal> PurchaseAndLedgerBalancePaymentSupplierFinal = new List<PurchaseAndLedgerBalancePaymentSupplierFinal>();
                    var dr = g1.return_dt("getPurchaseAndLedgerBalancePaymentSupplier '" + ula.SupplierId + "','" + ula.Cat + "','" + ula.CIN + "'," + ula.index + "," + ula.Count);

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
                            alldcr1.Add(new PurchaseAndLedgerBalancePaymentSupplier
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                               
                                VoucherNo = Convert.ToString(dr.Rows[i]["voucherno"].ToString()),
                                Date = Convert.ToString(dr.Rows[i]["date"].ToString()),
                                Amount = Convert.ToDecimal(dr.Rows[i]["amount"].ToString()),

                            });
                        }

                        PurchaseAndLedgerBalancePaymentSupplierFinal.Add(new PurchaseAndLedgerBalancePaymentSupplierFinal
                        {
                            PurchaseAndLedgerBalancePaymentSupplierdata = alldcr1,
                            ismore = more,
                        });

                        g1.close_connection();
                        alldcr.Add(new PurchaseAndLedgerBalancePaymentSuppliers
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = PurchaseAndLedgerBalancePaymentSupplierFinal,
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