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

namespace Project.Service.Controllers
{
    public class ExecutiveWisePodInvoiceListController : ApiController
    {

        [HttpPost]
        [ValidateModel]
        [Route("api/GetExecutiveWisePodInvoiceList")]
        public HttpResponseMessage GetPodList(ExecutiveWisePodInvoiceList ula)
        {
            DataConnectionTrans g2 = new DataConnectionTrans();
            Common cm = new Common();

            if (ula.ExId != 0)
            {

                try
                {
                    string data1;

                    List<GetPodInvoiceList> alldcr = new List<GetPodInvoiceList>();
                    List<PodInvoiceList> alldcr1 = new List<PodInvoiceList>();

                    var dr = g2.return_dr("execwisepodtlist'" + ula.ExId + "','" + ula.FromDate + "','" + ula.ToDate + "','" + ula.Type + "','" + ula.Search + "'");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new PodInvoiceList
                            {

                                ExecutiveName = Convert.ToString(dr["salesexname"].ToString()),
                                PartyName = Convert.ToString(dr["displaynm"].ToString()),
                                InvoiceNo = Convert.ToString(dr["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr["invoicedate"].ToString()),
                                TotalAmount = Convert.ToString(dr["totalamount"].ToString()),
                                PODDate = Convert.ToString(dr["PODDate"].ToString()),

                            });
                        }
                        g2.close_connection();

                        alldcr.Add(new GetPodInvoiceList
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
                        response.Content = new StringContent(cm.StatusTime(true, "No data available."), Encoding.UTF8, "application/json");

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