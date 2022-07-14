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
                                TransactionID = Convert.ToString(dr.Rows[i]["TransactionID"].ToString()),
                                TransactionDate = Convert.ToString(dr.Rows[i]["TransactionDate"].ToString()),
                                TranAmount = Convert.ToString(dr.Rows[i]["TranAmount"].ToString()),
                                BalanceOutstanding = Convert.ToString(dr.Rows[i]["BalanceOutstanding"].ToString()),
                                DueDate = Convert.ToString(dr.Rows[i]["DueDate"].ToString()),
                                //OverdueWithinCureINR = Convert.ToString(dr.Rows[i]["OverdueWithinCureINR"].ToString()),
                                //OverdueWithinCureNoOfDays = Convert.ToString(dr.Rows[i]["OverdueWithinCureNoOfDays"].ToString()),
                                //OverdueBeyondCureINR = Convert.ToString(dr.Rows[i]["OverdueBeyondCureINR"].ToString()),
                                //OverdueBeyondCureNoOfDays = Convert.ToString(dr.Rows[i]["OverdueBeyondCureNoOfDays"].ToString()),
                               
                            });
                        }

                     //   var dttotalsum = g1.return_dt("App_CFDetails '" + ula.CIN + "'," + 0 + "," + 999999);


                        TotalCFDetailsListdata.Add(new TotalCFDetailsList
                        {
                            Dealerssanctionlimits = Convert.ToString(dr.Rows[0]["Dealerssanctionlimits"].ToString()),
                            AvailableLimits = Convert.ToString(dr.Rows[0]["AvailableLimits"].ToString()),
                            BalanceOSwiththebank = Convert.ToString(dr.Rows[0]["BalanceOSwiththebank"].ToString()),
                            OverdueWithinCureINR = Convert.ToString(dr.Rows[0]["OverdueWithinCureINR"].ToString()),
                            OverdueWithinCureNoOfDays = Convert.ToString(dr.Rows[0]["OverdueWithinCureNoOfDays"].ToString()),
                            OverdueBeyondCureINR = Convert.ToString(dr.Rows[0]["OverdueBeyondCureINR"].ToString()),
                            OverdueBeyondCureNoOfDays = Convert.ToString(dr.Rows[0]["OverdueBeyondCureNoOfDays"].ToString()),
                            IsFreeze = Convert.ToBoolean(dr.Rows[0]["IsFreeze"].ToString()),
                            
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