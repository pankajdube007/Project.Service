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

namespace Project.Service.Controllers
{
    public class VendorPaymentOutstandingController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getVendorOutstanding")]
        public HttpResponseMessage GetDetails(ListsofVendorOutstanding ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<VendorOutstandings> alldcr = new List<VendorOutstandings>();
                    List<VendorOutstanding> alldcr1 = new List<VendorOutstanding>();
                    List<VendorOutstandingFinal> alldcrFinal = new List<VendorOutstandingFinal>();
                    List<VendorOutstandingTotal> alldcrtotal = new List<VendorOutstandingTotal>();
                  
                    var dr = g1.return_dt("AppThirdPartyoutstanding '" +DateTime.Now.ToString("MM/dd/yyyy") + "','"+ ula.CIN + "','" + ula.Category + "'" );
                  
                    if (dr.Rows.Count > 0)
                    {
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new VendorOutstanding
                            {
                                InvoiceId = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                                CatId = Convert.ToInt32(dr.Rows[i]["Typeid"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"].ToString()),
                                DivisionName = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),                                
                                InvoiceAmt = Convert.ToString(dr.Rows[i]["finalamount"].ToString()),
                                outstadingamtAmt = Convert.ToString(dr.Rows[i]["outstandingamt"].ToString()),
                                Payamt = Convert.ToString(dr.Rows[i]["payamount"].ToString()),
                                DueDays = Convert.ToString(dr.Rows[i]["duedays"].ToString()),                               
                                percent = Convert.ToString(dr.Rows[i]["interestrate"].ToString()),
                                interestamt = Convert.ToString(dr.Rows[i]["interestamt"].ToString()),
                            });

                          
                        }

                        alldcrtotal.Add(new VendorOutstandingTotal
                        {
                            totalinvoiceamt = Math.Round(Convert.ToDecimal(dr.Compute("SUM(finalamount)", string.Empty))).ToString(),
                            totalpayamt = Math.Round(Convert.ToDecimal(dr.Compute("SUM(payamount)", string.Empty))).ToString(),
                            totaloutstanding = Math.Round(Convert.ToDecimal(dr.Compute("SUM(outstandingamt)", string.Empty))).ToString(),
                        });



                        alldcrFinal.Add(new VendorOutstandingFinal
                        {
                            details = alldcr1,
                            Total = alldcrtotal,
                        });


                        g1.close_connection();
                        alldcr.Add(new VendorOutstandings
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = alldcrFinal,
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
                        response.Content = new StringContent(cm.StatusTime(true, "Please contact to concern person to activate online payment!!!!"), Encoding.UTF8, "application/json");

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