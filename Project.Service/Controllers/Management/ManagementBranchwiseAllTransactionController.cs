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
    public class ManagementBranchwiseAllTransactionController : ApiController
    {
        [HttpPost]
        [ValidateModel]
        [Route("api/GetBranchwiseAllTransaction")]
        public HttpResponseMessage GetDetails(ListofManagementBranchwiseAllTransaction ula)
        {
            DataConection g1 = new DataConection();
            Common cm = new Common();
            if (ula.CIN !="")
            {
                try
                {
                    string data1;

                    List<ManagementBranchwiseAllTransactions> alldcr = new List<ManagementBranchwiseAllTransactions>();
                    List<ManagementBranchwiseAllTransaction> alldcr1 = new List<ManagementBranchwiseAllTransaction>();

                    var dr = g1.return_dr("BranchwiseAllTransactionselect '"+ula.CIN+ "','" + ula.Category + "' ");

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            alldcr1.Add(new ManagementBranchwiseAllTransaction
                            {
                                branchid = Convert.ToString(dr["branchid"].ToString()),
                                branchnm = Convert.ToString(dr["branchnm"].ToString()),
                                salewithtaxamt = Convert.ToString(dr["salewithtaxamt"].ToString()),
                                salewithouttaxamt = Convert.ToString(dr["salewithouttaxamt"].ToString()),
                                payment = Convert.ToString(dr["payment"].ToString()),
                                creditnote = Convert.ToString(dr["creditnote"].ToString()),
                                debitnote = Convert.ToString(dr["debitnote"].ToString()),
                                outstandingamt = Convert.ToString(dr["outstandingamt"].ToString()),
                                stockamt = Convert.ToString(dr["stockamt"].ToString()),
                                purchaseamt = Convert.ToString(dr["purchaseamt"].ToString()),

                            });
                        }
                        g1.close_connection();
                        alldcr.Add(new ManagementBranchwiseAllTransactions
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
                        response.Content = new StringContent(cm.StatusTime(true, "No Data Found"), Encoding.UTF8, "application/json");

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