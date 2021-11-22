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
    public class CreditNoteExcutiveDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCreditNoteDetailsExcutive")]
        public HttpResponseMessage GetDetails(ListsofCreditNoteDetail ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.SlNo != 0)
            {
                try
                {
                    string data1;

                    List<CreditNoteDetails> alldcr = new List<CreditNoteDetails>();
                    List<CreditNoteDetail> alldcr1 = new List<CreditNoteDetail>();
                    var dr = g1.return_dr("AppCreditNoteExcutiveshow " + ula.SlNo + ",'" + ula.Type + "'");
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new CreditNoteDetail
                            {
                                Invoice = Convert.ToString(dr["invoiceno"]),
                                InvoiceDate = Convert.ToString(dr["invoicedate"]),
                                Amount = Convert.ToString(dr["totalamount"]),
                                AdjustedAmount = Convert.ToString(dr["adjustedamount"]),
                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new CreditNoteDetails
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