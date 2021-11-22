using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Project.Service.Filters;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Project.Service.Controllers
{
    public class OuststandingDivisionWiseController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/getOutstndingDivisionWise")]
        public HttpResponseMessage GetDetails(OutstandingDivisionAction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN != null)
            {
                try
                {
                    string data1;

                    List<OutstandingDivisions> alldcr = new List<OutstandingDivisions>();
                    List<OutstandingDivision> alldcr1 = new List<OutstandingDivision>();
                    List<OutstandingDivisionDetails> OutstandingDivisionDetails = new List<OutstandingDivisionDetails>();
                    List<OutstandingDivisionTotal> OutstandingDivisionTotal = new List<OutstandingDivisionTotal>();

                    DataTable dr = g1.return_dt("App_OutstandingDivisionWise '" + ula.CIN + "'");

                    if (dr.Rows.Count > 0)
                    {
                        decimal duetotal = 0;
                        decimal overduetotal = 0;
                        decimal outstandingtotal = 0;
                        for (int i = 0; i < dr.Rows.Count; i++)
                        {
                            OutstandingDivisionDetails.Add(new OutstandingDivisionDetails
                            {
                                divisionnm = Convert.ToString(dr.Rows[i]["divisionnm"].ToString()),
                                due = Convert.ToString(dr.Rows[i]["due"].ToString()),
                                overdue = Convert.ToString(dr.Rows[i]["overdue"].ToString()),
                                outstanding = Convert.ToString(dr.Rows[i]["outstanding"].ToString())
                            });

                            duetotal = duetotal + Convert.ToDecimal(dr.Rows[i]["due"]);
                            overduetotal = overduetotal + Convert.ToDecimal(dr.Rows[i]["overdue"]);
                            outstandingtotal = outstandingtotal + Convert.ToDecimal(dr.Rows[i]["outstanding"]);
                        }

                        OutstandingDivisionTotal.Add(new OutstandingDivisionTotal
                        {
                            // totalsale = dr1.Compute("Sum(BalAmt)", "").ToString()
                            duetotal = duetotal.ToString(),
                            overduetotal = overduetotal.ToString(),
                            outstandingtotal = outstandingtotal.ToString()
                        });

                        alldcr1.Add(new OutstandingDivision
                        {
                            outstandingdetails = OutstandingDivisionDetails,
                            outstandingtotal = OutstandingDivisionTotal,
                            OnlinePayment= Convert.ToBoolean(dr.Rows[0]["onlinepayment"]),
                            channel= Convert.ToBoolean(dr.Rows[0]["channel"]),
                            freepay= Convert.ToBoolean(dr.Rows[0]["freepay"]),
                            payu= Convert.ToBoolean(dr.Rows[0]["payu"]),
                            fanfilter= Convert.ToBoolean(dr.Rows[0]["fanfilter"]),
                            IsActive= Convert.ToBoolean(dr.Rows[0]["IsActive"]),
                            isregistered = Convert.ToBoolean(dr.Rows[0]["freepayregister"]),
                            errormsg = Convert.ToString(dr.Rows[0]["errormsg"]),
                            duesquence= Convert.ToBoolean(dr.Rows[0]["duesquence"]),
                            PayUMinAmt = Convert.ToDecimal(ConfigurationManager.AppSettings["PayUMinAmt"])
                        });

                        g1.close_connection();
                        alldcr.Add(new OutstandingDivisions
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