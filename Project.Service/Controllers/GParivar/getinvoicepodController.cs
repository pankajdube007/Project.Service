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
    public class getinvoicepodController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getinvoicepod")]
        public HttpResponseMessage GetDetails(ListsInvoicePod ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != "")
            {
                try
                {
                    string data1;

                    List<LInvoicePods> alldcr = new List<LInvoicePods>();
                    List<InvoicePod> alldcr1 = new List<InvoicePod>();
                    var dr = g2.return_dr("getinoicedetailforpod '" + ula.Uniquekey + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new InvoicePod
                            {
                                Name = Convert.ToString(dr["name"]),
                                InvoiceNo = Convert.ToString(dr["invoiceno"]),
                                FinalAmount = Convert.ToString(dr["finalamount"]),
                                LrNO = Convert.ToString(dr["lrno"]),
                                Transporter = Convert.ToString(dr["transporter"]),
                                Invoicedate = Convert.ToString(dr["Invoicedate"]),
                                Date = Convert.ToString(dr["date"]),
                                OrderNo=Convert.ToString(dr["orderno"]),
                            });
                        }
                        g2.close_connection();
                        alldcr.Add(new LInvoicePods
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
                        g2.close_connection();
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