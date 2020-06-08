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
    public class InvoicewiseCDController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getInvoicewiseCD")]
        public HttpResponseMessage GetAllUserLatLong(ListofInvoicewiseCD ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.InvoiceNo != "")
            {
                try
                {
                    string data1;
                    List<InvoicewiseCDs> alldcr = new List<InvoicewiseCDs>();
                    List<InvoicewiseCD> alldcr1 = new List<InvoicewiseCD>();
                    var dr = g1.return_dr("InvoicewiseCD '" + ula.CIN+"','"+ula.InvoiceNo+"'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new InvoicewiseCD
                            {
                                duedays = Convert.ToString(dr["todays"].ToString()),
                                percentage = Convert.ToString(dr["percentage"].ToString()),
                                duedate = Convert.ToString(dr["duedt"].ToString()),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new InvoicewiseCDs
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!" + ex.ToString()), Encoding.UTF8, "application/json");

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