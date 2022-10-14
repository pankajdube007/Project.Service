using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class pendingOrderDivisionWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getPendingOrderDivisionWise")]
        public HttpResponseMessage GetDetails(PendingOrderDivisionAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<PendingOrderDivisions> alldcr = new List<PendingOrderDivisions>();
                    List<PendingOrderDivision> alldcr1 = new List<PendingOrderDivision>();
                    List<PendingOrderDivisionDetails> PendingOrderDivisionDetails = new List<PendingOrderDivisionDetails>();
                    List<PendingOrderDivisionTotal> PendingOrderDivisionTotal = new List<PendingOrderDivisionTotal>();

                    DataTable dr = new DataTable();

                    if(ula.ExecId==0)
                    {
                         dr = g1.return_dt("App_PendingOrderDivisionWise '" + ula.CIN + "'");

                    }
                    else
                    {
                         dr = g1.return_dt("App_PendingOrderDivisionWisegstar '" + ula.CIN + "','" + ula.ExecId + "'");

                    }
                   

                    if (dr.Rows.Count > 0)
                    {
                        decimal total = 0;
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            PendingOrderDivisionDetails.Add(new PendingOrderDivisionDetails
                            {
                                divisionnm = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                pending = Convert.ToString(dr.Rows[i]["pendingamt"].ToString())
                            });

                            total = total + Convert.ToDecimal(dr.Rows[i]["pendingamt"]);
                        }

                        PendingOrderDivisionTotal.Add(new PendingOrderDivisionTotal
                        {
                            // totalsale = dr1.Compute("Sum(BalAmt)", "").ToString()
                            pendingtotal = total.ToString()
                        });

                        alldcr1.Add(new PendingOrderDivision
                        {
                            pendingdetails = PendingOrderDivisionDetails,
                            pendingtotal = PendingOrderDivisionTotal
                        });

                        g1.close_connection();
                        alldcr.Add(new PendingOrderDivisions
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