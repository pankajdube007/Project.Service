using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class CFSInvoiceController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCFSInvoiceDetails")]
        public HttpResponseMessage GetDetails(ListofCFSInvoiceDetails ula)
        {
            DataConnectionTrans g1 = new DataConnectionTrans();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<GetCFSInvoiceLists> alldcr = new List<GetCFSInvoiceLists>();
                    List<GetCFSInvoiceList> alldcr1 = new List<GetCFSInvoiceList>();
                    var dr = g1.return_dr("App_CFSInvoice '" + ula.CIN + "','" + ula.Date + "','" + ula.Amount + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new GetCFSInvoiceList
                            {
                                InvoiceId = Convert.ToString(dr["InvoiceId"].ToString()),
                                InvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString()),
                                Division = Convert.ToString(dr["Division"].ToString()),
                                Amount = Convert.ToString(dr["Amount"].ToString()),
                                DueDays = Convert.ToString(dr["DueDays"].ToString()),
                                
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new GetCFSInvoiceLists
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