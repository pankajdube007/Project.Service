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
    public class CustomerReciptController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesPaymentReport")]
        public HttpResponseMessage GetDetails(ListsofCustomerReciptAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<CustomerRecipts> alldcr = new List<CustomerRecipts>();
                    List<CustomerRecipt> alldcr1 = new List<CustomerRecipt>();
                    List<CustomerReciptFinal> CustomerReciptFinal = new List<CustomerReciptFinal>();
                    var dr = g1.return_dt("App_CustomerRecipt '" + ula.CIN + "'," + ula.Index + "," + ula.Count + ",'" + ula.SearchText + "','" + ula.FromDate + "','" + ula.ToDate + "'");

                    bool more = false;

                    if (dr.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dr.Rows[0]["TotalCount"].ToString()) > (ula.Count + ula.Index))
                        {
                            more = true;
                        }
                        else
                        {
                            more = false;
                        }
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            alldcr1.Add(new CustomerRecipt
                            {
                                slno = Convert.ToInt32(dr.Rows[i]["slno"].ToString()),
                                InstrumentType = Convert.ToString(dr.Rows[i]["Instrument_type"].ToString()),
                                Reciept = Convert.ToString(dr.Rows[i]["Receipt"].ToString()),
                                Date = Convert.ToString(dr.Rows[i]["Voucherdt"].ToString()),
                                Division = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                Status = Convert.ToString(dr.Rows[i]["status"].ToString()),
                                Amount = Convert.ToString(dr.Rows[i]["Cheque_amt"].ToString()),
                            });
                        }
                        CustomerReciptFinal.Add(new CustomerReciptFinal
                        {
                            custrecieptdata = alldcr1,
                            ismore = more
                        });
                        g1.close_connection();
                        alldcr.Add(new CustomerRecipts
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = CustomerReciptFinal,
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