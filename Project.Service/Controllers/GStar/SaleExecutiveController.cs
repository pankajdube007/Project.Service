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
    public class SaleExecutiveController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getSalesExecutive")]
        public HttpResponseMessage GetDetails(ListsofSalesExecutive ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.ExId != 0)
            {
                try
                {
                    string data1;
                    decimal totalsale = 0;
                    decimal totaltarget = 0;
                    decimal totaldealertarget = 0;
                    List<SalesExecutives> alldcr = new List<SalesExecutives>();
                    List<SalesExecutive> alldcr1 = new List<SalesExecutive>();
                    List<SalesExecutiveFinal> Final = new List<SalesExecutiveFinal>();
                    var dr = g1.return_dr("AppSaleExcutive " + ula.ExId + ",'" + ula.FromDate + "','" + ula.ToDate + "'," + Convert.ToBoolean(ula.Hierarchy));
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new SalesExecutive
                            {
                                division = dr["divisionnm"].ToString(),
                                sales = dr["sales"].ToString(),
                                target = dr["targetamt"].ToString(),
                                //dealertarget = dr["dealertarget"].ToString()
                            });
                            totalsale = totalsale + Convert.ToDecimal(dr["sales"].ToString());
                            totaltarget = totaltarget + Convert.ToDecimal(dr["targetamt"].ToString());
                            //totaldealertarget = totaltarget + Convert.ToDecimal(dr["dealertarget"].ToString());
                        }

                        Final.Add(new SalesExecutiveFinal
                        {
                            SalesExecutiveDetails = alldcr1,
                            TotalSale = totalsale.ToString(),
                            TotalTarget = totaltarget.ToString(),
                            //TotaldealerTarget = totaldealertarget.ToString(),
                        });

                        g1.close_connection();
                        alldcr.Add(new SalesExecutives
                        {
                            result = true,
                            message = string.Empty,
                            servertime = DateTime.Now.ToString(),
                            data = Final,
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
                    response.Content = new StringContent(cm.StatusTime(false, "Oops! Something is wrong, try again later!!!!!!!!"), Encoding.UTF8, "application/json");

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