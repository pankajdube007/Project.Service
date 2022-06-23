using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using Project.Service.Models.GParivar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers.GParivar
{
    public class GetCFDetailsController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getCFDetailsList")]
        public HttpResponseMessage GetDetails(ListofCFDetails ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<CFDetailsLists> alldcr = new List<CFDetailsLists>();
                    List<CFDetailsList> alldcr1 = new List<CFDetailsList>();
                    
                    List<TotalCFDetailsList> TotalCFDetailsListdata = new List<TotalCFDetailsList>();
                  
                    List<FinalData> OutstandingFinal = new List<FinalData>();
                    var dr = g1.return_dt("App_CFDetails '" + ula.CIN + "'," + ula.Index + "," + ula.Count );
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
                            alldcr1.Add(new CFDetailsList
                            {
                                InvoiceId = Convert.ToInt32(dr.Rows[i]["invoiceid"].ToString()),
                                InvoiceNo = Convert.ToString(dr.Rows[i]["invoiceno"].ToString()),
                                InvoiceDate = Convert.ToString(dr.Rows[i]["invoicedate"].ToString()),
                                DivisionName = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                InvoiceAmt = Convert.ToString(dr.Rows[i]["finalamount"].ToString()),
                                //OuststandingAmt = Convert.ToString(dr.Rows[i]["outstandingamt"].ToString()),
                                DueDate = Convert.ToString(dr.Rows[i]["DueDate"].ToString()),
                                Interestdate = Convert.ToString(dr.Rows[i]["Interestdate"].ToString()),
                                
                            });
                        }

                     //   var dttotalsum = g1.return_dt("App_CFDetails '" + ula.CIN + "'," + 0 + "," + 999999);


                        TotalCFDetailsListdata.Add(new TotalCFDetailsList
                        {
                            Dealerssanctionlimits = Convert.ToString(dr.Rows[0]["Dealerssanctionlimits"].ToString()),
                            AvailableLimits = Convert.ToString(dr.Rows[0]["AvailableLimits"].ToString()),
                            BalanceOSwiththebank = Convert.ToString(dr.Rows[0]["BalanceOSwiththebank"].ToString()),
                        });

                        OutstandingFinal.Add(new FinalData
                        {
                            CFDetailsListdata = alldcr1,
                            TotalCFDetailsListdata = TotalCFDetailsListdata,
                            ismore = more
                        });

                        g1.close_connection();
                        alldcr.Add(new CFDetailsLists
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = OutstandingFinal,
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